using ClubMembershipApplication.Models;

namespace ClubMembershipApplication.Data
{
    public interface ILogin //20260621 Part 5 - Delegates - Create a Code Example
    {
        User Login(string emailAddress, string password);
    }
}
