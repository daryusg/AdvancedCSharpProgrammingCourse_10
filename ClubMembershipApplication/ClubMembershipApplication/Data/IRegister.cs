using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Data
{
    public interface IRegister //20260621 Part 5 - Delegates - Create a Code Example
    {
        bool Register(string[] fields);
        bool EmailExists(string emailAddress);
    }
}
