using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Skeleton : Monster
{
    public Skeleton()
    {
        //default values for skeleton
        _weapon = new Weapon("Bow", 300, 3000, "Trash");
        _name = "Henry";
        _health = 300;
    }

    //overides Monster's Print Function
    public override void Print()
    {
        Console.WriteLine("Skeleton: Graaar!!! I'm a SkellyBoy!!");
        Console.WriteLine($"Name: {_name}, Health: {_health}");
        Console.Write($"Weapon Name: {_weapon.GetName()}, Weapon Damage: {_weapon.GetDamage()}");
        Console.WriteLine($", Weapon Range: { _weapon.GetRange()}, Weapon Catagory: { _weapon.GetCatagory()}\n");
    }
}

