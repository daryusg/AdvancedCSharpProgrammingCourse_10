using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityFunctions
{
    [Information(Description = "This class contains basic math functionality")]
    internal class BasicMathFunctions
    {
        [Information(Description = "This method returns the result of the first operand (num1) divided by the second operand (num2)")]
        public double DivideOperation(double num1, double num2)
        {
            return num1 / num2;
        }

        [Information(Description = "This method returns the result of the first operand multiplied by the second operand")]
        public double MultiplyOperation(double num1, double num2)
        {
            return num1 * num2;
        }
    }
}
