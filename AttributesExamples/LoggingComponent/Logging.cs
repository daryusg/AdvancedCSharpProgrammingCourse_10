using System.Diagnostics;

namespace LoggingComponent
{
    public class Logging
    {
        //[Conditional("LOG_INFO")] //only run below method if LOG_INFO is defined in the project
        //also see: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives
        [Obsolete("The LogToScreen method has been deprecated. Use the 'LogToFile' method instead.")] //green squiggly line
        //[Obsolete("The LogToScreen method has been deprecated. Use the 'LogToFile' method instead.", false)] //green squiggly line
        //[Obsolete("The LogToScreen method has been deprecated. Use the 'LogToFile' method instead.", true)] //red squiggly line
        public static void LogToScreen(string msg)
        {
            Console.WriteLine($"[{DateTime.Now}] {msg}");
        }

        public static void LogToFile(string msg)
        {
            Console.WriteLine($"(simulating writing to a text file) [{DateTime.Now}] {msg}");
        }
    }
}
