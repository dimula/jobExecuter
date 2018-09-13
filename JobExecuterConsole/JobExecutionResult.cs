using System;

namespace JobExecuterConsole
{
    public class JobExecutionResult
    {
        public TimeSpan ExecutionTime { get; set; }
        public TimeSpan EstimatedExecutionTime { get; set; }
        public char JobName { get; set; }
        public bool IsSucceeded { get; set; }
        public string ErrorMassage { get; set; }
    }
}