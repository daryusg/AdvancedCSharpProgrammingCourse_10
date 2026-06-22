using System;
using System.Collections.Generic;
using System.Text;

namespace HRAdministrationAPI
{
    public class EmployeeBase : IEmployee //20260619 Part 2 - Overview of the Advanced C# Course
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual decimal Salary { get; set; }
    }
}
