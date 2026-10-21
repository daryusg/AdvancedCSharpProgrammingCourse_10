using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ValidationComponent
{
    public static class Validation
    {
        public static bool PropertyValueIsValid(Type t, string value, string elementName, out string errorMessage)
        {
            // Implementation for validating property values
            errorMessage = string.Empty;
            //reflection to get the PropertyInfo for the specified elementName in the type t
            PropertyInfo prop = t.GetProperty(elementName);
            //Attribute[] attributes = Attribute.GetCustomAttributes(prop);
            Attribute[] attributes = prop.GetCustomAttributes().ToArray();

            foreach (Attribute attr in attributes)
            {
                switch (attr)
                {
                    case CustomAttributes.RequiredAttribute ra:
                        if (!FieldRequiredIsValid(value))
                        {
                            errorMessage = string.Format(ra.ErrorMessage, prop.Name);
                            return false;
                        }
                        break;

                    case CustomAttributes.StringLengthAttribute sla:
                        if (!FieldStringLengthIsValid(sla, value))
                        {
                            errorMessage = string.Format(sla.ErrorMessage, elementName, sla.MaxLength, sla.MinLength);
                            return false;
                        }
                        break;

                    case CustomAttributes.RegularExpressionAttribute rea:
                        if (!FieldPatternMatchIsValid(rea, value))
                        {
                            errorMessage = string.Format(rea.ErrorMessage, prop.Name, rea.Pattern);
                            return false;
                        }
                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        private static bool FieldRequiredIsValid(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            return true;
        }

        private static bool FieldStringLengthIsValid(CustomAttributes.StringLengthAttribute stringLengthAttribute, string value)
        {
            if (value.Length > stringLengthAttribute.MaxLength || value.Length < stringLengthAttribute.MinLength)
                return false;
            return true;
        }

        private static bool FieldPatternMatchIsValid(CustomAttributes.RegularExpressionAttribute regularExpressionAttribute, string value)
        {
            return (Regex.IsMatch(value, regularExpressionAttribute.Pattern));
        }
    }
}
