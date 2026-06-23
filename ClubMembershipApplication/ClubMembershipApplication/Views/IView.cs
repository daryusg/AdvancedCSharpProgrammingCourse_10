using ClubMembershipApplication.FieldValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.Views
{
    public interface IView //20260623 Part 5 - Delegates - Create a Code Example
    {
        void RunView();
        IFieldValidator FieldValidator { get; }
    }
}
