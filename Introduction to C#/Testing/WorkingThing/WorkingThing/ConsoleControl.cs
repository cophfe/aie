using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;



class ConsoleControl
{

    //----------------------------------------------------------------------------
    //-----------------------MANAGE CONSOLE INPUT---------------------------------
    //----------------------------------------------------------------------------

    static public Coord mousePos = new Coord();

    [STAThread]
    public static void ReadInput(IntPtr inputHandle, InputRecord inputRecord = new InputRecord(), uint numberOfEventsRead = 0)
    {
        PInvoke.ReadConsoleInput(inputHandle, ref inputRecord, 128, ref numberOfEventsRead);

        switch (inputRecord.eventType)
        {
            case EventType.KEY_EVENT: //occurs when a key is pressed or let go
                KeyInput(inputRecord.eventRecord.keyEvent);
                break;
            case EventType.MOUSE_EVENT: //occurs when mouse is used
                MouseInput(inputRecord.eventRecord.mouseEvent);
                break;
            case EventType.WINDOW_BUFFER_SIZE_EVENT: //occurs when window buffer size is changed or set
                WindowsBufferSizeInput(inputRecord.eventRecord.windowBufferSizeEvent);
                break;
            case EventType.MENU_EVENT: //occurs when menu is used
                MenuInput(inputRecord.eventRecord.menuEvent);
                break;
            case EventType.FOCUS_EVENT: //occurs when window focus is changed
                FocusInput(inputRecord.eventRecord.focusEvent);
                break;
        }
    }

    static void MouseInput(MouseEventRecord mouseER)
    {
        if (mouseER.dwEventFlags == MouseEventFlags.MOUSE_MOVED)
        {
            mousePos = new Coord(mouseER.dwMousePosition.X, mouseER.dwMousePosition.Y);
        }
            
           
    }

    static void KeyInput(KeyEventRecord keyER)
    {
            
    }

    static void MenuInput(MenuEventRecord menuER)
    {
        //ignored
    }

    static void WindowsBufferSizeInput(WindowBufferSizeEventRecord windowER)
    {
        //ignored
    }

    static void FocusInput(FocusEventRecord focusER)
    {
        //ignored
    }

    //----------------------------------------------------------------------------
    //-----------------------------MANAGE WINDOW SIZE-----------------------------
    //----------------------------------------------------------------------------

    public static void SetFullscreen()
    {
        PInvoke.ShowWindow(PInvoke.GetConsoleWindow(), 3);
        //PInvoke.SetConsoleDisplayMode(PInvoke.GetConsoleWindow(), 2);
    }

    public static void SetWindowed()
    {
        PInvoke.ShowWindow(PInvoke.GetConsoleWindow(), 6);
    }

    //----------------------------------------------------------------------------
    //--------------------------------CHANGE FONT---------------------------------
    //----------------------------------------------------------------------------

    public static void SetCurrentFont(string font, short fontSize)
    {
        IntPtr h = PInvoke.GetStdHandle(-11);

        FontInfo set = new FontInfo
        {
            cbSize = Marshal.SizeOf<FontInfo>(),
            FontIndex = 0,
            FontFamily = 54,
            FontName = font,
            FontWeight = 400,
            FontSize = fontSize
        };

        PInvoke.SetCurrentConsoleFontEx(h, false, ref set);
    }

}

