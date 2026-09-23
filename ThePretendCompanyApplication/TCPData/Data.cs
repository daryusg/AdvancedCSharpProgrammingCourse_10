namespace TCPData
{
    public static class Data
    {
        //public static List<Employee> Employees = new List<Employee>
        //{
        //    new Employee { Id = 1, FirstName = "John", LastName = "Doe", AnnualSalary = 40000, IsManager = false, DepartmentId = 1 },
        //    new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", AnnualSalary = 80000, IsManager = true, DepartmentId = 2 },
        //    new Employee { Id = 3, FirstName = "Bob", LastName = "Johnson", AnnualSalary = 45000, IsManager = false, DepartmentId = 1 },
        //    new Employee { Id = 4, FirstName = "Alice", LastName = "Williams", AnnualSalary = 90000, IsManager = true, DepartmentId = 3 },
        //    new Employee { Id = 5, FirstName = "Charlie", LastName = "Brown", AnnualSalary = 50000, IsManager = false, DepartmentId = 2 }
        //};
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee { Id = 1, FirstName = "John", LastName = "Doe", AnnualSalary = 40000, IsManager = false, DepartmentId = 1 },
                new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", AnnualSalary = 80000, IsManager = true, DepartmentId = 2 },
                new Employee { Id = 3, FirstName = "Bob", LastName = "Johnson", AnnualSalary = 45000, IsManager = false, DepartmentId = 1 },
                new Employee { Id = 4, FirstName = "Alice", LastName = "Williams", AnnualSalary = 90000, IsManager = true, DepartmentId = 3 },
                new Employee { Id = 5, FirstName = "Charlie", LastName = "Brown", AnnualSalary = 50000, IsManager = false, DepartmentId = 2 }
            };
            return employees;
        }

        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>
            {
                new Department { Id = 1, ShortName = "HR", LongName = "Human Resources" },
                new Department { Id = 2, ShortName = "IT", LongName = "Information Technology" },
                new Department { Id = 3, ShortName = "FIN", LongName = "Finance" }
            };
            return departments;
        }
    }
}
