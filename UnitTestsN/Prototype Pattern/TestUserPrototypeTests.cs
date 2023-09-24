using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwareDesignPatterns.Prototype_Pattern;

namespace UnitTests.Prototype_Pattern
{
    [TestClass]
    public sealed class TestUserPrototypeTests
    {
        [TestMethod]
        public void CompareCopies()
        {
            var user = new TestUser { Name = "Alex", Nicknames = new string[2] { "Ale", "Alexandros" }, Occupation = new Occupation { Role = "SDE", Level = 62 } };

            var clonedUser = user.ShallowCopy();
            var deepCopy = user.DeepCopy();

            Debug.WriteLine("\n User: \n" + user.ToString());
            Debug.WriteLine("\n ClonedUser: \n" + clonedUser.ToString());
            Debug.WriteLine("\n DeepCopyUser: \n" + deepCopy.ToString());

            user.Nicknames[0] = "Peppas";
            user.Occupation.Level = 63;

            Debug.WriteLine("MODIFIED INITIAL USER");
            Debug.WriteLine("---------------------");

            Debug.WriteLine("\n User: \n" + user.ToString());
            Debug.WriteLine("\n ClonedUser: \n" + clonedUser.ToString());
            Debug.WriteLine("\n DeepCopyUser: \n" + deepCopy.ToString());
        }

        [TestMethod]
        public void PrototypeManagerTests()
        {
            var protoManager = new PrototypeManager();
            var user = new TestUser { Name = "Alex", Nicknames = new string[2] { "Ale", "Alexandros" }, Occupation = new Occupation { Role = "SDE", Level = 62 } };

            protoManager["user"] = user;
            protoManager["clone"] = user.ShallowCopy();
            protoManager["deepCopy"] = user.DeepCopy();

            Debug.WriteLine("\n User: \n" + user.ToString());
            Debug.WriteLine("\n ClonedUser: \n" + protoManager["user"].ToString());
            Debug.WriteLine("\n DeepCopyUser: \n" + protoManager["deepCopy"].ToString());

            user.Nicknames[0] = "Peppas";
            user.Occupation.Level = 63;

            Debug.WriteLine("MODIFIED INITIAL USER");
            Debug.WriteLine("---------------------");

            Debug.WriteLine("\n User: \n" + user.ToString());
            Debug.WriteLine("\n ClonedUser: \n" + protoManager["user"].ToString());
            Debug.WriteLine("\n DeepCopyUser: \n" + protoManager["deepCopy"].ToString());
        }
    }
}
