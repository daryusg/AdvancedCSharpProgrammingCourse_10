using ClubMembershipApplication.FieldValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Views
{
    public class MainView : IView //20260623 Part 5 - Delegates - Create a Code Example
    {
        private readonly IView _registerView;
        private readonly IView _loginView;

        public MainView(IView registerView, IView loginView)
        {
            this._registerView = registerView;
            this._loginView = loginView;
        }
        public IFieldValidator FieldValidator => null;

        public void RunView()
        {
            CommonOutputText.DisplayMainHeading();
            Console.WriteLine("Press 'l' to login or 'r' to register");

            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.R:
                    RunUserRegistrationView();
                    RunLoginView();
                    break;

                case ConsoleKey.L:
                    RunLoginView();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Goodbye");
                    Console.ReadKey();
                    break;
            }
        }

        private void RunUserRegistrationView()
        {
            _registerView.RunView();
        }

        private void RunLoginView()
        {
            _loginView.RunView();
        }
    }
}
