using System.Collections;
using System.ComponentModel;
using System.Xml.Linq;

namespace LINQExamples_2
{
    //Part 23 - LINQ - Operators
    //https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/
    //c# doc not found: https://learn.microsoft.com/en-us/dotnet/visual-basic/programming-guide/concepts/linq/query-expression-syntax-for-standard-query-operators
    //c# doc not found: https://learn.microsoft.com/en-us/dotnet/visual-basic/programming-guide/concepts/linq/classification-of-standard-query-operators-by-manner-of-execution
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = Data.GetEmployees();
            List<Department> departments = Data.GetDepartments();

            ////Sorting Operations OrderBy, OrderByDescending, ThenBy, ThenByDescending
            ////Method Synax
            // var results = employees.Join(departments,
            //     e => e.DepartmentId,
            //     d => d.Id,
            //     (e, d) => new
            //     {
            //         Id = e.Id,
            //         EmployeeName = $"{e.FirstName} {e.LastName}",
            //         FirstName = e.FirstName,
            //         LastName = e.LastName,
            //         AnnualSalary = e.AnnualSalary,
            //         DepartmentId = e.DepartmentId,
            //         DepartmentName = d.LongName,
            //         //}).OrderBy(o => o.DepartmentId);
            //         //}).OrderByDescending(o => o.DepartmentId);
            //         //}).OrderBy(o => o.DepartmentId).ThenBy(o => o.AnnualSalary);
            //     }).OrderBy(o => o.DepartmentId).ThenByDescending(o => o.AnnualSalary);

            ////Query Syntax
            //var results = from e in employees
            //              join d in departments on e.DepartmentId equals d.Id
            //              orderby e.DepartmentId, e.AnnualSalary
            //              //orderby e.DepartmentId, e.AnnualSalary descending
            //              select new
            //              {
            //                  Id = e.Id,
            //                  EmployeeName = $"{e.FirstName} {e.LastName}",
            //                  FirstName = e.FirstName,
            //                  LastName = e.LastName,
            //                  AnnualSalary = e.AnnualSalary,
            //                  DepartmentId = e.DepartmentId,
            //                  DepartmentName = d.LongName,
            //              };
            //foreach (var item in results)
            //    Console.WriteLine($"Id: {item.Id,-5} FirstName: {item.FirstName,-10} LastName: {item.LastName,-10} Annual Salary: {item.AnnualSalary,10:C}\t Department Name: {item.DepartmentName}");

            //Grouping Operators
            ////GroupBy Operator - deferred execution
            //var groupResult = from e in employees
            //                  orderby e.DepartmentId
            //                  group e by e.DepartmentId; //note: doesn't end with a select operator
            ////Method Syntax
            ////var groupResult = employees.GroupBy(e => e.DepartmentId);

            //ToLookUp Operator - immediate execution
            //var groupResult = employees.ToLookup(e => e.DepartmentId); //note: doesn't end with a select operator
            var groupResult = employees.OrderBy(o => o.DepartmentId).ToLookup(e => e.DepartmentId); //note: doesn't end with a select operator

            //note: GroupBy and ToLookUp return an IGrouping generic interface: https://learn.microsoft.com/en-us/dotnet/api/system.linq.igrouping-2?view=net-10.0

            //foreach (var empGroup in groupResult)
            //{
            //    Console.WriteLine($"DepartmentId: {empGroup.Key}");
            //    foreach (var item in empGroup)
            //        Console.WriteLine($"\tFullName: {item.FirstName} {item.LastName}");
            //}

            ////Any, All, Contains Quantifier Operators
            ////All, Any
            //var annualSalaryCompare = 20_000;
            //var annualSalaryCompare = 100_000;
            //var annualSalaryCompare = 40_000;

            //bool isTrueAll = employees.All(e => e.AnnualSalary > annualSalaryCompare);
            //if (isTrueAll)
            //    Console.WriteLine($"All employees have an annual salary greater than {annualSalaryCompare:C}");
            //else
            //    Console.WriteLine($"Not all employees have an annual salary greater than {annualSalaryCompare:C}");

