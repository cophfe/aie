using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    
    static string a;
    static int i = 0;
    static Random rand = new Random();
    static void Main(string[] args)
    {
        a = "Hello Friend!";
        
        timer = new System.Timers.Timer(200);
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;
        timer.Enabled = true;

        Console.ReadKey();

    }
    static System.Timers.Timer timer;
    //static Random rand = new Random();
    static void WriteOutLine(string line, int millisecondsBetween)
	{
        timer = new System.Timers.Timer(millisecondsBetween);
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

     static void OnTimedEvent(Object src, ElapsedEventArgs e)
    {
        if (i < a.Length)
        {
            int b = rand.Next(1, 15); //never black
            Console.ForegroundColor = (ConsoleColor)b;
            //not changing background colour cuz it is impossible to read even when made to always have differing colours
            Console.Write(a.ElementAt<char>(i));
        }
        i++;
        if (i == a.Length)
        {
            timer.Interval = 500;
            
        }
        if (i == a.Length + 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n(Press anything to exit)");
            timer.Stop();
            timer.Dispose();
        }

    }   
}

