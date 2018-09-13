using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobExecuterConsole
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine($"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff} - Info: " + message);
        }
        public void Error(string message)
        {
            Console.WriteLine($"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff} - Error: " + message);
        }
        public void Debug(string message)
        {
            Console.WriteLine($"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff} - Debug: " + message);
        }
        public void Warn(string message)
        {
            Console.WriteLine($"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff} - Warn: " + message);
        }
    }
}
