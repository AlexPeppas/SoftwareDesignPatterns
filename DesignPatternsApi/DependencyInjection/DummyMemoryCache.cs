namespace DesignPatternsApi.DependencyInjection
{
    public sealed class DummyMemoryCache : IDummyMemoryCache
    {
        public async Task RememberMe() => await Task.Yield();
    }
}
