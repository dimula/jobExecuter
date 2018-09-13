using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobExecuterConsole
{
    public class JobExecuter
    {
        readonly ILogger logger;

        public JobExecuter(ILogger logger)
        {
            this.logger = logger;
        }

        public List<JobExecutionResult> Execute(List<char> jobNames, TimeSpan timeRange)
        {
            var random = new Random();
            TimeSpan JobFunction(object x)
            {
                var name = (char)x;
                if (name == char.ToLower(name))
                    throw new JobExecutionException($"jobName ({name}) is in lower case.");

                var delay = random.Next((int)timeRange.TotalMilliseconds);
                Thread.Sleep(delay);

                return TimeSpan.FromMilliseconds(delay);
            }

            logger.Info($"Execution started (JobNames: {String.Join(",", jobNames)}, TimeRange: {timeRange.TotalMilliseconds}).");
            var tasks = jobNames.ToDictionary(x => x, x => Task.Factory.StartNew(JobFunction, x));

            logger.Debug("Waiting...");
            try
            {
                Task.WaitAll(tasks.Values.ToArray());
            }
            catch (AggregateException ae)
            {
                //write to log all unknown exceptions
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    if(!(e is JobExecutionException))
                        logger.Error(e.ToString());
                }
            }

            logger.Debug("Execution completed.");
            return tasks.Keys.Select(key => new JobExecutionResult()
                {
                    JobName = key,
                    IsSucceeded = tasks[key].Status == TaskStatus.RanToCompletion,
                    ErrorMassage = tasks[key].Exception?.InnerException?.Message,
                    ExecutionTime = tasks[key].Status == TaskStatus.RanToCompletion ? tasks[key].Result : TimeSpan.MinValue
                }).ToList();
        }
    }
}
