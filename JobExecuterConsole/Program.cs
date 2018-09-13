using System;
using System.Collections.Generic;

namespace JobExecuterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ValidateParams(args))
            {
                var timeRange = int.Parse(args[0]);
                var jobNames = new List<char>(args[1].ToCharArray());
                var results = new JobExecuter(new ConsoleLogger())
                    .Execute(jobNames, TimeSpan.FromMilliseconds(timeRange));

                Console.WriteLine("");
                Console.WriteLine("Execution results:");

                results.ForEach((res) =>
                {
                    Console.WriteLine(res.IsSucceeded
                        ? $"- Job: {res.JobName}, Status: Completed, Execution Time (mls) Actual/Estimated: {(int) res.ExecutionTime.TotalMilliseconds}/{(int)res.EstimatedExecutionTime.TotalMilliseconds}"
                        : $"- Job: {res.JobName}, Status: Failed, Error: {res.ErrorMassage}");
                });
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static bool ValidateParams(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Bad option: command line is empty.");
            }
            else if (args.Length < 2)
            {
                Console.WriteLine("Bad option: it should be two parameters in command line.");
            }
            else if (args.Length > 2)
            {
                Console.WriteLine("Bad option: it should be two parameters in command line.");
            }
            else if (!int.TryParse(args[0], out int timeRange))
            {
                Console.WriteLine("Bad option: first parameter in command line should be time range in mls.");
            }
            else
                return true;

            Console.WriteLine("");
            Console.WriteLine("Usage: ");
            Console.WriteLine("\tConsoleApp.exe [time range in mls] [list of job names]");
            Console.WriteLine("");
            Console.WriteLine("Sample: ");
            Console.WriteLine("\tConsoleApp.exe 5000 aBCdeF");

            return false;
        }
    }
}
