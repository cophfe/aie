using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ReadingWriting
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Coord
	{
		public short X;
		public short Y;

		public Coord(short X, short Y)
		{
			this.X = X;
			this.Y = Y;
		}
	};
	[StructLayout(LayoutKind.Explicit)]
	public struct CharUnion
	{
		[FieldOffset(0)]
		public char UnicodeChar;
		[FieldOffset(0)]
		public byte AsciiChar;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct KeyEventRecord
	{
		public bool bKeyDown;
		public ushort wRepeatCount;
		public ushort wVirtualKeyCode;
		public ushort wVirtualScanCode;
		public CharUnion charUnion; 
		public uint dwControlKeyState;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct FocusEventRecord
	{
		public bool bSetFocus;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct WindowBufferSizeEventRecord
	{
		public Coord dwSize;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct MenuEventRecord
	{
		public uint dwCommandId;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct MouseEventRecord
	{
		public Coord dwMousePosition;
		public uint dwButtonState;
		public uint dwControlKeyState;
		public uint dwEventFlags;
	}
	//[StructLayout(LayoutKind.Explicit)]
	//public struct InputUnion
	//{
		
	//}

	[StructLayout(LayoutKind.Explicit)]
	public struct InputRecord
	{
		[FieldOffset(0)]
		public uint eventType;
		[FieldOffset(4)]
		public KeyEventRecord keyEvent;
		[FieldOffset(4)]
		public MouseEventRecord mouseEvent;
		[FieldOffset(4)]
		public WindowBufferSizeEventRecord windowBufferSizeEvent;
		[FieldOffset(4)]
		public MenuEventRecord menuEvent;
		[FieldOffset(4)]
		public FocusEventRecord focusEvent;
	}


	
	class ConsoleMouse
	{
		[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern SafeFileHandle GetStdHandle( //gets the standard handle
			int nStdHandle
			);
		//[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		//static extern SafeFileHandle CreateFile(
		//   string fileName,
		//   [MarshalAs(UnmanagedType.U4)] uint fileAccess,
		//   [MarshalAs(UnmanagedType.U4)] uint fileShare,
		//   IntPtr securityAttributes,
		//   [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
		//   [MarshalAs(UnmanagedType.U4)] int flags,
		//   IntPtr template);

		[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern bool ReadConsoleInput(
			SafeFileHandle hConsoleInput, //www.pinvoke.net says this should be an IntPtr
			out InputRecord[] lpBuffer,
			[MarshalAs(UnmanagedType.U4)] uint nLength,
			out uint lpNumberOfEventsRead
			);

		[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern bool SetConsoleMode(
			SafeFileHandle hConsoleInput,
			[MarshalAs(UnmanagedType.U4)] uint dwMode
			);
		[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern bool GetConsoleMode(
			SafeFileHandle hConsoleInput,
			[MarshalAs(UnmanagedType.U4)] ref uint dwMode
			);

		[DllImport("user32.dll")]
		static extern bool GetCursorPos(
			out Point lpPoint
			);
		[StructLayout(LayoutKind.Sequential)]
		public struct Point
		{
			public int X;
			public int Y;
		}

		[STAThread]
		public static void Read()
		{
			// \/ is valid but also wrong
			//SafeFileHandle h = CreateFile("CONIN$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero); // CONIN$ is input to console, CONOUT$ is screen output and error output
			SafeFileHandle h = GetStdHandle(-10); //is correct for some reason
			//Console.WriteLine(h.IsInvalid);

			InputRecord[] iR = new InputRecord[20];
			//iR[0] = new InputRecord();
			UInt32 i = 0;
			SetConsoleMode(h, 0x0008); //working probably (returns true)

			Vector2 coord;
			//Console.WriteLine(GetConsoleMode(h, ref i));
			//Console.WriteLine(i);
			//Console.ReadKey();
			Point p;
			
			
			//Console.ReadLine();
			while (true)
			{
				iR[0] = new InputRecord();
				Console.SetCursorPosition(0, 0);
				ReadConsoleInput(h, out iR, (uint)(iR.Length-1), out i);
				//if(iR != null)
				//{
				//	Console.WriteLine(iR[0].input.keyEvent.charUnion.UnicodeChar);
				//}


				//coord = new Vector2(iR[0].input.mouseEvent.dwMousePosition.X, iR[0].input.mouseEvent.dwMousePosition.X);
				//Console.WriteLine($"{coord.X},{coord.Y}");

				//GetCursorPos(out p);// gets cursor position on entire screen
				//Console.WriteLine($"{p.X},{p.Y}");


			}
			
		}
	}
}
