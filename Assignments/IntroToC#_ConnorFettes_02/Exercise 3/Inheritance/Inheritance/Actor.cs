using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Actor
{
    //Protected means they can be accessed by classes that inherit from Actor
    protected Weapon _weapon = new Weapon();
    protected string _name = "Actor";
    protected int _health = 100;
    protected int _strength = 50;

    //Empty Constructor
    public Actor() 
    {

    }

    //Constructor that allows you to set a weapon
    public Actor(Weapon weapon)
    {
        _weapon = weapon;
    }

    //Function to set weapon to a given value
    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    //Print Stats about the Actor. It is virtual so it can be overwritten by derived classes
    public virtual void Print()
    {
        Console.WriteLine($"Name: {_name}, Health: {_health}, Strength: {_strength}");
        Console.Write($"Weapon Name: {_weapon.GetName()}, Weapon Damage: {_weapon.GetDamage()}");
        Console.WriteLine($", Weapon Range: { _weapon.GetRange()}, Weapon Catagory: { _weapon.GetCatagory()}\n");
    }
}

