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
            Console.WriteLine("Info: " + message);
        }
        public void Error(string message)
        {
            Console.WriteLine("Error: " + message);
        }
        public void Debug(string message)
        {
            Console.WriteLine("Debug: " + message);
        }
        public void Warn(string message)
        {
            Console.WriteLine("Warn: " + message);
        }
    }
}
