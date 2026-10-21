using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationComponent.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
    public class RegularExpressionAttribute : Attribute
    {
        public string Pattern { get; set; }
        public string ErrorMessage { get; set; }

        public RegularExpressionAttribute(string pattern)
        {
            Pattern = pattern;
            ErrorMessage = "The field {0} does not match the required format (regex: {1})."; //NO $ = composite string formatting. $ = string interpolation
        }

        public RegularExpressionAttribute(string pattern, string errMsg)
        {
            Pattern = pattern;
            ErrorMessage = errMsg;
        }
    }
}
