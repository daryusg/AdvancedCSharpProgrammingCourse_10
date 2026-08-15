using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeComponent
{
    public class Employees
    {
        public ObservableCollection<EmployeeViewModel> GetEmployees()
        {
            var employees = new ObservableCollection<EmployeeViewModel>();
            employees.Add(new EmployeeViewModel { Id = 1, FirstName = "John", LastName = "Doe", Salary = 80000, Gender = 'M', IsManager = true });
            employees.Add(new EmployeeViewModel { Id = 2, FirstName = "Jane", LastName = "Smith", Salary = 40000, Gender = 'F', IsManager = false });
            employees.Add(new EmployeeViewModel { Id = 3, FirstName = "Bob", LastName = "Johnson", Salary = 30000, Gender = 'M', IsManager = false });
            employees.Add(new EmployeeViewModel { Id = 4, FirstName = "Alice", LastName = "Williams", Salary = 55000, Gender = 'F', IsManager = true });
            return employees;
        }
    }
}
