using SoftwareDesignPatterns.Lazy_Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Lazy_Initialization
{
    [TestClass]
    public sealed class LazyInitObjectTests
    {
        [TestMethod]
        public void LazyInitObject_Instantiation_LoadsConfig_Lazily()
        {
            var configObjectId = Guid.NewGuid();

            var lazyConfig = new LazyInitObject(configObjectId);

            var settings = lazyConfig.ConfigSettings;

            Assert.IsNotNull(settings); 
        }

        [TestMethod]
        public void LazyInitObject_ThreadSafety_ConcurrentAccessOnLoadConfig_Successful()
        {
            var configObjectId = Guid.NewGuid();

            var lazyConfig = new LazyInitObject(configObjectId);

            // Use a thread-safe collection to store exceptions thrown by threads
            var exceptions = new List<Exception>();

            int numThreads = 10; // Number of threads to simulate concurrent access

            // Create multiple threads that access the configuration settings simultaneously
            var tasks = new List<Task>();
            for (int i = 0; i < numThreads; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        var settings = lazyConfig.ConfigSettings;
                        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - SomeSetting: {settings.SomeSetting}");
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            // Check if any exceptions were thrown
            Assert.AreEqual(0, exceptions.Count, $"Exceptions occurred: {exceptions.Count}");
        }
    }
}
