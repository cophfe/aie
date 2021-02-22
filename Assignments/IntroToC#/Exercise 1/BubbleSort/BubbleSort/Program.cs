using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleSort
{
	class Program
	{
		static void Main(string[] args)
		{
			//repeats unless told to stop
			while (true)
			{
				//user is asked to give value for how many numbers will be sorted
				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.WriteLine("How many random numbers should be sorted?");
				Console.ResetColor();
				int[] arrayValue;
				Random rand = new Random();
				//user input is either parsed and used as array length or is defaulted to 10
				if (int.TryParse(Console.ReadLine(), out int amount))
				{
					arrayValue = new int[amount];
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Value could not be accepted, defaulting to 10");
					Console.ResetColor();
					arrayValue = new int[10];
				}

				//user is then shown the unsorted values
				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.WriteLine("\nUnsorted:");
				Console.ResetColor();
				for (int i = 0; i < arrayValue.Length; ++i)
				{
					//random values assigned to array
					arrayValue[i] = rand.Next(1, arrayValue.Length);
					//each value is written in console (with comma after if it isn't the last value)
					Console.Write(arrayValue[i]);
					if (i != arrayValue.Length - 1)
					{
						Console.Write(", ");
					}
				}
				// BubbleSort: number of comparisons is equal to (number of elements - 1)^2
				// BubSort function sorts an array in ascending order using bubble sort algorythim
				BubSort(arrayValue); //dont need ref for array to change value for reasons we don't know yet

				//Write the sorted values in console
				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.WriteLine("\n\nSorted:");
				Console.ResetColor();
				for (int i = 0; i < arrayValue.Length; ++i)
				{
					Console.Write(arrayValue[i]);
					if (i != arrayValue.Length - 1)
					{
						Console.Write(", ");
					}
				}

				//ask user if they want to restart the process. if they don't answer "y" then it exits
				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.WriteLine("\n\nTest Again? (y/n)");
				Console.ResetColor();

				if (Console.ReadLine() != "y"){
					break; 
				}
				Console.Clear();
			}
		}
		static void BubSort(int[] unsorted)
		{
			//loops through array until it is sorted by number size
			bool sorted = false;
			while (!sorted)
			{
				//assumes array is sorted. If it is not, then sorted will be changed to false while sorting and the code will go again 
				sorted = true;
				for(int i = 0; i < unsorted.Length - 1;++i)
				{
					//use greater than for ascending
					if (unsorted[i] > unsorted[i + 1])
					{
						sorted = false;
						int val = unsorted[i];
						unsorted[i] = unsorted[i+1];
						unsorted[i + 1] = val;
					}
				}
			}
			 
		}
	}
}
