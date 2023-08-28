using SoftwareDesignPatterns.Multithreading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Multithreading
{
    [TestClass]
    public sealed class AsynchronousMultithreadingTests
    {
        [TestMethod]
        public void AsynchronousMultithreading_Experiment()
        {
            var instance = new AsynchronousMultithreading();
            instance.Experiment();
        }

        [TestMethod]
        public void AsynchronousMultithreading_ExecuteThreadSafe_Succeeds()
        {
            var instance = new AsynchronousMultithreading(1);
            var tasks = new List<Task>();

            var criticalTask = Task.Run(async () =>
            {
                await instance.ExecuteThreadSafe(CancellationToken.None);
            });

            var criticalTask2 = Task.Run(async () =>
            {
                await instance.ExecuteThreadSafe(CancellationToken.None);
            });

            var criticalTask3 = Task.Run(async () =>
            {
                await instance.ExecuteThreadSafe(CancellationToken.None);
            });

            tasks.Add(criticalTask);
            tasks.Add(criticalTask2);
            tasks.Add(criticalTask3);

            Task.WaitAll(tasks.ToArray());
        }
    }
}