            //bool isTrueAny = employees.Any(e => e.AnnualSalary > annualSalaryCompare);
            //if (isTrueAny)
            //    Console.WriteLine($"At least one employee has an annual salary greater than {annualSalaryCompare:C}");
            //else
            //    Console.WriteLine($"No employees have an annual salary greater than {annualSalaryCompare:C}");

            ////Contains Operator
            //var searchEmployee = new Employee
            //{
            //    Id = 3,
            //    FirstName = "Douglas",
            //    LastName = "Roberts",
            //    AnnualSalary = 40000.2m,
            //    IsManager = false,
            //    //DepartmentId = 2 //note: somehow, changed to 1 in-between 10:41:03 and 10:42:18
            //    DepartmentId = 1
            //};

            ////bool containsEmployee = employees.Contains(searchEmployee); <---doesn't work. further investigate for clarity
            //bool containsEmployee = employees.Contains(searchEmployee, new EmployeeComparer()); // <---works. further investigate for clarity
            //if (containsEmployee)
            //    Console.WriteLine($"{searchEmployee.FirstName} {searchEmployee.LastName} is in the employees list");
            //else
            //    Console.WriteLine($"{searchEmployee.FirstName} {searchEmployee.LastName} is NOT in the employees list");

            ////OfType filter Operator
            //ArrayList mixedCollection = Data.GetHeterogeneousDataCollection();

            //var stringResult = from s in mixedCollection.OfType<string>()
            //                   select s;
            //Console.WriteLine("Strings in the mixed collection:");
            //foreach (var str in stringResult)
            //    Console.WriteLine($"\t{str}");

            //var intResult = from i in mixedCollection.OfType<int>()
            //                select i;
            //Console.WriteLine("Integers in the mixed collection:");
            //foreach (var num in intResult)
            //    Console.WriteLine($"\t{num}");

            //var employeeResult = from e in mixedCollection.OfType<Employee>()
            //                    select e;
            //Console.WriteLine("Employees in the mixed collection:");
            //foreach (var emp in employeeResult)
            //    Console.WriteLine($"\t{emp.Id,-5}{emp.FirstName,-10} {emp.LastName,-10}");

            //var deptResult = from d in mixedCollection.OfType<Department>()
            //                     select d;
            //Console.WriteLine("Departments in the mixed collection:");
            //foreach (var dept in deptResult)
            //    Console.WriteLine($"\t{dept.Id,-5} {dept.LongName,-30} {dept.ShortName,-10}");

            ////ElemantAt, ElementAtOrDefault, First, FirstOrDefault, Last, LastOrDefault, Single, and SingleOrDefault Operators
            ////ElemantAt, ElementAtOrDefault Operators
            //int i = 3;
            ////var employee = employees.ElementAt(i);
            //var employee = employees.ElementAtOrDefault(i);
            ////DEFAULT VALUES: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values
            ////f               for any reference type, the default value is null.
            //if (employee == null)
            //    Console.WriteLine($"No employee found at element {i}.");
            //else
            //{
            //    Console.WriteLine($"Employee at element {i}:");
            //    Console.WriteLine($"\t{employee.Id,-5}{employee.FirstName,-10} {employee.LastName,-10}");
            //}

            ////First, FirstOrDefault, Last, LastOrDefault Operators
            //List<int> integerList = new List<int> { 3, 14, 23, 17, 28, 89 };
            //List<int> integerList = new List<int> { 3, 141, 23, 27, 17, 281, 89 };
            //int result = integerList.First(); string strDesc = "First";
            //int result = integerList.Last(); string strDesc = "Last";
            //Console.WriteLine($"{strDesc} element in the integer list: {result}");

            //result = integerList.First(i => i % 2 == 0);
            //Console.WriteLine($"{strDesc} even element in the integer list: {result}");
            //result = integerList.Last(i => i % 2 == 0);
            //Console.WriteLine($"{strDesc} even element in the integer list: {result}");
            //result = integerList.FirstOrDefault(i => i % 2 == 0);
            //result = integerList.LastOrDefault(i => i % 2 == 0);
            //if (result != 0)
            //    Console.WriteLine($"{strDesc} even element in the integer list: {result}");
            //else
            //    Console.WriteLine($"No even element found in the integer list.");

