using ClubMembershipApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Data
{
    public class LoginUser : ILogin //20260623 Part 5 - Delegates - Create a Code Example
    {
        public User Login(string emailAddress, string password)
        {
            User user = null;
            using (var dbContext = new ClubMembershipDbContext())
            {
                user = dbContext.Users.FirstOrDefault(u => u.Email.ToLower().Trim() == emailAddress.ToLower().Trim() && u.Password == password);
            }

            return user;
        }
    }
}
