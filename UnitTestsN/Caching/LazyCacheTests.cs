using Shared;
using SoftwareDesignPatterns.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Caching
{
    [TestClass]
    public sealed class LazyCacheTests
    {
        protected CancellationToken cancellationToken = new CancellationToken();

        [TestMethod]
        public void LazyCache_GetOrdAdd_Succeeds()
        {
            var cache = new LazyCache();
            var cacheKey = "1";

            var personBoxed = cache.GetOrAdd(cacheKey, (cts) =>
            {
                Console.WriteLine("initializing...");
                return new Person
                {
                    Id = 1,
                    Name = "Alex",
                    LastName = "Pep",
                };
            },
            this.cancellationToken);

            var samePersonBoxed = cache.TryGetValue(cacheKey);

            Assert.AreEqual(personBoxed, samePersonBoxed);
            Assert.IsInstanceOfType(personBoxed, typeof(Person));
        }

        [TestMethod]
        public void LazyCache_Disposed_UsingBrackets()
        {
            using (var cache = new LazyCache())
            {
                var cacheKey = "1";

                var personBoxed = cache.GetOrAdd(cacheKey, (cts) =>
                {
                    Console.WriteLine("initializing...");
                    return new Person
                    {
                        Id = 1,
                        Name = "Alex",
                        LastName = "Pep",
                    };
                },
                this.cancellationToken);

                var samePersonBoxed = cache.TryGetValue(cacheKey);

                Assert.AreEqual(personBoxed, samePersonBoxed);
                Assert.IsInstanceOfType(personBoxed, typeof(Person));
            }
        }

        [TestMethod]
        public void LazyCache_Disposed_UsingNoBrackets()
        {
            using var cache = new LazyCache();
            
            var cacheKey = "1";

            var personBoxed = cache.GetOrAdd(cacheKey, (cts) =>
            {
                Console.WriteLine("initializing...");
                return new Person
                {
                    Id = 1,
                    Name = "Alex",
                    LastName = "Pep",
                };
            },
            this.cancellationToken);

            var samePersonBoxed = cache.TryGetValue(cacheKey);

            Assert.AreEqual(personBoxed, samePersonBoxed);
            Assert.IsInstanceOfType(personBoxed, typeof(Person));
        }
    }
}
