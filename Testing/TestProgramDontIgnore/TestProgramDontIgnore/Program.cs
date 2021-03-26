using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
	static void Main(string[] args)
	{
		//Inheritance: the idea that we can have a class that gives code to another
		//eg "zombie" class inherits from "melee enemy" class inherits from "enemy" class enherits from "actor" class inherits from "gameObject" class
		
		Horse _hrse = new Horse("Rawr", 25, 100);
		_hrse.PrintStats();
		new EnemyCommon("ememy",100).PrintStats();
		new Common("haha").PrintStats();
		
		Console.ReadLine();
	}
}

