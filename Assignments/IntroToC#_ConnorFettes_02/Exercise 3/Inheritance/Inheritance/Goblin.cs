using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Goblin : Monster
{
    public Goblin()
    {
        //Goblin has default values for everything
        _weapon = new Weapon("Knife", 3000, 10, "Melee");
        _name = "George";
        _health = 500;
        _strength = 50;
    }

    //overrides Monster's Print Function
    public override void Print()
    {
        //Prints stats (obviously)
        Console.WriteLine("Goblin: Guur... I'm a Goblin...");
        Console.WriteLine($"Name: {_name}, Health: {_health}, Strength: {_strength}");
        Console.Write($"Weapon Name: {_weapon.GetName()}, Weapon Damage: {_weapon.GetDamage()}");
        Console.WriteLine($", Weapon Range: { _weapon.GetRange()}, Weapon Catagory: { _weapon.GetCatagory()}\n");
    }
}

