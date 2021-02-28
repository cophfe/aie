using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

#region Input
//FOR CONSOLE INPUT
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
	public Coord(int X, int Y)
	{
		this.X = (short)X;
		this.Y = (short)Y;
	}
};

[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
public struct KeyEventRecord
{
	[FieldOffset(0), MarshalAs(UnmanagedType.Bool)]
	public bool bKeyDown;
	[FieldOffset(4), MarshalAs(UnmanagedType.U2)]
	public ushort wRepeatCount;
	[FieldOffset(6), MarshalAs(UnmanagedType.U2)]
	public VirtualKeys wVirtualKeyCode;
	[FieldOffset(8), MarshalAs(UnmanagedType.U2)]
	public ushort wVirtualScanCode;
	[FieldOffset(10)]
	public char UnicodeChar;
	[FieldOffset(12), MarshalAs(UnmanagedType.U4)]
	public ControlKeyState dwControlKeyState;
}

[StructLayout(LayoutKind.Sequential)]
public struct FocusEventRecord
{
	public uint bSetFocus;
}

public struct WindowBufferSizeEventRecord
{
	public Coord dwSize;

	public WindowBufferSizeEventRecord(short x, short y)
	{
		dwSize = new Coord(x, y);
	}
}

[StructLayout(LayoutKind.Sequential)]
public struct MenuEventRecord
{
	public uint dwCommandId;
}

[StructLayout(LayoutKind.Explicit)]
public struct MouseEventRecord
{
	[FieldOffset(0)]
	public Coord dwMousePosition;
	[FieldOffset(4)]
	public int dwButtonState;
	[FieldOffset(8)]
	public int dwControlKeyState;
	[FieldOffset(12)]
	public MouseEventFlags dwEventFlags;
}


[StructLayout(LayoutKind.Explicit)]
public struct InputRecordUnion
{
	[FieldOffset(0)]
	public KeyEventRecord keyEvent;
	[FieldOffset(0)]
	public MouseEventRecord mouseEvent;
	[FieldOffset(0)]
	public WindowBufferSizeEventRecord windowBufferSizeEvent;
	[FieldOffset(0)]
	public MenuEventRecord menuEvent;
	[FieldOffset(0)]
	public FocusEventRecord focusEvent;
};

[StructLayout(LayoutKind.Sequential)]
public struct InputRecord
{
	public EventType eventType;
	public InputRecordUnion eventRecord;
};
#endregion

#region Move Window
//FOR MOVING CONSOLE WINDOW

[StructLayout(LayoutKind.Sequential)]
public struct Rect
{
	public int Left, Top, Right, Bottom;

	public Rect(int left, int top, int right, int bottom)
	{
		Left = left;
		Top = top;
		Right = right;
		Bottom = bottom;
	}
}
#endregion

#region Font
//FOR FONT CHANGING

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct FontInfo
{
	internal int cbSize;
	internal int FontIndex;
	internal short FontWidth;
	public short FontSize;
	public int FontFamily;
	public int FontWeight;
	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
	public string FontName;
}
#endregion

#region Render
//Render to console

[StructLayout(LayoutKind.Explicit)] //UNUSED SINCE IT CAN BE REPLACED WITH JUST A CHAR
public struct CharUnion
{
	[FieldOffset(0)]
	public char unicodeChar;
	[FieldOffset(0)]
	public byte asciiChar;
}

[StructLayout(LayoutKind.Explicit)]
public struct CharInfo
{
	[FieldOffset(0)] public CharUnion Char;
	[FieldOffset(2)] public short Attributes;
}

[StructLayout(LayoutKind.Sequential)]
public struct SmallRect
{
	public short Left;
	public short Top;
	public short Right;
	public short Bottom;
}
#endregion 


