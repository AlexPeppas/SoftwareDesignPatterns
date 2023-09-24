using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignPatterns.Prototype_Pattern
{
    public abstract class Prototype
    {
        public abstract Prototype ShallowCopy();

        public abstract Prototype DeepCopy();
    }

    public sealed class TestUser : Prototype
    {
        public string Name { get; set; }

        public string[] Nicknames { get; set; } 

        public Occupation Occupation { get; set; }

        public override Prototype DeepCopy()
        {
            var clonedUser = (TestUser)this.MemberwiseClone();
            clonedUser.Occupation = new Occupation { Level = Occupation.Level, Role = Occupation.Role };

            clonedUser.Nicknames = new string[Nicknames.Length];
            var indx = 0;
            foreach (var nick in Nicknames)
            {
                clonedUser.Nicknames[indx++] = nick;
            }

            return clonedUser;
        }

        public override Prototype ShallowCopy()
        {
            return (Prototype)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{Name}, {string.Join(",", Nicknames)}, {Occupation.Role}-{Occupation.Level}";
        }
    }

    public sealed class Occupation
    {
        public string Role { get; set; }

        public int Level { get; set; }
    }

    public sealed class PrototypeManager
    {
        private readonly Dictionary<string, Prototype> protoStore= new();

        public Prototype this[string key]
        {
            get => protoStore[key];
            set => protoStore[key] = value;
        }
    }
}
