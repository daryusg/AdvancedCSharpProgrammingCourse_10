namespace LINQExamples_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = Data.GetEmployees();
            List<Department> departments = Data.GetDepartments();

            //Select and Where Operators - Method Syntax
            //var results = employees.Select(e => new
            //{
            //    FullName = e.FirstName + " " + e.LastName,
            //    AnnualSalary = e.AnnualSalary,
            //    DepartmentName = departments.FirstOrDefault(d => d.Id == e.DepartmentId)?.LongName
            //}
            //).Where(e => e.AnnualSalary >= 50000);

            //Select and Where Operators - Query Syntax
            //var results = from emp in employees
            //              where emp.AnnualSalary >= 50000
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary,
            //              };

            //employees.Add(new Employee
            //{
            //    Id = 5,
            //    FirstName = "Alice",
            //    LastName = "Johnson",
            //    AnnualSalary = 75000.20m,
            //    IsManager = false,
            //});

            ////Deferred Execution Example (lazy evaluation)
            //var results = from emp in employees.GetHighSalariedEmployees()
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary,
            //              };

            //employees.Add(new Employee
            //{
            //    Id = 5,
            //    FirstName = "Sam",
            //    LastName = "Davis",
            //    AnnualSalary = 100000.20m,
            //    IsManager = true,
            //    DepartmentId = 2
            //});

            ////Immediate Execution Example
            //var results = (from emp in employees.GetHighSalariedEmployees()
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary,
            //              }).ToList(); //<--- .ToList() causes query to run immediately. note: query syntax doesn't support 2 operators in a single statement, so we have to use method syntax for the ToList() operator

            //employees.Add(new Employee
            //{
            //    Id = 5,
            //    FirstName = "Sam",
            //    LastName = "Davis",
            //    AnnualSalary = 100000.20m,
            //    IsManager = true,
            //    DepartmentId = 2
            //});

            //Join Operation Example - Method Syntax
            //var results = departments.Join(employees,
            //        dept => dept.Id,
            //        emp => emp.DepartmentId,
            //        (dept, emp) => new
            //        {
            //            FullName = emp.FirstName + " " + emp.LastName,
            //            AnnualSalary = emp.AnnualSalary,
            //            DepartmentName = dept.LongName
            //        }
            //    );

            //Join Operation Example - Query Syntax
            //var results = from dept in departments
            //              join emp in employees on dept.Id equals emp.DepartmentId
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary,
            //                  DepartmentName = dept.LongName
            //              };
            ////TRANSACT SQL Equivalent:
            ////SELECT CONCAT(emp.FirstName, ' ', emp.LastName) AS FullName, emp.AnnualSalary, dept.LongName AS DepartmentName
            ////FROM Employees emp INNER JOIN Departments dept ON (emp.DepartmentId = dept.Id)

            //GroupJoin Operator Example - Method Syntax
            //var results = departments.GroupJoin(employees,
            //    dept => dept.Id,
            //    emp => emp.DepartmentId,
            //    (dept, emps) => new
            //    {
            //        DepartmentName = dept.LongName,
            //        Employees = emps
            //    });

            //GroupJoin Operator Example - Query Syntax
            var results = from dept in departments
                          join emp in employees on dept.Id equals emp.DepartmentId into empGroup
                          select new
                          {
                              DepartmentName = dept.LongName,
                              Employees = empGroup
                          };
            //TRANSACT SQL Equivalent:
            //SELECT CONCAT(emp.FirstName, ' ', emp.LastName) AS FullName, emp.AnnualSalary, dept.LongName AS DepartmentName
            //FROM Employees emp LEFT OUTER JOIN Departments dept ON (emp.DepartmentId = dept.Id)

            Console.WriteLine($"  {"FirstName",-20} {"LastName",-20} {"Annual Salary",20}");
            foreach (var item in results)
            {
                Console.WriteLine($"Department Name: {item.DepartmentName}");
                foreach (var emp in item.Employees)
                {
                    Console.WriteLine($"  {emp.FirstName,-20} {emp.LastName,-20} {emp.AnnualSalary,20:C}");
                }
            }

            //Console.WriteLine($"{"Full Name",-20} {"Annual Salary",20}    {"Department",-20}");
            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,20:C}    {item.DepartmentName,-20}");
            //}
        }

        //note: i had to move the following to its own class as got error: Extension methods must be defined in a top level static class; EnumerableCollectionExtensionMethods is a nested class
        //public static class EnumerableCollectionExtensionMethods
        //{
        //    public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees)
        //    {
        //        foreach (Employee emp in employees)
        //        {
        //            Console.WriteLine($"Accessing employee: {emp.FirstName} {emp.LastName}");
        //            if (emp.AnnualSalary > 50000)
        //                yield return emp;
        //        }
        //    }
        //}



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
                List<Employee> employees = new List<Employee>();

                Employee employee = new Employee
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Jones",
                    AnnualSalary = 60000.3m,
                    IsManager = true,
                    DepartmentId = 1
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 2,
                    FirstName = "Sarah",
                    LastName = "Jameson",
                    AnnualSalary = 80000.1m,
                    IsManager = true,
                    DepartmentId = 2
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 3,
                    FirstName = "Douglas",
                    LastName = "Roberts",
                    AnnualSalary = 40000.2m,
                    IsManager = false,
                    DepartmentId = 2
                };
                employees.Add(employee);
                employee = new Employee
                {
                    Id = 4,
                    FirstName = "Jane",
                    LastName = "Stevens",
                    AnnualSalary = 30000.2m,
                    IsManager = false,
                    DepartmentId = 2
                };
                employees.Add(employee);

                return employees;
            }

            public static List<Department> GetDepartments()
            {
                List<Department> departments = new List<Department>();

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

        }
    }
}
