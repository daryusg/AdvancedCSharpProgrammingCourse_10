using HRAdministrationAPI;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace SchoolHRAdministration
{
    public enum EmployeeType
    {
        Teacher,
        HeadOfDepartment,
        DeputyHeadMaster,
        HeadMaster
    }
    class Program //20260619 Part 2 - Overview of the Advanced C# Course
    {
        static void Main(string[] args)
        {
            decimal totalSalaries = 0;
            List<IEmployee> employees = new();

            SeedData(employees);

            //foreach (var employee in employees)
            //{
            //    totalSalaries += employee.Salary;
            //}
            //Console.WriteLine($"Total Annual Salaries (inc. bonus): {totalSalaries}");
            Console.WriteLine($"Total Annual Salaries (inc. bonus): {employees.Sum(e => e.Salary)}");
            Console.ReadKey();
        }

        public static void SeedData(List<IEmployee> employees)
        {
            // Implementation for seeding data
            //IEmployee teacher1 = new Teacher
            //{
            //    EmployeeId = 1,
            //    FirstName = "Bob",
            //    LastName = "Fisher",
            //    Salary = 40000
            //};
            IEmployee teacher1 = EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 1, "Bob", "Fisher", 40000);
            employees.Add(teacher1);

            //IEmployee teacher2 = new Teacher
            //{
            //    EmployeeId = 2,
            //    FirstName = "Alice",
            //    LastName = "Smith",
            //    Salary = 40000
            //};
            IEmployee teacher2 = EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 2, "Alice", "Smith", 40000);
            employees.Add(teacher2);

            //IEmployee headOfDepartment = new HeadOfDepartment
            //{
            //    EmployeeId = 3,
            //    FirstName = "Charlie",
            //    LastName = "Johnson",
            //    Salary = 50000
            //};
            IEmployee headOfDepartment = EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadOfDepartment, 3, "Charlie", "Johnson", 50000);
            employees.Add(headOfDepartment);

            //IEmployee deputyHeadMaster = new DeputyHeadMaster
            //{
            //    EmployeeId = 4,
            //    FirstName = "David",
            //    LastName = "Williams",
            //    Salary = 60000
            //};
            IEmployee deputyHeadMaster = EmployeeFactory.GetEmployeeInstance(EmployeeType.DeputyHeadMaster, 4, "David", "Williams", 60000);
            employees.Add(deputyHeadMaster);

            //IEmployee headMaster = new HeadMaster
            //{
            //    EmployeeId = 5,
            //    FirstName = "Damien",
            //    LastName = "Jones",
            //    Salary = 80000
            //};
            IEmployee headMaster = EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadMaster, 5, "Damien", "Jones", 80000);
            employees.Add(headMaster);
        }
    }

    public class Teacher : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.02m); }
    }

    public class HeadOfDepartment : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.03m); }
    }

    public class DeputyHeadMaster : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.04m); }
    }

    public class HeadMaster : EmployeeBase
    {
        public override decimal Salary { get => base.Salary + (base.Salary * 0.05m); }
    }

    public static class EmployeeFactory
    {
        public static IEmployee GetEmployeeInstance(EmployeeType employeeType, int employeeId, string firstName, string lastName, decimal salary)
        {
            IEmployee employee = null;

            switch (employeeType)
            {
                case EmployeeType.Teacher:
                    employee = FactoryPattern<IEmployee, Teacher>.GetInstance();
                    break;
                case EmployeeType.HeadOfDepartment:
                    employee = FactoryPattern<IEmployee, HeadOfDepartment>.GetInstance();
                    break;
                case EmployeeType.DeputyHeadMaster:
                    employee = FactoryPattern<IEmployee, DeputyHeadMaster>.GetInstance();
                    break;
                case EmployeeType.HeadMaster:
                    employee = FactoryPattern<IEmployee, HeadMaster>.GetInstance();
                    break;
                default:
                    break;

            }

            if (employee != null)
            {
                employee.EmployeeId = employeeId;
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Salary = salary;
            }
            else
            {
                throw new NullReferenceException();
            }

            return employee;
            
        //return employeeType switch
        //{
        //    EmployeeType.Teacher => new Teacher { EmployeeId = employeeId, FirstName = firstName, LastName = lastName, Salary = salary },
        //    EmployeeType.HeadOfDepartment => new HeadOfDepartment { EmployeeId = employeeId, FirstName = firstName, LastName = lastName, Salary = salary },
        //    EmployeeType.DeputyHeadMaster => new DeputyHeadMaster { EmployeeId = employeeId, FirstName = firstName, LastName = lastName, Salary = salary },
        //    EmployeeType.HeadMaster => new HeadMaster { EmployeeId = employeeId, FirstName = firstName, LastName = lastName, Salary = salary },
        //    _ => throw new ArgumentException("Invalid employee type", nameof(employeeType)),
        //};
    }
    }
}