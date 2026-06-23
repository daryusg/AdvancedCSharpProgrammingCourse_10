using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Views
{
    public class UserRegistrationView : IView //20260623 Part 5 - Delegates - Create a Code Example
    {
        IFieldValidator _fieldValidator = null;
        IRegister _register = null;

        public UserRegistrationView(IRegister register, IFieldValidator fieldValidator)
        {
            _register = register;
            _fieldValidator = fieldValidator;
        }

        public IFieldValidator FieldValidator { get => _fieldValidator; }

        public void RunView()
        {
            CommonOutputText.DisplayMainHeading();
            CommonOutputText.DisplayRegistrationHeading();
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.Email] = GetInputFromUser(FieldConstants.UserRegistrationField.Email, "Please enter your email address: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.FirstName] = GetInputFromUser(FieldConstants.UserRegistrationField.FirstName, "Please enter your first name: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.LastName] = GetInputFromUser(FieldConstants.UserRegistrationField.LastName, "Please enter your last name: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.Password] = GetInputFromUser(FieldConstants.UserRegistrationField.Password, $"Please enter your password.{Environment.NewLine}(It must contain at least 1 lowercase letter, 1 uppercase letter, 1 special character and be 6 - 10 characters long): ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PasswordConfirmation] = GetInputFromUser(FieldConstants.UserRegistrationField.PasswordConfirmation, "Please re-enter your password: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.DateOfBirth] = GetInputFromUser(FieldConstants.UserRegistrationField.DateOfBirth, "Please enter your date of birth: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PhoneNumber] = GetInputFromUser(FieldConstants.UserRegistrationField.PhoneNumber, "Please enter your phone no.: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.Address1] = GetInputFromUser(FieldConstants.UserRegistrationField.Address1, "Please enter your address (line 1): ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.Address2] = GetInputFromUser(FieldConstants.UserRegistrationField.Address2, "Please enter your address (line 2): ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.City] = GetInputFromUser(FieldConstants.UserRegistrationField.City, "Please enter your city: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PostCode] = GetInputFromUser(FieldConstants.UserRegistrationField.PostCode, "Please enter your postcode: ");

            RegisterUser();
        }

        private void RegisterUser()
        {
            _register.Register(_fieldValidator.FieldArray);

            CommonOutputFormat.ChangeFontColour(FontTheme.Success);
            Console.WriteLine("Successful registration. Press any key to log in");
            CommonOutputFormat.ChangeFontColour(FontTheme.Default);
            Console.ReadKey();
        }

        private string GetInputFromUser(FieldConstants.UserRegistrationField field, string promptText)
        {
            string fieldVal = "";
            do
            {
                //Console.WriteLine(promptText);
                Console.Write(promptText);
                fieldVal = Console.ReadLine() + "";
            }
            while (!FieldValid(field, fieldVal));
            return fieldVal;
        }

        private bool FieldValid(FieldConstants.UserRegistrationField field, string fieldValue)
        {
            //if not valid data then write error message
            if (!_fieldValidator.ValidatorDel((int)field, fieldValue, _fieldValidator.FieldArray, out string invalidMessage))
            {
                CommonOutputFormat.ChangeFontColour(FontTheme.Danger);
                Console.WriteLine(invalidMessage);
                CommonOutputFormat.ChangeFontColour(FontTheme.Default);
                return false;
            }

            return true;
        }
    }
}
