using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This amazing machine will allow you to do some basic maths!");

			while (true)
			{
				Console.WriteLine("Enter first number: ");
				float firstNumber;
				if (!float.TryParse(Console.ReadLine(), out firstNumber))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("That's not a number... it's broken now. Well done.");
					Console.ReadKey();
					break;
				}

				Console.WriteLine("Enter operator: ");
				char symbol;
				if (!char.TryParse(Console.ReadLine(), out symbol))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("That's not a operator... it's broken now. Well done.");
					Console.ReadKey();
					break;
				}

				Console.WriteLine("Enter second number: ");
				float secondNumber;
				if (!float.TryParse(Console.ReadLine(), out secondNumber))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("That's not a number... it's broken now. Well done.");
					Console.ReadKey();
					break;
				}
				Console.WriteLine(new string('_', Console.WindowWidth));

				Console.WriteLine("Your answer is " +DoMath(firstNumber, symbol, secondNumber));
				Console.WriteLine(new string('_', Console.WindowWidth));
				Console.WriteLine("");
			}
		}

		static string DoMath(float first, char sym, float second)
		{
			float solution;
			if(sym == '*')
				solution = first * second;
			else if(sym == '/')
				solution = first / second;
			else if(sym == '+')
				solution = first + second;
			else if(sym == '-')
				solution = first - second;
			else
				return "[Operator not supported]";
			return solution.ToString();
		}
	}
}
