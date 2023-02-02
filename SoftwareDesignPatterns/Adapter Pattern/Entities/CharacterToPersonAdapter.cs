using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareDesignPatterns
{
    public sealed class CharacterToPersonAdapter : Person
    {
        private readonly Character character;

        public CharacterToPersonAdapter(Character character)
        {
            this.character = character;
        }

        public override string Name 
        { 
            get => this.character.CharacterName;
            set => this.character.CharacterName = value; 
        }

        public override string LastName 
        {
            get => this.character.Surname;
            set => this.character.Surname = value; 
        }
    }
}
