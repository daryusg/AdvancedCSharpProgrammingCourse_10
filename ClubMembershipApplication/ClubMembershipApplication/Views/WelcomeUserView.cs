using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Views
{
    public class WelcomeUserView : IView //20260623 Part 5 - Delegates - Create a Code Example
    {
        private readonly User _user;

        public WelcomeUserView(User user)
        {
            this._user = user;
        }

        public IFieldValidator FieldValidator => null;

        public void RunView()
        {
            Console.Clear();
            CommonOutputText.DisplayMainHeading();

            CommonOutputFormat.ChangeFontColour(FontTheme.Success);
            Console.WriteLine($"Hi {_user.FirstName}!!{Environment.NewLine}Welcome to the Cycling Club!!");
            CommonOutputFormat.ChangeFontColour(FontTheme.Default);
            Console.ReadKey();
        }
    }
}
