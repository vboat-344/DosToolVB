using System;
using System.IO;

namespace DosToolVB
{
    static class Logger
    {
        private static readonly object lockObj = new object();

        public static void Log(string message, ConsoleColor color = ConsoleColor.White)
        {
            lock (lockObj)
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string fullMessage = "[" + timestamp + "] " + message;

                Console.ForegroundColor = color;
                Console.WriteLine(fullMessage);
                Console.ResetColor();

                using (StreamWriter sw = new StreamWriter("logs.log", true))
                {
                    sw.WriteLine(fullMessage);
                }
            }
        }

        public static void ShowLogs()
        {
            Console.Clear();
            if (!File.Exists("logs.log"))
            {
                Console.WriteLine("Логов пока нет.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("═══ ЛОГИ ═══");
            string[] lines = File.ReadAllLines("logs.log");
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("═══════════════");
            Console.ReadKey();
        }
    }
}