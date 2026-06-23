using System;
using System.Collections.Generic;
using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models; //20260623

namespace ClubMembershipApplication.Data  //20260621 Part 5 - Delegates - Create a Code Example
{
    public class RegisterUser : IRegister
    {
        public bool EmailExists(string emailAddress) //20260623
        {
            bool emailExists = false;
            using (var dbContext = new ClubMembershipDbContext())
            {
                emailExists = dbContext.Users.Any(u => u.Email.ToLower().Trim() == emailAddress.ToLower().Trim());
            }
            return emailExists;
        }

        public bool Register(string[] fields)
        {
            using (var dbContext = new ClubMembershipDbContext())
            {
                User user = new()
                {
                    Email = fields[(int)FieldConstants.UserRegistrationField.Email],
                    FirstName = fields[(int)FieldConstants.UserRegistrationField.FirstName],
                    LastName = fields[(int)FieldConstants.UserRegistrationField.LastName],
                    Password = fields[(int)FieldConstants.UserRegistrationField.Password],
                    DateOfBirth = DateTime.Parse(fields[(int)FieldConstants.UserRegistrationField.DateOfBirth]),
                    PhoneNumber = fields[(int)FieldConstants.UserRegistrationField.PhoneNumber],
                    Address1 = fields[(int)FieldConstants.UserRegistrationField.Address1],
                    Address2 = fields[(int)FieldConstants.UserRegistrationField.Address2],
                    City = fields[(int)FieldConstants.UserRegistrationField.City],
                    PostCode = fields[(int)FieldConstants.UserRegistrationField.PostCode]
                };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            return true;
        }
    }
}
