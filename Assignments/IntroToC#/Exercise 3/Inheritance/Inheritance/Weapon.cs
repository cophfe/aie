using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Weapon
{
    //initialise variables with default values
    string _name = "Default Gun";
    int _damage = 100;
    float _range = 100;
    //is this even how you are supposed to spell catagory
    string _catagory = "Default";

    //Empty Constructor
    public Weapon() { }

    //Constructer that allows you to set the variables through its parameters
    public Weapon(string name, int damage, float range, string catagory)
    {
        _name = name;
        _damage = damage;
        _range = range;
        _catagory = catagory;
    }


    //These functions are used to access the weapon stats without using public variables
    public string GetName()
    {
        return _name;
    }
    public int GetDamage()
    {
        return _damage;
    }
    public float GetRange()
    {
        return _range;
    }
    public string GetCatagory()
    {
        return _catagory;
    }
}
