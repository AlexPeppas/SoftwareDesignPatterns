using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignPatterns
{
    public class MainThread
    {
        private readonly IPersonSourceAdapter personSource;

        public MainThread(IPersonSourceAdapter personSource)
        {
            this.personSource = personSource;
        }

        public async Task<IEnumerable<Person>> ListPersonsAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var persons = await this.personSource.RetrievePersons();

            if (persons is null)
            {
                Console.WriteLine("No Persons found");
                return Enumerable.Empty<Person>();
            }

            foreach (var person in persons)
            {
                Console.WriteLine(person.ToString());
            }

            return persons;
        }
    }
}
