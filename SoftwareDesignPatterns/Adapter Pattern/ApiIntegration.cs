using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignPatterns
{
    public class ApiIntegration
    {
        public ApiIntegration() { }

        public IEnumerable<Person> GetPersons(string targetUrl)
        {
            Console.WriteLine($"Sending HTTP request to {targetUrl}");
            Console.WriteLine("HTTP response, 200 Ok");
            yield return new Person 
            {
                Name = "name",
                LastName = "description",
                Id = 1
            };

        }
    }

    public class ApiIntegrationAdapter : IPersonSourceAdapter
    {
        private readonly ApiIntegration instance;
        private readonly string targetUrl;

        public ApiIntegrationAdapter(ApiIntegration instance, string targetUrl)
        {
            this.instance = instance;
            this.targetUrl = targetUrl;
        }

        public Task<IEnumerable<Person>> RetrievePersons()
        {
            return Task.FromResult(
                this.instance.GetPersons(targetUrl));
        }
    }
}
