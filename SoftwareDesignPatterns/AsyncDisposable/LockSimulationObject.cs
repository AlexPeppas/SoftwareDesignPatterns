namespace SoftwareDesignPatterns.AsyncDisposable
{
    public sealed class LockSimulationObject : IAsyncDisposable
    {
        public LockSimulationObject()
        {
            
        }

        public async Task DoSomethingThenDispose()
        {
            await using (this.ConfigureAwait(false))
            {
                for (int i=0; i<5; i++)
                {
                    Console.WriteLine($"Spending CPU cycles round {i}");
                    Console.WriteLine(Math.Pow(2, i));
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("Disposing unmanaged resources...");

            await Task.Delay(1000); // 1 sec

            Console.WriteLine("Disposed!");
        }
    }
}
