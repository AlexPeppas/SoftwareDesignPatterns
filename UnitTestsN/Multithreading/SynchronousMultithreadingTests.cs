using SoftwareDesignPatterns.Multithreading;

namespace UnitTests.Multithreading
{
    [TestClass]
    public sealed class SynchronousMultithreadingTests
    {
        [TestMethod]
        public void SyncMultithreading_TryExecute_Syncrhonously_Succeeds()
        {
            var instance = new SynchronousMultiThreading();

            var actions = new List<Action>
            {
                Greet,
                Greet,
                Greet,
                Greet,
                Greet,
                Greet,
                Greet,
            };

            Parallel.ForEach(actions, action =>
            {
                instance.TryExecute(action);
            });
        }

        private void Greet()
        {
            Console.WriteLine($"{Task.CurrentId}, hello world");
        }
    }
}
