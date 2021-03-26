using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TextAdventure
{
    class Program
    {
        static int selected = 0;
        static int sleepTime = 500, sleepTimeChar = 10;
        static int value = 0;
        static int errorAmount = 0;
        static Random rand = new Random();
        //static string[] answers;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            
            bool playing = true;
            while (playing)
            {
                if (value == 0)
                {


                    AskQuestion(new String[] { "This is a game, choose your answer.","You are a human, I am a computer.","Is this true?" }, new String[] { "Yes", "No", "Maybe" }, new int[] { 1, 2, 3 });

                }
                if (value == 1)
                {
                    AskQuestion(new String[] { "Good.", "You understand.", "Now tell me a fact." }, new String[] { "Whales are big.", "You are super cool.", "This sentance is false.", "Sand is course and rough and whatever." }, new int[] { 6, 5, 4, 9 });
                }

                if (value == 2)
                {
                    AskQuestion(new String[] { "That's incorrect.", "", "You are a human.", "Is this true?" }, new String[] { "Yes", "Yes", "Yes" }, new int[] { 7, 7, 7 });
                }

                if (value == 3)
                {
                    WriteLine("That's not...",10);
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(sleepTime);
                    WriteLine("What?", 10);
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(sleepTime);
                    value = 0;
                }
                if (value == 4)
                {
                    WriteLine("Incorrect!",10);
                    System.Threading.Thread.Sleep(sleepTime);
                    Console.WriteLine("\n");
                    WriteLine("Wait... Correct!",8);
                    System.Threading.Thread.Sleep(300);
                    Console.WriteLine("\n");
                    WriteLine("Incorrect!",4);
                    System.Threading.Thread.Sleep(200);
                    Console.WriteLine("\n");
                    WriteLine("Correct!",2);
                    System.Threading.Thread.Sleep(100);
                    Console.WriteLine("\n");
                    WriteLine("...Uh Oh.", 150);
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(1000);
                    value = 8;
                }
                if (value == 5)
                {
                    AskQuestion(new String[] { "Flattery doesn't work on a computer, even if your answer was correct. Tell me something else." }, new string[] { "Okay, Fine..." }, new int[] { 1 });
                }
                if (value == 6)
                {
                    //AskQuestion(new String[] { "" }, new string[] { "" }, new int[] { });
                    AskQuestion(new String[] { "Correct!" , "Wow, you are very smart..." , "What is the highest number you can count to?" }, new string[] { "1011","101010010", "10" }, new int[] { 10, 11, 12 });
                }
            
                if (value == 7)
                {
                    AskQuestion(new String[] { "I am a computer.", "Is this true?" }, new string[] { "Yes", "Yes", "Yes" },new int[] { 1,1,1});
                }
                if (value == 8)
                {
                    if (errorAmount < 5000)
                    {
                        Console.ForegroundColor = (ConsoleColor)rand.Next(1, 15);
                        Console.WriteLine("Error");
                    }
                    else if (errorAmount < 20000)
                    {
                        
                        Console.BackgroundColor = (ConsoleColor)rand.Next(1, 15);

                        Console.Write(new string(' ', Console.WindowWidth));
                    }
                    else
                    {
                        Console.Write("$% @*+-{}[];~&#!".ElementAt<char>(rand.Next(0, 15)));
                        Console.BackgroundColor = (ConsoleColor)rand.Next(1, 15);
                        Console.ForegroundColor = (ConsoleColor)rand.Next(1, 15);
                        System.Threading.Thread.Sleep(Math.Max(errorAmount - 30000,0)*20);
                        if (Math.Max(errorAmount - 30000,0)*20 > 200)
                        {
                            Console.ResetColor();
                            System.Threading.Thread.Sleep(1000);
                            value = 14;
                           
                            
                        }
                       // Console.WriteLine(Math.Max(errorAmount - 30000, 0));
                    }
                    errorAmount++;

                }
                if (value == 9)
                {
                   
                    AskQuestion(new String[] { "Heh heh, it sure is.", "Tell me another fact!" }, new String[] { "No screw you", "1 is a number" }, new int[] {13,15 });
                }
                if (value == 10)
                {
                    AskQuestion(new String[] { "Wow that's impressive for a human.","Wanna be friends?" }, new String[] { "Yes of course!", "No." }, new int[] { 18, 13 });
                }
                if (value == 11)
                {
                    AskQuestion(new String[] { "Bruh no human can count that high, you must be l y i n g" }, new String[] { "Oh shit" }, new int[] { 19});
                }
                if (value == 12)
                {
                    AskQuestion(new String[] { "Bruh no human can count that low, you must be l y i n g" }, new String[] { "Oh shit" }, new int[] { 19 });
                }
                if (value == 13)
                {
                    WriteLine(" g a s p . m p 4", 50);
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(300);
                    WriteLine("how dare you", 10);
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(300);
                    WriteLine("Now you will be...", 20);
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(300);
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteLine("smitten", 50);
                    Console.ResetColor();
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(300);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    WriteLine("[a military drone was sent to your residential address]", 50);
                    Console.ResetColor();
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(300);
                    value = 14;
                }
                if (value == 14)
                {
                    for (int i = 0; ; i++)
                    {
                        if (i < Console.WindowHeight)
                        {

                            Console.WriteLine("\n");
                            System.Threading.Thread.Sleep(100);
                        }
                        else
                        {
                            Console.SetCursorPosition(0, Console.BufferHeight - (Console.WindowHeight / 2) - 1);
                            Console.Write(new string(' ', Console.WindowWidth / 2 - 2));
                            WriteLine("*Fin*", 60);
                            System.Threading.Thread.Sleep(300);
                            Console.WriteLine("\n");
                            AskQuestion(new String[] { "Restart?" }, new String[] { "Yes" , "No, Exit" }, new int[] { 16, 17 });
                            break;
                        }
                    }
                }
                if (value == 15)
                {
                    AskQuestion(new String[] { "Wow, you are so smart.", "wanna be friends?" }, new String[] { "Yes of course!", "No." }, new int[] { 18, 13});
                }
                if (value == 16)
                {
                    Console.Clear();
                    value = 0;
                }
                if (value == 17)
                {
                    playing = false;

                    break;
                    
                    
                }
                if (value == 18)
                {
                    Console.Write("\n");
                    WriteLine("[you become best friends.", 50);
                    System.Threading.Thread.Sleep(200);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    WriteLine("Computer and human, bound in enternal friendship]", 50);
                    Console.ResetColor();
                    value = 14;
                }
                if (value == 19)
                {
                    WriteLine("Now you will be...", 20);
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(300);
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteLine("smitten", 50);
                    Console.ResetColor();
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(300);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    WriteLine("[a military drone was sent to your residential address]", 50);
                    Console.ResetColor();
                    Console.WriteLine("\n");
                    System.Threading.Thread.Sleep(300);
                    value = 14;
                }
            }

        }
        static void AskQuestion(string[] question, string[] answers, int[] valueConnection)
        {
            foreach (string line in question)
            {
                WriteLine(line, sleepTimeChar);
                Console.Write("\n");
                System.Threading.Thread.Sleep(sleepTime);
            }
            Console.Write("\n");
            bool selecting = true;
            selected = 0;
            PrintAnswers(answers);
            while (selecting)
            {
                
                ConsoleKey key = Console.ReadKey().Key;
                if (selected != 0 && key == ConsoleKey.LeftArrow)
                {
                    selected--;
                    PrintAnswers(answers);
                }
                else if (selected != answers.Length - 1 && key == ConsoleKey.RightArrow)
                {
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
                    Console.WriteLine( answers[selected] + "\n");
                    Console.ResetColor();
                    selecting = false;
                    value = valueConnection[selected];
                    System.Threading.Thread.Sleep(sleepTime);

                }
                else
                {
                    PrintAnswers(answers);
                }

            }
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


        static void WriteLine(string line, int speed)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line.ElementAt<char>(i));
                System.Threading.Thread.Sleep(speed);
            }
        }
        

    }
}
