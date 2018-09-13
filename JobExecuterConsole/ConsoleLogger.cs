using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobExecuterConsole
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine($"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff} Thread: {Thread.CurrentThread.ManagedThreadId} Info: " + message);
        }
        public void Error(string message)
        {
            Console.WriteLine($"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff} Thread: {Thread.CurrentThread.ManagedThreadId} Error: " + message);
        }
        public void Debug(string message)
        {
            Console.WriteLine($"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff} Thread: {Thread.CurrentThread.ManagedThreadId} Debug: " + message);
        }
        public void Warn(string message)
        {
            Console.WriteLine($"{DateTime.Now:MM/dd/yyyy HH:mm:ss.fff} Thread: {Thread.CurrentThread.ManagedThreadId} Warn: " + message);
        }
    }
}
