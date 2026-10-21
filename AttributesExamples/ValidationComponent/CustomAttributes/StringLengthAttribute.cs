using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationComponent.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class StringLengthAttribute : Attribute
    {
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public string ErrorMessage { get; set; }

        public StringLengthAttribute(int maxLength)
        {
            SetProperties(maxLength);
        }

        public StringLengthAttribute(int maxLength, string errMsg)
        {
            SetProperties(maxLength, errMsg);
        }

        public StringLengthAttribute(int maxLength, int minLength)
        {
            SetProperties(maxLength, minLength:minLength);
        }

        public StringLengthAttribute(int maxLength, string errMsg, int minLength)
        {
            SetProperties(maxLength, errMsg, minLength);
        }

        //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types
        private void SetProperties(int maxLength, string errMsg = "", int? minLength = null)
        {
            if (errMsg == "") //set default value for ErrorMessage property
            {
                if (minLength == null)
                    ErrorMessage = "Field {0} must be less than or equal to {1} in length.";
                else
                    ErrorMessage = "Field {0} must be between {2} and {1} characters in length.";
            }
            else
                ErrorMessage = errMsg;

            MaxLength = maxLength;
            MinLength = minLength ?? 0;
            //or
            //MinLength = minLength.HasValue ? minLength.Value : 0;
        }
    }
}
    
