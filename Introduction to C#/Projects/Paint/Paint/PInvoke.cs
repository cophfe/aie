using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

//All P/Invoke with help from PInvoke.net
class PInvoke
{

	#region Input

	//Gets the standard handle of console
	[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern IntPtr GetStdHandle( //gets the standard handle
		int nStdHandle
		);

	//Reads console input
	[DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)]
	public static extern bool ReadConsoleInput(
		IntPtr hConsoleInput,
		ref InputRecord lpBuffer,
		uint nLength,
		ref uint lpNumberOfEventsRead
		);

	//sets the mode of the console
	[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern bool SetConsoleMode(
		IntPtr hConsoleInput,
		[MarshalAs(UnmanagedType.U4)] uint dwMode
		);

	//Gets the current console mode
	[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern bool GetConsoleMode(
		IntPtr hConsoleInput,
		[MarshalAs(UnmanagedType.U4)] ref uint dwMode
		);

	#endregion 

	#region Move Window

	[DllImport("kernel32.dll")]
	public static extern IntPtr GetConsoleWindow();

	[DllImport("user32.dll")]
	public static extern bool ShowWindow(
		IntPtr hWnd,
		int nCmdShow);


	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr CreateFile(
	string FileName,
	[MarshalAs(UnmanagedType.U4)] uint fileAccess,
	[MarshalAs(UnmanagedType.U4)] uint fileShare,
	IntPtr securityAttributes,
	[MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
	[MarshalAs(UnmanagedType.U4)] int flags,
	IntPtr template);

	#endregion

	#region Font

	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	public static extern bool SetCurrentConsoleFontEx(
		IntPtr hConsoleOutput,
		bool MaximumWindow,
		ref FontInfo ConsoleCurrentFontEx);
	
	#endregion 

	#region Rendering

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool WriteConsoleOutput(
		IntPtr hConsoleOutput,
		CharInfo[] lpBuffer,
		Coord dwBufferSize,
		Coord dwBufferCoord,
		ref SmallRect lpWriteRegion);

	#endregion
	
}

