using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleList
{
    class Program
    {
        static void Main(string[] args)
        {
            // lists have a variable size, unlike an array:  just consider it a variable array
            List<string> textList = new List<string>(100);

            //adding to the end is fast, inserting is slower. Is best to define a size for speed
            //arrays are faster so some programmers use a program to log the maximum size of each list. before releasing the app they turn every list into an array.
            //REMOVING DOESN'T SHRINK THE LIST IN MEMORY. USE TRIMEXCESS TO SHRINK
            string[] myArray = new string[3];
            Dictionary<int, string> myDictionary = new Dictionary<int, string>();

            myArray[0] = "Cat";
            myDictionary.Add(0, "Cat");

            myArray[0] = "Dog";
            myDictionary[0] = "Cat";

            Dictionary<string, string> myDictionaryString = new Dictionary<string, string>();
            myDictionaryString.Add("Cat", "Dog");
            myDictionaryString["Cat"] = "Carp";

            while (true)
            {
                Console.WriteLine("Type something:");

                string text = Console.ReadLine();
                textList.Add(text);

                foreach (string t in textList)
                {
                    Console.Write($" {t} ");
                }
                Console.Write('\n');

                textList.RemoveAll(MySearch);
            }
        }

        static bool MySearch(string text)
        {
            return text.Contains("hi");
        }
    }
}
