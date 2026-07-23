using System.Linq;

namespace FuncActionPredicateExamples
{
    class Program //20260710 Part 7 - Delegates - Fund, Action and Predicate
    {
        //20260723
        //****************
        //***   Func   ***
        //****************
        delegate TResult Func2<out TResult>();
        delegate TResult Func2<in T1, out TResult>(T1 arg);
        delegate TResult Func2<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
        delegate TResult Func2<in T1, in T2, in T3, out TResult>(T1 arg1, T2 arg2, T3 arg3);

        static void Main(string[] args)
        {
            MathClasss mathClass = new MathClasss();
            //Func<int, int, int> calc = mathClass.Sum;
            //Func<int, int, int> calc = delegate (int a, int b) { return a + b; };
            //Func<int, int, int> calc = (int a, int b) => { return a + b; };
            //Func<int, int, int> calc = (a, b) => a + b; //=> lambda expression is a shortcut for an anonymous method. introduced (alongside linq) in c# v3
            Func2<int, int, int> calc = (a, b) => a + b;
            //https://learn.microsoft.com/en-us/dotnet/csharp/linq/
            //int result = calc(1, 2);
            //Console.WriteLine($"Result: {result}");

            //float d = 2.3f, e = 4.56f;
            //int f = 5;
            //Func<float, float, int, float> calc2 = (x, y, z) => (x + y) * z;
            //float result2 = calc2(d, e, f);
            //Console.WriteLine($"Result: {result2}");

            Func<decimal, decimal, decimal> calculateTotalAnnualSalary = (annualSalary, bonusPercentage) => annualSalary + (annualSalary * (bonusPercentage / 100));
            //Console.WriteLine($"Total Annual Salary: {calculateTotalAnnualSalary(60000, 2)}"); // Example usage

            //******************
            //***   Action   ***
            //******************
            Action<int, string, string, decimal, char, bool> displayEmployeeDetails = (id, firstName, lastName, salary, gender, manager) =>
            {
                Console.WriteLine($"Id: {id}");
                Console.WriteLine($"First Name: {firstName}");
                Console.WriteLine($"Last Name: {lastName}");
                Console.WriteLine($"Annual salary: {salary}");
                Console.WriteLine($"Gender: {gender}");
                Console.WriteLine($"Manager: {manager}");
            };
            //printEmployeeDetails(1, "John Doe", "Engineering", 75000m, 'm', true);
            //printEmployeeDetails(2, "Jane Smith", "Marketing", 80000m, 'f', false);

            //*********************
            //***   Predicate   ***
            //*********************
            //List<Employee> employees = new List<Employee>;
            //employees.Add(new Employee { Id = 1, FirstName = "John", LastName = "Doe", AnnualSalary = 60000m, Gender = 'm', IsManager = true });
            //employees.Add(new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", AnnualSalary = 80000m, Gender = 'f', IsManager = false });
            //employees.Add(new Employee { Id = 3, FirstName = "Alice", LastName = "Johnson", AnnualSalary = 75000m, Gender = 'f', IsManager = true });
            //employees.Add(new Employee { Id = 4, FirstName = "Bob", LastName = "Brown", AnnualSalary = 55000m, Gender = 'm', IsManager = false });
            List<Employee> employees = new List<Employee>
            {
                //new Employee { Id = 1, FirstName = "John", LastName = "Doe", AnnualSalary = 60000m, Gender = 'm', IsManager = true },
                //new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", AnnualSalary = 35000m, Gender = 'f', IsManager = false },
                //new Employee { Id = 3, FirstName = "Alice", LastName = "Johnson", AnnualSalary = 75000m, Gender = 'f', IsManager = true },
                //new Employee { Id = 4, FirstName = "Bob", LastName = "Brown", AnnualSalary = 30000m, Gender = 'm', IsManager = false }
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", AnnualSalary = calculateTotalAnnualSalary(60000m, 2), Gender = 'm', IsManager = true },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", AnnualSalary = calculateTotalAnnualSalary(35000m, 2), Gender = 'f', IsManager = false },
                new Employee { Id = 3, FirstName = "Alice", LastName = "Johnson", AnnualSalary = calculateTotalAnnualSalary(75000m, 2), Gender = 'f', IsManager = true },
                new Employee { Id = 4, FirstName = "Bob", LastName = "Brown", AnnualSalary = calculateTotalAnnualSalary(30000m, 2), Gender = 'm', IsManager = false }
            };

