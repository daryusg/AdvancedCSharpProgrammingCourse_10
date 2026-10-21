//Part 25 - C# Attributes
#define LOG_INFO //must be at the top of the file (before any using statements otherwise it will not work)
using System.Reflection;
using AttributesExamples.Models;
using LoggingComponent;
using ValidationComponent; //note: added "Project Reference..." to LoggingComponent project

//[assembly: AssemblyVersion("2.0.1")]
//Compile-time error: Duplicate 'System.Reflection.AssemblyVersionAttribute' attribute:
//double-click on error opens auto-generated file: C:\Users\kev\source\repos\AdvancedCSharpProgrammingCourse_10\AttributesExamples\AttributesExamples\obj\Debug\net10.0\AttributesExamples.AssemblyInfo.cs
//  line 20: [assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")]
[assembly: AssemblyDescription("My Assembly Description")]

//https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild?view=visualstudio

namespace AttributesExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee emp = new Employee();
            Department dept = new Department();

            string empId = string.Empty;
            string firstName = string.Empty;
            string postCode = string.Empty;

            string deptShortName = string.Empty;


            Type employeeType = typeof(Employee);
            Type departmentType = typeof(Department);

            if (GetInput(employeeType, "Enter Employee Id: ", "Id", out empId))
                emp.Id = Int32.Parse(empId);
            //or
            //emp.Id = Convert.ToInt32(empId);

            if(GetInput(employeeType, "Enter First Name: ", "FirstName", out firstName))
                emp.FirstName = firstName;
            
            if(GetInput(employeeType, "Enter Post Code: ", "PostCode", out postCode))
                emp.PostCode = postCode;

            if (GetInput(departmentType, "Enter Department Short Name: ", "ShortName", out deptShortName))
                dept.ShortName = deptShortName;

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Thank you for entering the employee information successfully!! (First Name: {emp.FirstName}, Post Code: {emp.PostCode}, Department Short Name: {dept.ShortName}).");
            Console.WriteLine();
            Console.ResetColor();

            Console.ReadLine();

            var employeeJSON = System.Text.Json.JsonSerializer.Serialize(emp);
            //var employeeJSON = System.Text.Json.JsonSerializer.Serialize<Employee>(emp);
            Console.WriteLine(employeeJSON);
            /*
                https://dotnet.microsoft.com/en-us/apps/aspnet/mvc
                public class Person
                {
                    public int PersonId { get; set; }

                    [Required]
                    [MinLength(2)]
                    public string Name { get; set; }

                    [Phone]
                    public string PhoneNumber { get; set; }

                    [EmailAddress]
                    public string Email { get; set; }
                }
            */
            //OutputGlobalAttributeInfo();
        }

        private static bool GetInput(Type t, string promptText, string fieldName, out string fieldValue)
        {
            fieldValue = string.Empty;
            string errorMessage = string.Empty;
            do
            {
                Console.Write(promptText);
                string inputValue = Console.ReadLine();
                if (!Validation.PropertyValueIsValid(t, inputValue, fieldName, out errorMessage))
                {
                    fieldValue = string.Empty;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor= ConsoleColor.White;
                    Console.WriteLine($"Error: {errorMessage}");
                    Console.WriteLine();
                    Console.ResetColor();
                }
                else
                {
                    fieldValue = inputValue;
                    break;
                }
            } while (true);

            return true;
        }

        private static void LoggingTest()
        {
            Logging.LogToScreen("This message is testing the logging functionality.");
        }

        private static void OutputGlobalAttributeInfo()
        {
            //---------------------------------------------------------------------------------
            //with chatgpt assist:
            //---------------------------------------------------------------------------------
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //Console.WriteLine($"Assembly: {assembly.FullName}");
            //Console.WriteLine($"Assembly Version: {assembly.GetName().Version}");
            //// Get the AssemblyDescription attribute
            //var descriptionAttribute = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            //if (descriptionAttribute != null)
            //{
            //    Console.WriteLine($"Description: {descriptionAttribute.Description}");
            //}
            //---------------------------------------------------------------------------------
            Assembly thisAss = typeof(Program).Assembly;
            AssemblyName thisAssName = thisAss.GetName();
            Version thisAssVersion = thisAssName.Version;
            object[] attributes = thisAss.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            //var thisAssDescriptionAttributes = ((AssemblyDescriptionAttribute)attributes[0]).Description;
            var thisAssDescriptionAttributes = attributes[0] as AssemblyDescriptionAttribute;


            Console.WriteLine($"Assembly Name: {thisAssName}");
            Console.WriteLine($"Assembly Version: {thisAssVersion}");
            if (thisAssDescriptionAttributes != null)
                Console.WriteLine($"Assembly Description: {thisAssDescriptionAttributes.Description}");
            //or new style(ish):
            //Console.WriteLine($"Assembly Description: {thisAssDescriptionAttributes?.Description}");
        }
    }
}
