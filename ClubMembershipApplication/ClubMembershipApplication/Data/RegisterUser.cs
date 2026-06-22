using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Data  //20260621 Part 5 - Delegates - Create a Code Example
{
    public class RegisterUser : IRegister
    {
        public bool EmailExists(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public bool Register(string[] fields)
        {
            using (var dbContext = new ClubMembershipDbContext())
            {
            }
            return true;
        }
    }
}
