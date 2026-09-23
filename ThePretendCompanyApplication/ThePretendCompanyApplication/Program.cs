using TCPData;
using TCPExtensions;

//Part 21 - LINQ - Introduction
//https://learn.microsoft.com/en-us/archive/blogs/mattwar/linq-building-an-iqueryable-provider-part-i
//https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/linq-to-dataset
//https://learn.microsoft.com/en-us/dotnet/standard/linq/linq-xml-overview

namespace ThePretendCompanyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = Data.GetEmployees();
            int salaryFilter = 50000;
            bool greaterThanFilter = false;
            bool isManagerFilter = false;

            var filteredEmployees = employees.Filter(e => greaterThanFilter ? e.AnnualSalary > salaryFilter && e.IsManager == isManagerFilter : e.AnnualSalary < salaryFilter && e.IsManager == isManagerFilter).ToList();

            Console.WriteLine($"Filtered Employees (Annual Salary {(greaterThanFilter ? ">" : "<")} {salaryFilter:C} and is{(isManagerFilter ? "" : "n't")} a manager):");
            Console.WriteLine($"{"FirstName",-15} {"LastName",-20} {"Annual Salary",13}    {"Is Manager?",-11}");
            foreach (var employee in filteredEmployees)
            {
                Console.WriteLine($"{employee.FirstName,-15} {employee.LastName,-20} {employee.AnnualSalary,13:C}    {employee.IsManager,-11}");
            }

            Console.WriteLine();
            Console.WriteLine();

            List<Department> departments = Data.GetDepartments();
            //var filteredDepartments = departments.Filter(d => d.ShortName.StartsWith("H")).ToList();
            //var filteredDepartments = departments.Filter(d => d.Id > 1).ToList();
            //var filteredDepartments = departments.Where(d => d.Id > 1).ToList();
            var filteredDepartments = departments.Where(d => d.ShortName == "HR" || d.ShortName == "FIN").ToList();
            Console.WriteLine("Departments:");
            Console.WriteLine($"{"Id":2}    {"Short Name",-13} {"Long Name",-30}");
            foreach (var department in filteredDepartments)
            {
                Console.WriteLine($"{department.Id,2}    {department.ShortName,-13} {department.LongName,-30}");
            }

            Console.WriteLine();
            Console.WriteLine();

            var resultList = from emp in employees
                             join dept in departments on emp.DepartmentId equals dept.Id
                             where greaterThanFilter ? emp.AnnualSalary >= salaryFilter && emp.IsManager == isManagerFilter : emp.AnnualSalary <= salaryFilter && emp.IsManager == isManagerFilter
                             orderby emp.LastName, emp.FirstName
                             select new
                             {
                                 FirstName = emp.FirstName,
                                 LastName = emp.LastName,
                                 AnnualSalary = emp.AnnualSalary,
                                 Manager = emp.IsManager,
                                 Department = dept.LongName
                             };

            Console.WriteLine($"Filtered Employees (Annual Salary {(greaterThanFilter ? ">=" : "<=")} {salaryFilter:C} and is{(isManagerFilter ? "" : "n't")} a manager):");
            Console.WriteLine($"{"FirstName",-15} {"LastName",-20} {"Annual Salary",13}    {"Department",-11}");
            foreach (var employee in resultList)
            {
                Console.WriteLine($"{employee.FirstName,-15} {employee.LastName,-20} {employee.AnnualSalary,13:C}    {employee.Department,-11}");
            }

            var averageAnualSalary = resultList.Average(e => e.AnnualSalary);
            var highestAnnualSalary = resultList.Max(e => e.AnnualSalary);
            var lowestAnnualSalary = resultList.Min(e => e.AnnualSalary);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Average Annual Salary: {averageAnualSalary:C}");
            Console.WriteLine($"Highest Annual Salary: {highestAnnualSalary:C}");
            Console.WriteLine($"Lowest Annual Salary: {lowestAnnualSalary:C}");
        }
    }
}
