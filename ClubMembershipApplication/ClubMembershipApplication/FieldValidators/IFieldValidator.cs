using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication.FieldValidators
{
    public delegate bool FieldValidatorDel(int fieldIndex, string fieldValue, string[] fieldArray, out string errMsg);
    public interface IFieldValidator //20260621 Part 5 - Delegates - Create a Code Example
    {
        void InitialiseValidatorDelgates();
        string[] FieldArray { get; set; }
        FieldValidatorDel ValidatorDel { get; }
    }
}
