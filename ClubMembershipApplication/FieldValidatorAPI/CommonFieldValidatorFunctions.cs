namespace FieldValidatorAPI
{
    public delegate bool RequiredValidDel(string fieldVal);
    public delegate bool StringLenValidDel(string fieldVal, int min, int max);
    public delegate bool DateValidDel(string fieldVal, out DateTime validDateTime);
    public delegate bool PatternMatchValidDel(string fieldVal, string pattern);
    public delegate bool CompareFieldsValidDel(string fieldVal, string fieldValCompare);

    public class CommonFieldValidatorFunctions //20260619 Part 5 - Delegates - Create a Code Example
    {
        private static RequiredValidDel _requiredValidDel = null;
        private static StringLenValidDel _stringLenValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchValidDel _patternMatchValidDel = null;
        private static CompareFieldsValidDel _compareFieldsValidDel = null;



        public static RequiredValidDel RequiredValidDel
        {
            get
            {
                if (_requiredValidDel == null)
                    _requiredValidDel = new RequiredValidDel(RequiredValid);

                return _requiredValidDel;
            }
        }

        public static StringLenValidDel StringLenValidDel
        {
            get
            {
                if (_stringLenValidDel == null)
                    _stringLenValidDel = new StringLenValidDel(StringLenValid);

                return _stringLenValidDel;
            }
        }

        public static DateValidDel DateValidDel
        {
            get
            {
                if (_dateValidDel == null)
                    _dateValidDel = new DateValidDel(DateValid);

                return _dateValidDel;
            }
        }

        public static PatternMatchValidDel PatternMatchValidDel
        {
            get
            {
                if (_patternMatchValidDel == null)
                    _patternMatchValidDel = new PatternMatchValidDel(PatternMatch);

                return _patternMatchValidDel;
            }
        }

        public static CompareFieldsValidDel CompareFieldsValidDel
        {
            get
            {
                if (_compareFieldsValidDel == null)
                    _compareFieldsValidDel = new CompareFieldsValidDel(CompareFieldsValid);

                return _compareFieldsValidDel;
            }
        }



        private static bool RequiredValid(string fieldVal)
        {
            return !string.IsNullOrWhiteSpace(fieldVal);
        }

        private static bool StringLenValid(string fieldVal, int min, int max)
        {
            return fieldVal.Length >= min && fieldVal.Length <= max;
        }

        private static bool DateValid(string fieldVal, out DateTime validDateTime)
        {
            return DateTime.TryParse(fieldVal, out validDateTime);
        }

        private static bool PatternMatch(string fieldVal, string pattern)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(fieldVal, pattern);
        }

        private static bool CompareFieldsValid(string fieldVal, string fieldValCompare)
        {
            return fieldVal == fieldValCompare;
        }
    }
}
