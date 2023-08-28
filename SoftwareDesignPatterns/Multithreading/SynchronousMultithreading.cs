namespace SoftwareDesignPatterns.Multithreading
{
    using System;

    public sealed class SynchronousMultiThreading
    {
        private readonly object locker;

        public SynchronousMultiThreading()
        {
            this.locker = new object();
        }

        public void TryExecute(Action action)
        {
            lock(locker) 
            {
                Console.WriteLine($"Task {Task.CurrentId} enters lock");

                action.Invoke();

                Console.WriteLine($"Task {Task.CurrentId} releases lock");
            }
        }

        public void TryExecuteAsync(Func<Task> action)
        {
            lock (locker)
            {
                Console.WriteLine($"Task {Task.CurrentId} enters lock");

                action.Invoke();

                Console.WriteLine($"Task {Task.CurrentId} releases lock");
            }
        }
    }
}
