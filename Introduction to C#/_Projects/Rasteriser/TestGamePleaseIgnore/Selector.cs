using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


static class Selector
{
    static int selected = 0;
	public static string Select(params string[] answers)
	{
        int sleepTime = 100;
        bool selecting = true;
        PrintAnswers(answers);
        while (selecting)
        {

            ConsoleKey key = Console.ReadKey().Key;
            if (selected != 0 && key == ConsoleKey.LeftArrow)
            {
                Console.Clear();
                selected--;
                PrintAnswers(answers);
            }
            else if (selected != answers.Length - 1 && key == ConsoleKey.RightArrow)
            {
                Console.Clear();
                selected++;
                PrintAnswers(answers);
            
            }
            else if (key == ConsoleKey.Enter)
            {
                int currentLineCursor = Console.CursorTop;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, currentLineCursor);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(answers[selected] + "\n");
                Console.ResetColor();
                selecting = false;
                return answers[selected];
                

            }
            else
            {
                PrintAnswers(answers);
            }

        }
        return "wow";
    }
    static void PrintAnswers(string[] answers)
    {

        Console.SetCursorPosition(0, Console.CursorTop);

        for (int i = 0; i < answers.Length; i++)
        {
            if (i == selected)
            {
                Console.ForegroundColor = (ConsoleColor)0;
                Console.BackgroundColor = (ConsoleColor)15;
            }
            Console.Write(answers[i]);
            Console.ResetColor();
            if (i != answers.Length - 1)
            {
                Console.Write(" | ");
            }

        }


    }
}
	

