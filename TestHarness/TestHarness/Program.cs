using System.Reflection;
using System.Transactions;

namespace TestHarness
{
    //Part 26 - C# Reflection
    //TestHarness...
    //...discovers relevant namespace within TestAssembly_x
    //...discovers relevant classes within namespace
    //...discovers relevant methods within classes
    //...discovers parameters within methods
    //...passes in a value for each parameter for methods and invokes methods.
    //
    //if results are as expected, TestAssembly_x passes the test.

    //Note: To test, I copied the dll...
    //  from: ...\AdvancedCSharpProgrammingCourse_10\TestHarness\UtilityFunctions\bin\Debug\net10.0
    //  to: ...\AdvancedCSharpProgrammingCourse_10\TestHarness\TestHarness\bin\Debug\net10.0

    //https://learn.microsoft.com/en-us/dotnet/api/system.reflection.emit?view=net-10.0

    public class Program
    {
        const string InformationAttributeTypeName = "UTILITYFUNCTIONS.INFORMATIONATTRIBUTE";
        const string InformationAttributeDescriptionPropertyName = "Description";

        static void Main(string[] args)
        {

            const string TargetAssemblyFilename = "UtilityFunctions.dll";
            const string TargetNamespace = "UtilityFunctions";

            Assembly assembly = Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TargetAssemblyFilename));

            //List<System.Type> classes = assembly.GetTypes()
            //    .Where(t => t.Namespace == TargetNamespace).ToList();
            //or
            List<System.Type> classes = [.. assembly.GetTypes().Where(t => t.Namespace == TargetNamespace && HasInformationAttribute(t))];

