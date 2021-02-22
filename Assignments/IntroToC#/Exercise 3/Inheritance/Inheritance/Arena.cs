using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Arena
{
    //Contains an array of actors in the arena
    Actor[] _actors;

    //Constructer that requires a list of actors and has optional list of weapons
    public Arena(Actor[] actors, Weapon[] weapons = null)
    {
        
        _actors = actors;

        if (weapons != null)
        {
            //gives a weapon to every actor until it runs out of either actors or weapons
            for (int i = 0; i < Math.Min(weapons.Length, _actors.Length); i++)
            {
                _actors[i].SetWeapon(weapons[i]);
            }
        }
        
        
    }
    public void PrintAll()
    {
        foreach(var a in _actors)
        {
            a.Print();
        }
    }
}