            ////Single, SingleOrDefault Operators
            //var employee = employees.Single(); //System.InvalidOperationException: Sequence contains more than one element
            //var employee = employees.Single(e => e.Id == 2);
            //var employee = employees.Single(e => e.Id > 1);
            //var employee = employees.SingleOrDefault(e => e.Id > 1); //System.InvalidOperationException: Sequence contains more than one matching element
            //Console.WriteLine($"{employee.Id,-5}{employee.FirstName,-10} {employee.LastName,-10}");

            var employee = employees.SingleOrDefault(e => e.Id == 2);
            if (employee == null) //null being the default data type value for a reference type, which is what Employee is. https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values
                Console.WriteLine($"No employee found with the specified criteria.");
            else
                Console.WriteLine($"{employee.Id,-5}{employee.FirstName,-10} {employee.LastName,-10}");
        }
    }

    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee? x, Employee? y)
        {
            if (x == null || y == null)
                return false;
            return x.Id == y.Id &&
                   x.FirstName.ToLower() == y.FirstName.ToLower() &&
                   x.LastName.ToLower() == y.LastName.ToLower(); //&&
                   //x.AnnualSalary == y.AnnualSalary &&
                   //x.IsManager == y.IsManager &&
                   //x.DepartmentId == y.DepartmentId;
        }
        public int GetHashCode(Employee obj)
        {
            //return HashCode.Combine(obj.Id, obj.FirstName, obj.LastName, obj.AnnualSalary, obj.IsManager, obj.DepartmentId);
            return obj.Id.GetHashCode();
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public bool IsManager { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }

    public static class Data
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new();

            Employee employee = new Employee
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnnualSalary = 60000.3m,
                IsManager = true,
                //DepartmentId = 1 //note: somehow, changed to 2 in-between 10:41:03 and 10:42:18
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Jameson",
                AnnualSalary = 80000.1m,
                IsManager = true,
                DepartmentId = 3
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 3,
                FirstName = "Douglas",
                LastName = "Roberts",
                AnnualSalary = 40000.2m,
                IsManager = false,
                //DepartmentId = 2 //note: somehow, changed to 1 in-between 10:41:03 and 10:42:18
                DepartmentId = 1
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 4,
                FirstName = "Jane",
                LastName = "Stevens",
                AnnualSalary = 30000.2m,
                IsManager = false,
                DepartmentId = 3
            };
            employees.Add(employee);

            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = [];

            Department department = new Department
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Resources"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Finance"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 3,
                ShortName = "TE",
                LongName = "Technology"
            };
            departments.Add(department);

            return departments;
        }

        public static ArrayList GetHeterogeneousDataCollection()
        {
            ArrayList arrayList = new ArrayList(); //ArrayList is not  recommended for new development. Use List<T> instead. https://learn.microsoft.com/en-us/dotnet/api/system.collections.arraylist?view=net-10.0
            arrayList.Add(100);
            arrayList.Add("Bob Jones");
            arrayList.Add(true);
            arrayList.Add(2000);
            arrayList.Add(3000);
            arrayList.Add("Bill Henderson");
            arrayList.Add(new Employee { Id = 6, FirstName = "Jennifer", LastName = "Dale", AnnualSalary = 90_000m, IsManager = true, DepartmentId = 1 });
            arrayList.Add(new Employee { Id = 7, FirstName = "Dane", LastName = "Hughes", AnnualSalary = 60_000m, IsManager = false, DepartmentId = 2 });
            arrayList.Add(new Department { Id = 4, ShortName = "MK", LongName = "Marketing" });
            arrayList.Add(new Department { Id = 5, ShortName = "R&D", LongName = "Research and Development" });
            arrayList.Add(new Department { Id = 6, ShortName = "PRD", LongName = "Production" });

            return arrayList;
        }

    }
}
