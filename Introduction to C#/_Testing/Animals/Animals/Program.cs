using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
	class Program
	{
		static void Main(string[] args)
		{
			//set stats of animals
			Animal[] animalList = new Animal[5];
			animalList[0] = new Animal("Puppy", 1000, 1000);
			animalList[1] = new Animal("Goat", 2, 10);
			animalList[2] = new Animal("Spoider", 50, 2);
			animalList[3] = new Animal("Cat", 40, 100 * 9);
			animalList[4] = new Animal("Fish", 20, 20);

			//bubble sort array
			Sort(animalList);

			//print values for each animal
			for (int i = 0; i < animalList.Length; ++i)
			{
				animalList[i].Print();
			}
			Console.ReadLine();
		}

		static void Sort(Animal[] list)
		{
			bool sorted = false;
			while(!sorted)
			{
				sorted = true;
				for (int i = 0; i < list.Length - 1; i++)
				{
					if (list[i].GetSpeed() > list[i+1].GetSpeed())
					{
						sorted = false;
						Animal sp = list[i];
						list[i] = list[i + 1];
						list[i + 1] = sp ;
					}
				}
			}
		}
	}
}
