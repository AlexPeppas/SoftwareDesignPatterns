using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Person
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string LastName { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name}-{LastName}";
        }
    }
}
