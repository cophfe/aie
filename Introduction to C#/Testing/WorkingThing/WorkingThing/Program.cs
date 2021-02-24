using System;


class Program
{
	static void Main(string[] args)
	{
			
		//gets standard input handle
		IntPtr inputHandle = PInvoke.GetStdHandle(-10);
		//disables quick edit mode
		PInvoke.SetConsoleMode(inputHandle, 0x0080);
		//enables input without console.read and enables mouse input
		PInvoke.SetConsoleMode(inputHandle, 0x0008 | 0x0010);
		//sets console font
		ConsoleControl.SetCurrentFont("NSimSun", 5);
		//makes the console fullscreen
		ConsoleControl.SetFullscreen();
		//removes the scroll bar
		Console.BufferHeight = Console.WindowHeight;

		bool running = true;

		while (running)
        {
			ConsoleControl.ReadInput(inputHandle);
        }
            
	}
}

