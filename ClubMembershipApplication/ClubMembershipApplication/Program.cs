using ClubMembershipApplication.Views;

namespace ClubMembershipApplication;

class Program //20260621 Part 5 - Delegates - Create a Code Example
{
    private static void Main(string[] args) //20260623 Part 5 - Delegates - Create a Code Example
    {
        IView mainView = Factory.GetMainViewObject();
        mainView.RunView();

        Console.ReadKey();
    }
}