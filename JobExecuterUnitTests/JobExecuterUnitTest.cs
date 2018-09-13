using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JobExecuterConsole;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace JobExecuterConsoleUnitTests
{
    [TestClass]
    public class JobExecuterUnitTest
    {
        [TestMethod]
        public void Execute_LowerCaseNameTest()
        {
            var executer = new JobExecuter(Mock.Create<ILogger>());
            var res = executer.Execute(new List<char>() {'a'}, TimeSpan.FromMilliseconds(500));

            Assert.AreEqual<int>(1, res.Count);
            var job = res.Find(x => x.JobName == 'a');
            TestExecutionResult(job, 'a');
        }

        [TestMethod]
        public void Execute_UpperCaseNameTest()
        {
            var executer = new JobExecuter(Mock.Create<ILogger>());
            var res = executer.Execute(new List<char>() { 'B' }, TimeSpan.FromMilliseconds(500));

            Assert.AreEqual<int>(1, res.Count);
            var job = res.Find(x => x.JobName == 'B');
            TestExecutionResult(job, 'B');
        }

        [TestMethod]
        public void Execute_ActualTimeRangeTest()
        {
            var executer = new JobExecuter(Mock.Create<ILogger>());
            var res = executer.Execute(new List<char>() { 'E' }, TimeSpan.FromMilliseconds(500));

            Assert.AreEqual<int>(1, res.Count);
            var job = res.Find(x => x.JobName == 'E');
            Assert.IsNotNull(job);
            Assert.IsTrue(job.ExecutionTime.TotalMilliseconds > 0);
            Assert.IsTrue(job.ExecutionTime.TotalMilliseconds < 500);
        }

        [TestMethod]
        public void Execute_EstimatedTimeRangeTest()
        {
            var executer = new JobExecuter(Mock.Create<ILogger>());
            var res = executer.Execute(new List<char>() { 'E' }, TimeSpan.FromMilliseconds(500));

            Assert.AreEqual<int>(1, res.Count);
            var job = res.Find(x => x.JobName == 'E');
            Assert.IsNotNull(job);
            Assert.IsTrue(job.EstimatedExecutionTime.TotalMilliseconds > 0);
            Assert.IsTrue(job.EstimatedExecutionTime.TotalMilliseconds < 500);
        }

        private static void TestExecutionResult(JobExecutionResult execResult, char jobName)
        {
            Assert.IsNotNull(execResult);
            Assert.AreEqual<char>(jobName, execResult.JobName);
            if (jobName == char.ToLower(jobName))
            {
                Assert.AreEqual<char>(jobName, execResult.JobName);
                Assert.IsFalse(execResult.IsSucceeded);
                Assert.IsNotNull(execResult.ErrorMassage);
            }
            else
            {
                Assert.IsTrue(execResult.IsSucceeded);
                Assert.IsNull(execResult.ErrorMassage);
                Assert.IsTrue(execResult.ExecutionTime.Ticks >0);
            }
        }

        [TestMethod]
        public void Execute_MixedCaseNameTest()
        {
            var executer = new JobExecuter(Mock.Create<ILogger>());
            var jobs = new List<char>() { 'B', 'c', 'e', 'Z' };
            var res = executer.Execute(jobs, TimeSpan.FromMilliseconds(500));

            Assert.AreEqual<int>(4, res.Count);
            jobs.ForEach(jobName =>
            {
                var job = res.Find(x => x.JobName == jobName);
                TestExecutionResult(job, jobName);
            });
        }

        [TestMethod]
        public void Execute_LoggerMustBeCalledTest()
        {
            var logger = Mock.Create<ILogger>();

            Mock.Arrange(() => logger.Debug(Arg.IsAny<string>())).MustBeCalled();
            Mock.Arrange(() => logger.Info(Arg.IsAny<string>())).MustBeCalled();

            var executer = new JobExecuter(logger);
            var jobs = new List<char>() { 'B', 'c', 'e', 'Z' };
            executer.Execute(jobs, TimeSpan.FromMilliseconds(500));

            Mock.Assert(logger);
        }
    }
}
