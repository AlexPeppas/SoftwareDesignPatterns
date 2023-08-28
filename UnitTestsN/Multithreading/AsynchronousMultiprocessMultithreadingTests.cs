using SoftwareDesignPatterns.Multithreading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Multithreading
{
    [TestClass]
    public sealed class AsynchronousMultiprocessMultithreadingTests
    {
        [TestMethod]
        public void AsynchronousMultiprocessMultithreading_Experiment()
        {
            var instance = new AsynchronousMultiprocessMultithreading();
            instance.Experiment();
        }

        [TestMethod]
        public void AsynchronousMultiprocessMultithreading_ExecuteThreadSafe_TwoInstances_SameLockResource_MutexAccessedSequentially()
        {
            var instance = new AsynchronousMultiprocessMultithreading();
            var instance2 = new AsynchronousMultiprocessMultithreading();
            var tasks = new List<Task>();

            var criticalTask = Task.Run(async () =>
            {
                await instance.ExecuteThreadSafe(CancellationToken.None);
            });

            var criticalTask2 = Task.Run(async () =>
            {
                await instance2.ExecuteThreadSafe(CancellationToken.None);
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

        [TestMethod]
        public void AsynchronousMultiprocessMultithreading_ExecuteThreadSafe_TwoInstances_DiffLockResource_BothAccessMutex()
        {
            var instance = new AsynchronousMultiprocessMultithreading("lock1");
            var instance2 = new AsynchronousMultiprocessMultithreading("lock2");
            var tasks = new List<Task>();

            var criticalTask = Task.Run(async () =>
            {
                await instance.ExecuteThreadSafe(CancellationToken.None);
            });

            var criticalTask2 = Task.Run(async () =>
            {
                await instance2.ExecuteThreadSafe(CancellationToken.None);
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
