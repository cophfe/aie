using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Inherits From Actor
class Monster : Actor
{
    
    public Monster()
    {
        //Sets the Actor variables to the default monster values
        _weapon = new Weapon("Default Monstar Gun", 999, 999, "Monstar");
        _name = "Default Monstar";
        _health = 1000;
    }

    //overrides Monster's Print Function
    public override void Print()
    {
        //does the same thing as Actor but an extra line is added to the start
        Console.WriteLine("Rawr! I'm a monstar!\n...\nAnyways");
        base.Print();
    }
}

