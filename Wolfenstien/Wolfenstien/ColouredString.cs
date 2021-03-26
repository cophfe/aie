using System;
using System.Collections.Generic;
using System.Text;

namespace Wolfenstien
{
    class ColouredString
    {
        ConsoleColor color;
        char character;
        int size;

        public ColouredString(ConsoleColor color, char character, int size)
        {
            this.color = color;
            this.character = character;
            this.size = size;
        }

        public void Write()
        {
            
            Console.BackgroundColor = color;
            Console.Write(new String(character, size));
        }
        public ConsoleColor GetColor()
        {
            return color;
        }
        public int GetSize()
        {
            return size;
        }
        public string GetString()
        {
            return new String(character, size);
        }
    }
}
