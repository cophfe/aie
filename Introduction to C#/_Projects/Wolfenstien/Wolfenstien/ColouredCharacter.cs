using System;
using System.Collections.Generic;
using System.Text;

namespace Wolfenstien
{
    struct ColouredCharacter
    {
        public char character;
        public ConsoleColor color;
        public ColouredCharacter(ConsoleColor color, char character)
        {
            this.character = character;
            this.color = color;
        }
        
    }
}
