using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Character
    {
        public static class FieldNames
        {
            public static string CharacterNameField = "CharacterName";
            public static string SurnameField = "Surname";
            public static string GenderField = "Gender";
        }

        public string CharacterName { get; set; }

        public string Surname { get; set; }

        public string Gender { get; set; }
    }
}
