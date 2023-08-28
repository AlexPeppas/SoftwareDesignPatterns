namespace SoftwareDesignPatterns.Multithreading
{
    public sealed class AsynchronousMultiprocessMultithreading
    {
        private readonly Mutex mutex;

        private const int numIterations = 1;
        private const int numThreads = 3;

        private int padding;
        private string lockId = "sharedResource";

        public AsynchronousMultiprocessMultithreading()
        {
            mutex = new Mutex(false, lockId);
        }

        public AsynchronousMultiprocessMultithreading(string lockResourceId)
        {
            this.lockId = lockResourceId;
            mutex = new Mutex(false, lockResourceId);
        }

        public async Task ExecuteThreadSafe(CancellationToken cancellationToken)
        {
            // Wait until it is safe to enter.
            var taskId = Task.CurrentId;
            Console.WriteLine("{0} is requesting the mutex", taskId);

            mutex.WaitOne();
            try
            {
                Interlocked.Add(ref padding, 100);

                Console.WriteLine("{0} Entered critical area of {1}, {2}", Task.CurrentId, this.lockId, DateTime.UtcNow);

                // The task just sleeps for 1+ seconds.
                await Task.Delay(1000 + padding);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
            Console.WriteLine("{0} has released the mutex of {1}, {2}",
                taskId, this.lockId, DateTime.UtcNow);
        }

        public void Experiment()
        {
            // Create the threads that will use the protected resource.
            for (int i = 0; i < numThreads; i++)
            {
                Thread newThread = new Thread(new ThreadStart(ThreadProc));
                newThread.Name = string.Format("Thread{0}", i + 1);
                newThread.Start();
            }

            Thread.Sleep(2000);
            // The main thread exits, but the application continues to
            // run until all foreground threads have exited.
        }

        private void ThreadProc()
        {
            for (int i = 0; i < numIterations; i++)
            {
                UseResource();
            }
        }

        // This method represents a resource that must be synchronized
        // so that only one thread at a time can enter.
        private void UseResource()
        {
            // Wait until it is safe to enter.
            Console.WriteLine("{0} is requesting the mutex",
                              Thread.CurrentThread.Name);
            mutex.WaitOne();

            Console.WriteLine("{0} has entered the protected area, {1}",
                              Thread.CurrentThread.Name, DateTime.UtcNow);

            // Place code to access non-reentrant resources here.

            // Simulate some work.
            Thread.Sleep(500);

            Console.WriteLine("{0} is leaving the protected area",
                Thread.CurrentThread.Name);

            // Release the Mutex.
            mutex.ReleaseMutex();
            Console.WriteLine("{0} has released the mutex {1}",
                Thread.CurrentThread.Name, DateTime.UtcNow);
        }
    }
}
