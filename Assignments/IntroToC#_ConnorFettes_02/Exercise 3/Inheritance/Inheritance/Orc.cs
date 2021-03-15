using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Orc : Monster
{
    public Orc()
    {
        //default values for Orc
        _weapon = new Weapon("Fists", 1000, 50, "Melee");
        _name = "Grog";
        _health = 2000;
        _strength = 200;
    }

    //overrides Monster's Print Function
    public override void Print()
    {
        Console.WriteLine("Orc: A true twist of fate, it is, making us fight. I will go out like a warrior. ");
        Console.WriteLine($"Name: {_name}, Health: {_health}, Strength: {_strength}");
        Console.Write($"Weapon Name: {_weapon.GetName()}, Weapon Damage: {_weapon.GetDamage()}");
        Console.WriteLine($", Weapon Range: { _weapon.GetRange()}, Weapon Catagory: { _weapon.GetCatagory()}\n");
    }
}