            //set up and display header
            string heading = "All Employees";
            Console.WriteLine(heading);
            Console.WriteLine(new string('=', heading.Length));
            //display employees
            foreach (var employee in employees)
                displayEmployeeDetails(employee.Id, employee.FirstName, employee.LastName, employee.AnnualSalary, employee.Gender, employee.IsManager);

            //set up and display header
            Console.WriteLine(); //empty line
            heading = "Filtered Employees (Male):";
            Console.WriteLine(heading);
            Console.WriteLine(new string('=', heading.Length));
            //display employees
            List<Employee> employeesFiltered = FilterEmployees(employees, e => e.Gender == 'm');
            foreach (var employee in employeesFiltered)
                displayEmployeeDetails(employee.Id, employee.FirstName, employee.LastName, employee.AnnualSalary, employee.Gender, employee.IsManager);

            //set up and display header
            Console.WriteLine(); //empty line
            heading = "Filtered Employees (Annual Salary < $45,000):";
            Console.WriteLine(heading);
            Console.WriteLine(new string('=', heading.Length));
            //display employees
            employeesFiltered = FilterEmployees(employees, e => e.AnnualSalary < 45000m);
            foreach (var employee in employeesFiltered)
                displayEmployeeDetails(employee.Id, employee.FirstName, employee.LastName, employee.AnnualSalary, employee.Gender, employee.IsManager);

            //set up and display header
            Console.WriteLine(); //empty line
            heading = "Filtered Employees (Managers):";
            Console.WriteLine(heading);
            Console.WriteLine(new string('=', heading.Length));
            //display employees
            employeesFiltered = FilterEmployees(employees, e => e.IsManager == true);
            foreach (var employee in employeesFiltered)
                displayEmployeeDetails(employee.Id, employee.FirstName, employee.LastName, employee.AnnualSalary, employee.Gender, employee.IsManager);

            //set up and display header
            Console.WriteLine(); //empty line
            heading = "Filtered Employees (Non-managers):";
            Console.WriteLine(heading);
            Console.WriteLine(new string('=', heading.Length));
            //display employees
            employeesFiltered = employees.FilterEmployees(e => e.IsManager == false);
            foreach (var employee in employeesFiltered)
                displayEmployeeDetails(employee.Id, employee.FirstName, employee.LastName, employee.AnnualSalary, employee.Gender, employee.IsManager);

            //set up and display header
            Console.WriteLine(); //empty line
            heading = "Filtered Employees (Annual Salary > $70,000):";
            Console.WriteLine(heading);
            Console.WriteLine(new string('=', heading.Length));
            //display employees
            employeesFiltered = employees.Where(e => e.AnnualSalary > 70000m).ToList();
            foreach (var employee in employeesFiltered)
                displayEmployeeDetails(employee.Id, employee.FirstName, employee.LastName, employee.AnnualSalary, employee.Gender, employee.IsManager);



            Console.ReadKey();
        }

        static List<Employee> FilterEmployees(List<Employee> employees, Predicate<Employee> predicate)
        {
            List<Employee> filteredEmployees = new List<Employee>();
            foreach (var employee in employees)
            {
                if (predicate(employee))
                {
                    filteredEmployees.Add(employee);
                }
            }
            return filteredEmployees;
        }
    }

    public static class Extensions
    {
        //public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        //{
        //    foreach (var item in source)
        //    {
        //        action(item);
        //    }
        //}
        public static List<Employee> FilterEmployees(this List<Employee> employees, Predicate<Employee> predicate)
        {
            List<Employee> filteredEmployees = new List<Employee>();
            foreach (var employee in employees)
            {
                if (predicate(employee))
                {
                    filteredEmployees.Add(employee);
                }
            }
            return filteredEmployees;
        }
    }

    public class  Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; } = 0;
        public char Gender { get; set; }
        public bool IsManager { get; set; }
    }
    public class MathClasss
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }
    }
}
