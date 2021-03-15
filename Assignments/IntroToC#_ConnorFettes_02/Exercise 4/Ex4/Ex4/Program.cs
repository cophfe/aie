using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ex4
{
	class Program
	{
		
		static void Main(string[] args)
		{
			//Get the time, in seconds, since the epoch.
			long unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

			//Store a variable to be set to the time since the program was last opened.
			long timePassed = 0;

			//Check if the storage file exists. if it does not, create it.
			if (!File.Exists("stored.txt"))
			{
				//Create the empty text file then close it so that the streamwriter can access it.
				File.Create("stored.txt").Close();
			}
			//If it does exist, read it.
			else
				//"using" keyword automatically calls dispose at the end of its scope.
				//Create a stream reader to read the contents of stored.txt.
				using (StreamReader r = new StreamReader("stored.txt"))
				{
					//Calculate the time passed since the last time the file was opened
					timePassed = unixTime - int.Parse(r.ReadLine());
					//Close the file so that the streamwriter can access it.
					r.Close();
				}

			//Create a stream writer to write to stored.txt
			using (StreamWriter w = new StreamWriter("stored.txt"))
			{
				w.WriteLine(unixTime);
				w.WriteLine(timePassed);
				w.Close();
			}
			
		}
	}
}
