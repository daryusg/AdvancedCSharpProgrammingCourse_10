using System;
using System.Collections.Generic;
using System.Text;

namespace HRAdministrationAPI
{
    public interface IEmployee //20260619 Part 2 - Overview of the Advanced C# Course
    {
        int EmployeeId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        decimal Salary { get; set; }
    }
}
