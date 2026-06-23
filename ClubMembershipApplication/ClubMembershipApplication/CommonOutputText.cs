using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication
{
    public static class CommonOutputText //20260623 Part 5 - Delegates - Create a Code Example
    {
        private static string MainHeading
        {
            get {
                string heading = "Cycling Club";
                return $"{heading}{Environment.NewLine}{new string('-', heading.Length)}";
            }
        }

        private static string RegistrationHeading
        {
            get
            {
                string heading = "Register";
                return $"{heading}{Environment.NewLine}{new string('-', heading.Length)}";
            }
        }

        private static string LoginHeading
        {
            get
            {
                string heading = "Login";
                return $"{heading}{Environment.NewLine}{new string('-', heading.Length)}";
            }
        }

        public static void DisplayMainHeading()
        {
            Console.Clear();
            Console.WriteLine(MainHeading);
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void DisplayRegistrationHeading()
        {
            Console.Clear();
            Console.WriteLine(RegistrationHeading);
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void DisplayLoginHeading()
        {
            Console.Clear();
            Console.WriteLine(LoginHeading);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
