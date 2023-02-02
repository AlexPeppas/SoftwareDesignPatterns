using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignPatterns
{
    public class DatabaseIntegration
    {
        public DatabaseIntegration()
        {

        }

        public IEnumerable<Person> RetrievePersons()
        {
            Console.WriteLine("Opening SQL connection");
            Console.WriteLine("Connection closed");
            yield return new Person
            {
                Name = "name2",
                LastName = "description2",
                Id = 2
            };
        }
    }

    public class DatabaseIntegrationAdapter : IPersonSourceAdapter
    {
        private readonly DatabaseIntegration instance;

        public DatabaseIntegrationAdapter(DatabaseIntegration instance)
        {
            this.instance = instance;
        }

        public Task<IEnumerable<Person>> RetrievePersons()
        {
            return Task.FromResult(
                this.instance.RetrievePersons());
        }
    }
}
