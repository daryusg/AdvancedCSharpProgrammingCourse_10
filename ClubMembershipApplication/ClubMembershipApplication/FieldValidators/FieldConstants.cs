using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.FieldValidators
{
    public class FieldConstants //20260621 Part 5 - Delegates - Create a Code Example
    {
        public enum UserRegistrationField
        {
            Email,
            FirstName,
            LastName,
            Password,
            PasswordConfirmation,
            DateOfBirth,
            PhoneNumber,
            Address1,
            Address2,
            City,
            PostCode
        }
    }
}
