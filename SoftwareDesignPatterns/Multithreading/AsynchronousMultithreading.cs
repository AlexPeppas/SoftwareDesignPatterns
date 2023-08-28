namespace SoftwareDesignPatterns.Multithreading
{
    public sealed class AsynchronousMultithreading
    {
        private readonly SemaphoreSlim semaphore;
        private static int padding;
        private static int maxCount = 1;

        public AsynchronousMultithreading()
        {
            semaphore = new SemaphoreSlim(0, maxCount);
        }

        public AsynchronousMultithreading(int initialThreadCount)
        {
            semaphore = new SemaphoreSlim(initialThreadCount, initialThreadCount);
        }

        public async Task ExecuteThreadSafe(CancellationToken cancellationToken)
        {
            int semaphoreCount;
            semaphore.Wait();
            try 
            {
                Interlocked.Add(ref padding, 100);

                Console.WriteLine("Task {0} enters the semaphore.", Task.CurrentId);

                // The task just sleeps for 1+ seconds.
                Thread.Sleep(1000 + padding);

                await Task.CompletedTask;
            }
            finally 
            {
                semaphoreCount = semaphore.Release(); 
            }
            Console.WriteLine("Task {0} releases the semaphore; previous count: {1}.",
                                      Task.CurrentId, semaphoreCount);
        }

        public void Experiment()
        {
            Console.WriteLine("{0} tasks can enter the semaphore.",semaphore.CurrentCount);
            Task[] tasks = new Task[5];

            // Create and start five numbered tasks.
            for (int i = 0; i <= 4; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    // Each task begins by requesting the semaphore.
                    Console.WriteLine("Task {0} begins and waits for the semaphore.",
                                      Task.CurrentId);

                    int semaphoreCount;
                    semaphore.Wait();
                    try
                    {
                        Interlocked.Add(ref padding, 100);

                        Console.WriteLine("Task {0} enters the semaphore.", Task.CurrentId);

                        // The task just sleeps for 1+ seconds.
                        Thread.Sleep(1000 + padding);
                    }
                    finally
                    {
                        semaphoreCount = semaphore.Release();
                    }
                    Console.WriteLine("Task {0} releases the semaphore; previous count: {1}.",
                                      Task.CurrentId, semaphoreCount);
                });
            }

            // Wait for half a second, to allow all the tasks to start and block.
            Console.WriteLine("blocking main thread for 500ms to allow task start and block");
            Thread.Sleep(500);

            // Restore the semaphore count to its maximum value.
            Console.Write("Main thread calls Release(3) --> ");
            semaphore.Release(maxCount);
            Console.WriteLine("{0} tasks can enter the semaphore.",
                              semaphore.CurrentCount);
            // Main thread waits for the tasks to complete.
            Task.WaitAll(tasks);

            Console.WriteLine("Main thread exits.");
        }
    }
}
