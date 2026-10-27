namespace UtilityFunctions
{
    [Information(Description = "This class contains basic utility methods")]
    public class BasicUtilityFunctions
    {
        [Information(Description = "This method returns a welcome message")]
        public string WriteWelcomeMessage()
        {
            return "Welcome to the 'BasicUtilityFunctions' class";
        }

        [Information(Description = "This method returns the sum of 2 integer operands")]
        public int IntegerPlusInteger(int operand1, int operand2)
        {
            return operand1 + operand2;
        }

        [Information(Description = "This method returns the concatenation of 3 strings in the same order that they appear in the parameter list from left to right")]
        public string Concat3Strings(string str1, string str2, string str3)
        {
            return String.Concat(str1, " ", str2, " ", str3);
        }

        [Information(Description = "This method returns the character length of a string argument")]
        public int GetStringLength(string str)
        {
            return str.Length;
        }
    }
}
