using SoftwareDesignPatterns.AsyncDisposable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.AsyncDisposable
{
    [TestClass]
    public class DisposeObjectTests
    {
        [TestMethod]
        public async Task DisposingObjectTests()
        {
            var disposableObject = new LockSimulationObject();

            await disposableObject.DoSomethingThenDispose();
        }
    }
}
