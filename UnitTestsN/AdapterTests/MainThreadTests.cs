using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareDesignPatterns;

namespace UnitTests
{
    [TestClass]
    public sealed class MainThreadTests
    {
        [TestMethod]
        public async Task TestApiIntegration()
        {
            // Arrange
            var apiIntegrationAdapter = new ApiIntegrationAdapter(
                    new ApiIntegration(), "api/personsContainer/listAll");

            var mainThreadInstance = new MainThread(apiIntegrationAdapter);

            // Act
            var persons = await mainThreadInstance.ListPersonsAsync(CancellationToken.None);

            // Assert
            Assert.IsNotNull(persons);
        }

        [TestMethod]
        public async Task TestDatabaseIntegration()
        {
            // Arrange
            var databaseIntegrationAdapter = new DatabaseIntegrationAdapter(
                    new DatabaseIntegration());
            
            var mainThreadInstance = new MainThread(databaseIntegrationAdapter);

            // Act
            var persons = await mainThreadInstance.ListPersonsAsync(CancellationToken.None);

            // Assert
            Assert.IsNotNull(persons);
        }
    }
}
