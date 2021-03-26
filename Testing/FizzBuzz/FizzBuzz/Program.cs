using System;

namespace FizzBuzz
{
	class Program
	{
		static void Main(string[] args)
		{
			const int n = 15; // 1 to 10
			string[] fizzBuzz = { "Fizz", "Buzz", "FizzBuzz" };
			string fB;
			int fizz = 3;
			int buzz = 5;
			for (int i = 1; i < n + 1; i++)
			{
				fB = "";
				fizz--;
				buzz--;
				
				if (fizz == 0)
				{
					fB += fizzBuzz[0];
					fizz = 3;
				}
				if (buzz == 0)
				{
					fB += fizzBuzz[1];
					buzz = 5;
				}
				if (fB == "")
				{
					fB = i.ToString();
				}
				Console.WriteLine(fB);

			}
		}
	}
}