            while (true)
            {
                Console.Clear();
                string str = "Please press the number key associated with the test class";
                WritePromptToScreen(str, true);

                //int count = 0;
                //foreach(var classItem in classes)
                //{
                //    Console.WriteLine($"{++count}. {classItem}");
                //}
                DisplayProgramElementList(classes);

                Type typeChoice = ReturnProgramElementReferenceFromList(classes);

                //ConsoleKey consoleKey = Console.ReadKey().Key;
                //Type classChoice = null;

                //bool retryGetKey = true;
                //int validNum;
                //while (retryGetKey)
                //{
                //    try
                //    {
                //        validNum = Convert.ToInt16(consoleKey);
                //        classChoice = classes[validNum];
                //        retryGetKey = false;
                //    }
                //    catch { }
                //}

                //switch(consoleKey)
                //{
                //    case ConsoleKey.D1:
                //        classChoice = classes[0];
                //        break;
                //    case ConsoleKey.D2:
                //        classChoice = classes[1];
                //        break;
                //    case ConsoleKey.D3:
                //        classChoice = classes[2];
                //        break;
                //    case ConsoleKey.D4:
                //        classChoice = classes[3];
                //        break;
                //}

                object classInstance = Activator.CreateInstance(typeChoice, null);

                Console.Clear();
                WriteHeadingToScreen($"Class: '{typeChoice}'");
                DisplayElementDescription(ReturnInformationCustomAttributeDescription(typeChoice));

                WritePromptToScreen("Select the number of the method to be tested", true);

                //List<MethodInfo> methods = typeChoice.GetMethods().ToList();
                List<MethodInfo> methods = [.. typeChoice.GetMethods().Where(m => HasInformationAttribute(m))];

                //DisplayProgramElementList<MethodInfo>(methods);
                DisplayProgramElementList(methods);

                MethodInfo methodChoice = ReturnProgramElementReferenceFromList(methods);

                if (methodChoice != null)
                {
                    Console.Clear();
                    WriteHeadingToScreen($"Class: '{typeChoice}' - Method: {methodChoice.Name}");
                    DisplayElementDescription(ReturnInformationCustomAttributeDescription(methodChoice));

                    ParameterInfo[] parameters = methodChoice.GetParameters();
                    object result = GetResult(classInstance, methodChoice, parameters);
                    WriteResultToScreen(result);
                }

                Console.WriteLine();
                WritePromptToScreen("Press Spacebar to exit or any other key to continue");
                if(Console.ReadKey().Key == ConsoleKey.Spacebar)
                    break;
            }
        }

        private static string ReturnInformationCustomAttributeDescription(MemberInfo mi)
        {
            foreach(var attrib in mi.GetCustomAttributes())
            {
                Type typeAttrib = attrib.GetType();
                if (typeAttrib.ToString().ToUpper() == InformationAttributeTypeName)
                {
                    PropertyInfo propertyInfo = typeAttrib.GetProperty(InformationAttributeDescriptionPropertyName);
                    if (propertyInfo != null)
                    {
                        object s = propertyInfo.GetValue(attrib, null);
                        if (s != null)
                            return s.ToString();
                    }
                }
            }
            return null;
        }

        private static void DisplayElementDescription(string elementDescription)
        {
            if (elementDescription != null)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(elementDescription);
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        private static bool HasInformationAttribute(MemberInfo mi)
        {
            foreach (var attrib in mi.GetCustomAttributes())
            {
                Type typeAttrib = attrib.GetType();
                if (typeAttrib.ToString().ToUpper() == InformationAttributeTypeName)
                    return true;
            }
            return false;
        }

        private static void WriteResultToScreen(object result)
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Result: {result}");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static object[] ReturnParameterValueInputAsObjectArray(ParameterInfo[] parameters)
        {
            object[] paramValues = new object[parameters.Length];
            int itemCount = 0;

            foreach (ParameterInfo parameterInfo in parameters)
            {
                //if (parameterInfo.ParameterType == typeof(string))
                //{
                //}
                WritePromptToScreen($"Enter a value for {parameterInfo.Name} ({parameterInfo.ParameterType.Name}): ");
                bool invalidEntry = true;
                while(invalidEntry)
                {
                    try
                    {
                        switch (parameterInfo.ParameterType)
                        {
                            case Type t when t == typeof(string):
                                string inputString = Console.ReadLine();
                                paramValues[itemCount] = inputString;
                                invalidEntry = false;
                                break;

                            case Type t when t == typeof(int):
                                int inputInt = Int32.Parse(Console.ReadLine());
                                paramValues[itemCount] = inputInt;
                                invalidEntry = false;
                                break;

                            case Type t when t == typeof(double):
                                double inputDouble = Double.Parse(Console.ReadLine());
                                paramValues[itemCount] = inputDouble;
                                invalidEntry = false;
                                break;
                        }
                    }
                    catch
                    {
                        WritePromptToScreen($"Invalid entry. Try again...");
                    }
                }
                itemCount++;
            }

            return paramValues;
        }

        private static object GetResult(Object classInstance, MethodInfo methodInfo, ParameterInfo[] parameters)
        {
            object result = null;
            if (parameters.Length == 0)
                result = methodInfo.Invoke(classInstance, null);
            else
            {
                var paramValueArray = ReturnParameterValueInputAsObjectArray(parameters);
                result = methodInfo.Invoke(classInstance, paramValueArray);
            }

            return result;
        }

        private static T ReturnProgramElementReferenceFromList<T>(List<T> list)
        {
            //switch (consoleKey)
            //{
            //    case ConsoleKey.D1:
            //        return list[0];
            //    case ConsoleKey.D2:
            //        return list[1];
            //    case ConsoleKey.D3:
            //        return list[2];
            //    case ConsoleKey.D4:
            //        return list[3];
            //}

            bool retryGetKey = true;
            int validNum;
            T ret;
            while (retryGetKey)
            {
                try
                {
                    ConsoleKey consoleKey = Console.ReadKey().Key;
                    if (consoleKey >= ConsoleKey.D0 && consoleKey <= ConsoleKey.D9)
                    {
                        validNum = consoleKey - ConsoleKey.D0;
                        ret = list[validNum - 1];
                        return ret;
                    }
                }
                catch { }
            }
            return default;
        }

        private static void DisplayProgramElementList<T>(List<T> list)
        {
            int count = 0;
            foreach (var item in list)
                Console.WriteLine($"{++count}. {item}");
        }

        private static void WriteHeadingToScreen(string str)
        {
            string str2 = "---   ";
            string str3 = "   ---";
            var colourBorder = ConsoleColor.Green;
            var colourText = ConsoleColor.Yellow;
            int addedLength = str2.Length + str3.Length;
            int totalLength = str.Length + addedLength;
            Console.ForegroundColor = colourBorder;
            Console.WriteLine(new string('-', totalLength));
            Console.Write(str2);
            Console.ForegroundColor = colourText;
            Console.Write(str);
            Console.ForegroundColor = colourBorder;
            Console.Write(str3 + "\n");
            Console.WriteLine(new string('-', totalLength));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void WritePromptToScreen(string str, bool ul = false)
        {
            Console.WriteLine(str);
            if (ul)
                Console.WriteLine(new string('-', str.Length));
        }
    }
}
