using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public sealed class MainThreadTests
    {
        [TestMethod]
        public async Task TestApiIntegration()
        {
            // Arrange
            var mainThreadInstance = new MainThread(
                new ApiIntegrationAdapter(
                    new ApiIntegration(), "api/personsContainer/listAll"));

            // Act
            var persons = await mainThreadInstance.ListPersonsAsync(CancellationToken.None);

            // Assert
            Assert.IsNotNull(persons);
        }

        [TestMethod]
        public async Task TestDatabaseIntegration()
        {
            // Arrange
            var mainThreadInstance = new MainThread(
                new DatabaseIntegrationAdapter(
                    new DatabaseIntegration()));

            // Act
            var persons = await mainThreadInstance.ListPersonsAsync(CancellationToken.None);

            // Assert
            Assert.IsNotNull(persons);
        }
    }
}
