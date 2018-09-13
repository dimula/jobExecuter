using System;

namespace JobExecuterConsole
{
    public class JobExecutionException : Exception
    {
        public JobExecutionException(string message) : base(message)
        {
        }
    }
}