using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        // Make an array of weapons
        Weapon[] _weapons = new Weapon[3] {
            new Weapon("Stabby Sword",1000,50,"Melee"),
            new Weapon("Death Laser",9999,9999,"OP"),
            new Weapon("Stick",1,50,"Trash")
        };

        // Make an array of Actors (could have been monsters but excercise says actors)
        Actor[] _actors = new Actor[3] { new Skeleton(), new Orc(), new Goblin() };

        //Make a new variable of type Arena and pass the actor and weapon arrays to it through its constuctor
        Arena _arena = new Arena(_actors, _weapons);
        
        //Print the stats for all of the monsters within the arena class
        _arena.PrintAll();

        //Pause the program before it exits automatically
        Console.ReadKey();
    }
}