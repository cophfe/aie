using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            sbyte a = 127; // 1 byte -128 to 127
            byte b = 255; // 1 byte 0 to 255
            short c = 32767; // 2 bytes -32767 to 32768
            ushort d = 65534; // 2 bytes 0 to 65534
            int e = 123; // 4 bytes -2 bill to 2 bill
            uint f = 123; // 4 bytes 0 to 4 bill
            long g = 123; //8 bytes - an amount of sextillions to positive that same amount of sextillions
            ulong h = 123; //8 bytes 0 to 9 sextillion

            char i = 'A'; //1 or 2 bytes, stores characters

            float j = 1.234567f; //4 bytes, stores 7 digits (on c#)
            double k = 1.23456789012345; //8 bytes, stores 15 digits

            bool l = true; //1 byte, stores true or false 

            bool alive = true;

            if (alive)
            {
                Console.WriteLine("Player Alive!");
            }
            else
            {
                Console.WriteLine("Player Dead!");
            }
            int health = 100;
            Console.WriteLine("Player, your health is equal to " + health);
            health = 50;
            Console.WriteLine("Player, your health is equal to " + health);
            Console.ReadKey();
        } //console closes at this point (put break here [F9] to stop in VS without code)
    }
}
