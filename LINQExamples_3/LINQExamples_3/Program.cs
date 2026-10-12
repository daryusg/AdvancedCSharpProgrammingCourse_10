namespace LINQExamples_3
{
    //Part 24 - LINQ - More Operators and Summary

    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = Data.GetEmployees();
            //List<Department> departments = Data.GetDepartments();
            List<Department> departments = Data.GetDepartments(employees);  //amended 12:12:49

            //Equality Operator
            //SequenceEqual
            //var integerList1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            //var integerList2 = new List<int> { 1, 2, 3, 4, 5, 6 };
            //var boolSequenceEqual = integerList1.SequenceEqual(integerList2);
            //Console.WriteLine(boolSequenceEqual);

            //var employeeListCompare = Data.GetEmployees();
            //bool boolSE = employees.SequenceEqual(employeeListCompare); <---fails
            //bool boolSE = employees.SequenceEqual(employeeListCompare, new EmployeeComparer());
            //Console.WriteLine(boolSE);

            //Concatenation Operator
            //Concat
            //List<int> integerList1 = [1, 2, 3, 4];
            //List<int> integerList2 = new List<int> { 5, 6, 7, 8, 9, 10 };
            //IEnumerable<int> concatenatedIntegerList = integerList1.Concat(integerList2);

            //foreach (int i in concatenatedIntegerList)
            //    Console.WriteLine(i);

            //List<Employee> employeeList1 = new List<Employee>
            //{
            //    new Employee { Id = 5, FirstName = "Tony", LastName = "Stark" },
            //    new Employee { Id = 6, FirstName = "Debbie", LastName = "Townsend" }
            //};

            //IEnumerable<Employee> concatenatedEmployeeList = employees.Concat(employeeList1);

            //foreach (var item in concatenatedEmployeeList)
            //    Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10}");

            //Aggregate Operators - Aggregate, Average, Count, Max, Min, Sum
            //Aggregate
            //decimal totalAnnualSalary = employees.Aggregate<Employee, decimal>(0.0m, (totalAnnualSalary, e) => {
            //    var bonus = e.IsManager ? 0.04m : 0.02m;
            //    return totalAnnualSalary + e.AnnualSalary + (e.AnnualSalary * bonus);
            //});

            //Console.WriteLine($"Total Annual Salary of All Employees (incl. bonus): {totalAnnualSalary:C}");

            //string data = employees.Aggregate<Employee, string>("Employee Annual Salaries (incl. bonus):\n", (s, e) => {
            //    var bonus = e.IsManager ? 0.04m : 0.02m;
            //    s += $"{e.Id,-5} {e.FirstName,-10} {e.LastName,-10} {e.AnnualSalary + (e.AnnualSalary * bonus):C}\n";
            //    return s;
            //    });
            //string commaAndSpace = ", ";
            //string data = employees.Aggregate<Employee, string, string>("Employee Annual Salaries (incl. bonus):\n", (s, e) => {
            //    var bonus = e.IsManager ? 0.04m : 0.02m;
            //    s += $"{e.Id,-5} {e.FirstName,-10} {e.LastName,-10} {e.AnnualSalary + (e.AnnualSalary * bonus):C}{commaAndSpace}";
            //    return s;
            //}, s => s.Substring(0, s.Length - commaAndSpace.Length));

            //Console.WriteLine(data);

            //Average
            //decimal averageAnnualSalary = employees.Average(e => e.AnnualSalary);
            //Console.WriteLine($"Average Annual Salary of All Employees: {averageAnnualSalary:C}");

            //decimal averageAnnualSalary = employees.Where(e => e.DepartmentId == 3).Average(e => e.AnnualSalary);
            //Console.WriteLine($"Average Annual Salary (Technology Department): {averageAnnualSalary:C}");

            //Count
            //int employeeCount = employees.Count();
            //Console.WriteLine($"Total Number of Employees: {employeeCount}");

            //int technologyDepartmentEmployeeCount = employees.Where(e => e.DepartmentId == 3).Count();
            //int technologyDepartmentEmployeeCount = employees.Count(e => e.DepartmentId == 3);
            //Console.WriteLine($"Number of Employees (Technology Department): {technologyDepartmentEmployeeCount}");

            //Sum
            //decimal totalAnnualSalary = employees.Sum(e => e.AnnualSalary);
            //Console.WriteLine($"Total Annual Salary of All Employees: {totalAnnualSalary:C}");

            //decimal totalAnnualSalary = employees.Where(e => e.DepartmentId == 3).Sum(e => e.AnnualSalary);
            //Console.WriteLine($"Total Annual Salary (Technology Department): {totalAnnualSalary:C}");

            //Max
            //var maxAnnualSalary = employees.Max(e => e.AnnualSalary);
            //Console.WriteLine($"Highest Annual Salary of All Employees: {maxAnnualSalary:C}");

            //var maxAnnualSalary = employees.Where(e => e.DepartmentId == 1).Max(e => e.AnnualSalary);
            //Console.WriteLine($"Highest Annual Salary (Human Resources): {maxAnnualSalary:C}");


            //Generation Operators - DefaultIfEmpty, Empty, Range, Repeat
            //DefaultIfEmpty
            //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/default-values
            List<int> intList = new List<int>();
            //var newList = intList.DefaultIfEmpty();
            //var newList = intList.DefaultIfEmpty(999999);

            //Console.WriteLine(newList.ElementAt(0));
            //List<Employee> employeeList = new List<Employee>();
            //var newList = employeeList.DefaultIfEmpty(new Employee { Id = 0, FirstName = "No", LastName = "Employee" });

            //var result = newList.ElementAt(0);
            //if (result.Id == 0)
            //{
            //    Console.WriteLine("No Employee Found");
            //    Console.WriteLine($"Result: {result.FirstName} {result.LastName}");
            //}
            //else
            //    Console.WriteLine($"Result: {result.FirstName} {result.LastName}");

            //Empty
            //IEnumerable<Employee> emptyEmployeeList = Enumerable.Empty<Employee>();
            //List<Employee> emptyEmployeeList = Enumerable.Empty<Employee>().ToList();
            //emptyEmployeeList.Add(new Employee { Id = 7, FirstName = "Dan", LastName = "Brown", AnnualSalary = 50000m, IsManager = false, DepartmentId = 1 });

            //foreach(var item in emptyEmployeeList)
            //    Console.WriteLine($"Result: {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C} {item.IsManager,10}");

            //Range
            //var intCollection = Enumerable.Range(25, 20);
            //foreach (var item in intCollection)
            //    Console.WriteLine(item);

            //Repeat
            //var strCollection = Enumerable.Repeat<string>("Placeholder", 10);
            //foreach (var item in strCollection)
            //    Console.WriteLine(item);


            //Set Operators - Distinct, Except, Intersect, Union
            //Distinct
            //List<int> ints = [1, 2, 2, 1, 4, 56, 8, 7, 7, 5, 3, 2, 4, 12, 34, 56];
            //var results = ints.Distinct();
            //foreach (var item in results)
            //    Console.WriteLine($"{item}");

            //Except
            //IEnumerable<int> collection1 = [1, 2, 3, 4];
            //IEnumerable<int> collection2 = [3, 4, 5, 6];
            //var results = collection1.Except(collection2); //note: 1 & 2 exist in the 1st collection but not in the 2nd collection, so they are returned
            //foreach (var item in results)
            //    Console.WriteLine($"collection1.Except(collection2): {item}");
            //results = collection2.Except(collection1); //note: 5 & 6 exist in the 2nd collection but not in the 1st collection, so they are returned
            //foreach (var item in results)
            //    Console.WriteLine($"collection2.Except(collection1): {item}");

            //List<Employee> employeesList =
            //[
            //    new Employee
            //    {
            //        Id = 1,
            //        FirstName = "Bob",
            //        LastName = "Jones",
            //        AnnualSalary = 60_000.3m,
            //        IsManager = true,
            //        //DepartmentId = 1 //note: somehow, changed to 2 in-between 10:41:03 and 10:42:18
            //        DepartmentId = 2
            //    },
            //    new Employee
            //    {
            //        Id = 3,
            //        FirstName = "Douglas",
            //        LastName = "Roberts",
            //        AnnualSalary = 40_000.2m,
            //        IsManager = false,
            //        //DepartmentId = 2 //note: somehow, changed to 1 in-between 10:41:03 and 10:42:18
            //        DepartmentId = 1
            //    },
            //    new Employee
            //    {
            //        Id = 5,
            //        FirstName = "Craig",
            //        LastName = "Laurence",
            //        AnnualSalary = 20_000.2m,
            //        IsManager = false,
            //        DepartmentId = 1
            //    },
            //    new Employee
            //    {
            //        Id = 6,
            //        FirstName = "Elisabeth",
            //        LastName = "Tate",
            //        AnnualSalary = 85_000,
            //        IsManager = true,
            //        DepartmentId = 1
            //    }
            //];

            //var results = employees.Except(employeesList); //fail: 1,2,3,4
            //Console.WriteLine($"employees.Except(employeesList):");
            //var results = employees.Except(employeesList, new EmployeeComparer()); //correct: 2,4
            //Console.WriteLine($"employees.Except(employeesList, new EmployeeComparer()):");
            //foreach (var item in results)
            //    Console.WriteLine($"\t{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");

            //Intersect
            //var results = employees.Intersect(employeesList); //invalid - no results because the default comparer is used, which compares object references, not values
            //var results = employees.Intersect(employeesList, new EmployeeComparer()); //1,3
            //Console.WriteLine($"employees.Intersect(employeesList, new EmployeeComparer()):");
            //foreach (var item in results)
            //    Console.WriteLine($"\t{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");

            //Union
            //var results = employees.Union(employeesList); //1,2,3,4,1,3,5,6
            //Console.WriteLine($"employees.Union(employeesList):");
            //var results = employees.Union(employeesList, new EmployeeComparer()); //1,2,3,4,5,6 (distinct by Id, FirstName, LastName (see EmployeeComparer))
            //Console.WriteLine($"employees.Union(employeesList, new EmployeeComparer()):");
            //foreach (var item in results)
            //    Console.WriteLine($"\t{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");


            //Partitioning Operators - Skip, SkipWhile, Take, TakeWhile
            //Skip
            //var results = employees.Skip(2); //skip first 2 employees
            //Console.WriteLine($"employees.Skip(2):");
            //foreach (var item in results)
            //    Console.WriteLine($"\t{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");

            //SkipWhile
            //employees.Add(
            //    new Employee
            //    {
            //        Id = 7,
            //        FirstName = "Sam",
            //        LastName = "Davis",
            //        AnnualSalary = 100_000m,
            //        IsManager = false,
            //        DepartmentId = 1
            //    });
            //var results = employees.SkipWhile(e => e.AnnualSalary > 50_000); //skip employees with AnnualSalary > 50,000
            //Console.WriteLine($"employees.SkipWhile(e => e.AnnualSalary > 50_000): ");
            //foreach (var item in results)
            //    Console.WriteLine($"\t{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");
            //
            //TUTORIAL ERROR: his explanation and data in the vid is nonsense because he, inadvertently, goes back the Data.GetEmployees() list even though it shows him using the new employeesList.
            //
            //note: once the SkipWhile's condition hits false, all the subsequent rows are returned

            //Take
            //var results = employees.Take(2); //take first 2 employees
            //Console.WriteLine($"employees.Take(2):");
            //foreach (var item in results)
            //    Console.WriteLine($"\t{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");

            //TakeWhile
            //var results = employees.TakeWhile(e => e.AnnualSalary > 50_000); //take employees with AnnualSalary > 50,000
            //Console.WriteLine($"employees.TakeWhile(e => e.AnnualSalary > 50_000):");
            //foreach (var item in results)
            //    Console.WriteLine($"\t{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");
            //note: once the TakeWhile's condition hits false, all the subsequent rows are ignored and not returned, even if they meet the condition


            //Conversion Operators - ToArray, ToList, ToDictionary //, OfType, Cast
            //ToList
            //List<Employee> results = (from emp in employees
            //                              where emp.AnnualSalary > 50_000
            //                              select emp).ToList(); //<--ToList forces immediate execution of the query
            //foreach (var item in results)
            //    Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");

            //ToDictionary
            // Filter employees and create a dictionary.
            // Key   = employee ID (int)
            // Value = employee object (Employee)
            //Dictionary<int, Employee> resultsDict = (from emp in employees
            //                                         where emp.AnnualSalary > 50_000
            //                                         //select emp).ToDictionary(e => e.Id, e => e); //<--ToDictionary forces immediate execution of the query
            //                                         select emp).ToDictionary(e => e.Id); //<--ToDictionary forces immediate execution of the query
            //foreach (var key in resultsDict.Keys)
            //    Console.WriteLine($"Key: {key}, Value: {resultsDict[key].FirstName,-10} {resultsDict[key].LastName,-10} {resultsDict[key].AnnualSalary,10:C}");

            //ToArray
            //Employee[] resultsArray = (from emp in employees
            //                           where emp.AnnualSalary > 50_000
            //                           select emp).ToArray(); //<--ToArray forces immediate execution of the query
            //foreach (var item in resultsArray)
            //    Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");


            //Clauses - Let, Into
            //Let
            //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/let-clause
            //var results = from emp in employees
            //              let Initials = emp.FirstName.Substring(0, 1).ToUpper() + emp.LastName.Substring(0, 1).ToUpper()
            //              let AnnualSalaryPlusBonus = emp.AnnualSalary + (emp.AnnualSalary * (emp.IsManager ? 0.04m : 0.02m))
            //              //where Initials == "JS" || Initials == "SJ" && AnnualSalaryPlusBonus > 50_000 <--- vid incorrectly shows this, but it is wrong because the && has higher precedence than the ||, so it will return employees with Initials == "JS" OR employees with Initials == "SJ" AND AnnualSalaryPlusBonus > 50_000, which is not what i want
            //              where (Initials == "JS" || Initials == "SJ") && (AnnualSalaryPlusBonus > 50_000)
            //              select new
            //              {
            //                  Initials,
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalaryPlusBonus
            //              };

            //foreach (var item in results)
            //    Console.WriteLine($"{item.Initials,-5} {item.FullName,-20} {item.AnnualSalaryPlusBonus,10:C}");

            //Into
            //var results = from emp in employees
            //              where emp.AnnualSalary > 50_000
            //              select emp
            //              into highearners
            //              where highearners.IsManager == true
            //              select highearners;

            //foreach (var item in results)
            //    Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10} {item.AnnualSalary,10:C}");


            //Projection Operators - Select, SelectMany
            //https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/projection-operations
            //Select
            var results = departments.Select(d => d.Employees); //returns IEnumerable<IEnumerable<Employee>>

            foreach (var item in results)
            {
                Console.WriteLine($"Department: {departments.First(d => d.Employees == item).ShortName}");
                foreach (var emp in item)
                    Console.WriteLine($"\t{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10} {emp.AnnualSalary,10:C}");
            }

            //chatgpt
            //foreach (var dept in departments)
            //{
            //    Console.WriteLine($"Department: {dept.ShortName}");

            //    foreach (var emp in dept.Employees)
            //        Console.WriteLine($"\t{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10} {emp.AnnualSalary,10:C}");
            //}
            Console.WriteLine();
            //SelectMany
            //var resultsMany = departments.SelectMany(d => d.Employees); //returns IEnumerable<Employee>
            var resultsMany = departments.SelectMany(d => d.Employees).OrderBy(o => o.Id); //returns IEnumerable<Employee>
            foreach (var emp in resultsMany)
                Console.WriteLine($"\t{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10} {emp.AnnualSalary,10:C}");

            //Entity Framework documentation hub: https://learn.microsoft.com/en-us/ef/
            //https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/expression-trees/
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
                return obj.Id.GetHashCode(); //https://learn.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=net-10.0
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
            public IEnumerable<Employee> Employees { get; set; } //added 12:11:48
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
                    AnnualSalary = 60_000.3m,
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
                    AnnualSalary = 80_000.1m,
                    IsManager = true,
                    DepartmentId = 3
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 3,
                    FirstName = "Douglas",
                    LastName = "Roberts",
                    AnnualSalary = 40_000.2m,
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
                    AnnualSalary = 30_000.2m,
                    IsManager = false,
                    DepartmentId = 3
                };
                employees.Add(employee);

                return employees;
            }

            //public static List<Department> GetDepartments()
            public static List<Department> GetDepartments(IEnumerable<Employee> employees) //amended 12:11:48
            {
                List<Department> departments = [];

                Department department = new Department
                {
                    Id = 1,
                    ShortName = "HR",
                    LongName = "Human Resources",
                    Employees = from emp in employees
                                where emp.DepartmentId == 1
                                select emp
                };
                departments.Add(department);
                department = new Department
                {
                    Id = 2,
                    ShortName = "FN",
                    LongName = "Finance",
                    Employees = from emp in employees
                                where emp.DepartmentId == 2
                                select emp
                };
                departments.Add(department);
                department = new Department
                {
                    Id = 3,
                    ShortName = "TE",
                    LongName = "Technology",
                    Employees = from emp in employees
                                where emp.DepartmentId == 3
                                select emp
                };
                departments.Add(department);

                return departments;
            }
        }
    }
}