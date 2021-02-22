using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace switches
{
	public enum EnemyTypeEnum
	{
		Orc, //means 0
		Skeleton, //means 1
		Dragon, //means 2
		//Can also define the value
		Goblin = 10,
		Chupacobra, //is one above the last value
		//(could lead to duplicate values )
		Count, // do a trick by calling the last one 'count' or 'length', which shows the number of options (unless you change one of the values)
		None // Add 'none' to the end or the start (given value of -1) for situations where you need a none of the above option
	}

	
	class Program
	{
		static void Main(string[] args)
		{
			///////////////////////////////////////////////////////
			// Option 1
			string enemyType = $"{2*4} hahahaha";
			Console.WriteLine($"{enemyType}");

			if (enemyType == "Dragon") //Dont do this! the program has to compare letter by letter. only use text when the player is going to see it.
			{

			}
			///////////////////////////////////////////////////////
			///
			int enemyTypeInt = 0;
			// Option 2
			switch (enemyTypeInt)
			{
				case 0:
					Console.WriteLine("That is an Orc");
					break;
				case 1:
					Console.WriteLine("That is a Dragon");
					break;
				case 2:
					Console.WriteLine("That is a Mermaid");
					break;
				

			}
			///////////////////////////////////////////////////////

			// Option 3
			EnemyTypeEnum enemyTypeEnum = EnemyTypeEnum.Orc;
			switch (enemyTypeEnum)
			{
				case EnemyTypeEnum.Dragon:
					break;
				case EnemyTypeEnum.Skeleton:
					break;

			}

			int playerNumber = 0;

			switch (int.Parse(Console.ReadLine())) //on c# it can be any variable, on c++ it had to be integers
			{
				case 0: //if code is here it does not "fall through" to the next case
				case 1:
					{ // scope allows you to delete variables between cases.
						Console.WriteLine("Your playernumber is 0 or 1");
					}
					break;
				case 2:
					Console.WriteLine("Your playernumber is 2");
					break;
				default:
					Console.WriteLine("uhhhhhh");
					break;

			}
		}
	}
}
