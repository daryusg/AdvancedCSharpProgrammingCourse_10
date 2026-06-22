using FieldValidatorAPI;

namespace ClubMembershipApplication.FieldValidators
{
    public class UserRegistrationValidator : IFieldValidator //20260621 Part 5 - Delegates - Create a Code Example
    {
        const int FirstName_Min_Len = 2;
        const int FirstName_Max_Len = 50;
        const int LastName_Min_Len = 2;
        const int LastName_Max_Len = 50;

        delegate bool EmailExistsDel(string emailAddress);

        FieldValidatorDel _fieldValidatorDel = null;

        RequiredValidDel _requiredValidDel = null;
        StringLenValidDel _stringLenValidDel = null;
        DateValidDel _dateValidDel = null;
        PatternMatchValidDel _patternMatchValidDel = null;
        CompareFieldsValidDel _compareFieldsValidDel = null;

        EmailExistsDel _emailExistsDel = null;

        string[] _fieldArray = null;

        public string[] FieldArray
        {
            get
            {
                if (_fieldArray == null)
                    _fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserRegistrationField)).Length];

                return _fieldArray;
            }
            set { _fieldArray = value; }
        }

        public FieldValidatorDel ValidatorDel => _fieldValidatorDel;

        public void InitialiseValidatorDelgates()
        {
            _fieldValidatorDel = new FieldValidatorDel(ValidField);

            _requiredValidDel = CommonFieldValidatorFunctions.RequiredValidDel;
            _stringLenValidDel = CommonFieldValidatorFunctions.StringLenValidDel;
            _dateValidDel = CommonFieldValidatorFunctions.DateValidDel;
            _patternMatchValidDel = CommonFieldValidatorFunctions.PatternMatchValidDel;
            _compareFieldsValidDel = CommonFieldValidatorFunctions.CompareFieldsValidDel;
        }

        private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string errMsg)
        {
            errMsg = string.Empty;

            FieldConstants.UserRegistrationField userRegistrationField = (FieldConstants.UserRegistrationField)fieldIndex;
            switch(userRegistrationField)
            {
                case FieldConstants.UserRegistrationField.Email:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    //errMsg += (!_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Email_Address_RegEx_Pattern)) ? $"You must enter a valid email address for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    errMsg += (!_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Email_Address_RegEx_Pattern)) ? $"You must enter a valid email address{Environment.NewLine}" : string.Empty;
                    break;
                case FieldConstants.UserRegistrationField.FirstName:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    errMsg += (!_stringLenValidDel(fieldValue, FirstName_Min_Len, FirstName_Max_Len)) ? $"Field length ({Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}) must be between {FirstName_Min_Len} and {FirstName_Max_Len} characters long{Environment.NewLine}" : string.Empty;
                    break;
                case FieldConstants.UserRegistrationField.LastName:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    errMsg += (!_stringLenValidDel(fieldValue, LastName_Min_Len, LastName_Max_Len)) ? $"Field length ({Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}) must be between {LastName_Min_Len} and {LastName_Max_Len} characters long{Environment.NewLine}" : string.Empty;
                    break;

                case FieldConstants.UserRegistrationField.Password:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    errMsg += (!_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Strong_Password_RegEx_Pattern)) ? $"Password must contain at 1 lowercase letter, 1 uppercase letter, 1 special character and be 6 - 10 characters long{Environment.NewLine}" : string.Empty;
                    break;

                case FieldConstants.UserRegistrationField.PasswordConfirmation:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    errMsg += (!_compareFieldsValidDel(fieldValue, fieldArray[(int)FieldConstants.UserRegistrationField.Password])) ? $"The password confirmation does not match the password for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    break;

                case FieldConstants.UserRegistrationField.DateOfBirth:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    errMsg += (!_dateValidDel(fieldValue, out DateTime validDateTime)) ? $"You must enter a valid date:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty;
                    break;

                case FieldConstants.UserRegistrationField.PhoneNumber:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    errMsg += (!_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Uk_PhoneNumber_RegEx_Pattern)) ? $"You must enter a valid UK phone number{ Environment.NewLine}" : string.Empty;
                    break;

                case FieldConstants.UserRegistrationField.Address1:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    break;

                case FieldConstants.UserRegistrationField.Address2:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    break;

                case FieldConstants.UserRegistrationField.City:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    break;

                case FieldConstants.UserRegistrationField.PostCode:
                    errMsg = (!_requiredValidDel(fieldValue) ? $"You must enter a value for field:{Enum.GetName(typeof(FieldConstants.UserRegistrationField), userRegistrationField)}{Environment.NewLine}" : string.Empty);
                    errMsg += (!_patternMatchValidDel(fieldValue, CommonRegularExpressionValidationPatterns.Uk_Post_Code_RegEx_Pattern)) ? $"You must enter a valid UK postcode{Environment.NewLine}" : string.Empty;
                    break;
                // Add additional cases for other fields as needed
                default:
                    throw new ArgumentException($"Invalid userRegistrationField: {userRegistrationField}");
            }

            return (errMsg == string.Empty);
        }
    }
}
