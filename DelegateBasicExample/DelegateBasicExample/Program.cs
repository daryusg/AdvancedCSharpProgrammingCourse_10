namespace DelegateBasicExample
{

    class Program //20260619 Part 4 - Delegates - Introduction
    {
        //delegate void LogDel(string msg, DateTime dateTime);
        delegate void LogDel(string msg);
        static void Main(string[] args)
        {
            LogDel logDel = new LogDel(LogMsgToScreen);
            //logDel("test message");
            Console.WriteLine("Please enter your name:");
            var name = Console.ReadLine();
            logDel(name + " (logged to screen 1)");

            logDel = new LogDel(LogMsgToFile);
            logDel(name + " (logged to file 1)");

            Log log = new();
            logDel = log.LogMsgToScreen;
            logDel(name + " (logged to screen 2)");

            logDel = log.LogMsgToFile;
            logDel(name + " (logged to file 2)");

            //Multicast Delegates
            LogDel LogMsgToScreenDel, LogMsgToFileDel;
            LogMsgToScreenDel = new LogDel(log.LogMsgToScreen);
            LogMsgToFileDel = new LogDel(log.LogMsgToFile);
            LogDel multiLogDel = LogMsgToScreenDel + LogMsgToFileDel;
            multiLogDel(name + " (multicast - logged to screen and file)");


            LogMsg(multiLogDel, name + " (parameters, multicast - logged to screen and file)");
            LogMsg(LogMsgToScreenDel, name + " (parameters - logged to screen)");
            LogMsg(LogMsgToFileDel, name + " (parameters - logged to file)");

            Console.ReadKey();
        }

        //static void LogMsgToScreen(string msg, DateTime dateTime)
        //{
        //    Console.WriteLine($"{dateTime}: {msg}");
        //}
        static void LogMsgToScreen(string msg)
        {
            Console.WriteLine($"{DateTime.Now}: {msg}");
        }

        static void LogMsgToFile(string msg)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"), true)) {
                sw.WriteLine($"{DateTime.Now}: {msg}"); //C:\Users\kev\source\repos\freeCodeCamp\DelegateBasicExample\DelegateBasicExample\bin\Debug\net10.0
            }
        }

        static void LogMsg(LogDel logDel, string text)
        {
            logDel(text);
        }
    }

    public class Log
    {
        public void LogMsgToScreen(string msg)
        {
            Console.WriteLine($"{DateTime.Now}: {msg}");
        }
        public void LogMsgToFile(string msg)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt"), true))
            {
                sw.WriteLine($"{DateTime.Now}: {msg}"); //C:\Users\kev\source\repos\freeCodeCamp\DelegateBasicExample\DelegateBasicExample\bin\Debug\net10.0
            }
        }
    }
}