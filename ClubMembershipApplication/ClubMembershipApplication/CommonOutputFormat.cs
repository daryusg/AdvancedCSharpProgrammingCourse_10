using System;
using System.Collections.Generic;
using System.Text;

namespace ClubMembershipApplication
{
    public enum FontTheme
    {
        Default,
        Danger,
        Success
    }

    public static class CommonOutputFormat //20260623 Part 5 - Delegates - Create a Code Example
    {
        public static void ChangeFontColour(FontTheme fontTheme)
        {
            switch (fontTheme)
            {
                case FontTheme.Danger:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case FontTheme.Success:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case FontTheme.Default:
                    Console.ResetColor();
                    break;
            }
        }
    }
}
