using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
	static void Main(string[] args)
	{

		Player p1 = new Player(); //Player is a new type, computer will alocate ram based on variables inside
								  //'new' keyword means to allocate ram
		p1.DealDamage(30);
		p1.XPChange(340);
		p1.PrintStats();

		// creates an array that can store 4 player objects and then creates 4 player objects inside of that array
		Player[] p = new Player[4] 
		{ 
			new Player(), 
			new Player(), 
			new Player(), 
			new Player() 
		};
		foreach (var player in p)
			player.PrintStats();
		
		
		Console.ReadLine();
	}
}

