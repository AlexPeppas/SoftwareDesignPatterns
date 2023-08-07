using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace SoftwareDesignPatterns.Caching
{
    public sealed class LazyCache : IDisposable //IMemoryCache
    {
        private ConcurrentDictionary<string, Lazy<object>> internalStore;
        private readonly object threadLock = new();

        public LazyCache()
        {
            internalStore = new ConcurrentDictionary<string, Lazy<object>>();
        }

        public void Dispose()
        {
            if (internalStore.Count > 0) 
            {
                lock (this.threadLock)
                {
                    if (internalStore.Count > 0) // double check critical area.
                    {
                        this.internalStore.Clear();
                    }
                }
            }
        }

        public object GetOrAdd(string key, Func<CancellationToken, object> valueFactory, CancellationToken cancellationToken)
        {
            var lazyObject = this.internalStore.GetOrAdd(key,
                new Lazy<object>(() => valueFactory(cancellationToken), LazyThreadSafetyMode.ExecutionAndPublication));

            return lazyObject.Value;
        }

        public object TryGetValue(string key)
        {
            this.internalStore.TryGetValue(key, out Lazy<object> lazyObject);

            return lazyObject.Value;
        }
    }
}
