using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models;

namespace ClubMembershipApplication.Views
{
    public class UserLoginView : IView //20260623 Part 5 - Delegates - Create a Code Example
    {
        private readonly ILogin _loginUser;

        public IFieldValidator FieldValidator => null; //no validation on the login

        public UserLoginView(ILogin login)
        {
            this._loginUser = login;
        }

        public void RunView()
        {
            CommonOutputText.DisplayMainHeading();
            CommonOutputText.DisplayLoginHeading();

            Console.WriteLine("Please enter your email address");
            string emailAddress = Console.ReadLine() + "";

            Console.WriteLine("Please enter your password");
            string password = Console.ReadLine() + "";

            User user = _loginUser.Login(emailAddress, password);
            if (user != null)
            {
                WelcomeUserView welcomeUserView = new(user);
                welcomeUserView.RunView();
            }
            else
            {
                Console.Clear();
                CommonOutputFormat.ChangeFontColour(FontTheme.Danger);
                Console.WriteLine("Invalid credentials");
                CommonOutputFormat.ChangeFontColour(FontTheme.Default);
            }
        }
    }
}
