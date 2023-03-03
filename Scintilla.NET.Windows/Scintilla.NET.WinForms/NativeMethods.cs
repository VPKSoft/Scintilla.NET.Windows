using System;
using System.Runtime.InteropServices;

namespace ScintillaNet.WinForms;

internal static class NativeMethods
{
    #region Constants
    private const string DLL_NAME_KERNEL32 = "kernel32.dll";
    private const string DLL_NAME_OLE32 = "ole32.dll";
    private const string DLL_NAME_USER32 = "user32.dll";
    #endregion

    #region Callbacks

    internal delegate IntPtr Scintilla_DirectFunction(IntPtr ptr, int iMessage, IntPtr wParam, IntPtr lParam);

    internal delegate IntPtr CreateLexer(string lexerName);

    internal delegate void GetLexerName(nuint index, IntPtr name, IntPtr bufferLength);

    internal delegate IntPtr GetLexerCount();

    internal delegate string LexerNameFromID(IntPtr identifier);

    #endregion Callbacks

    #region Functions

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool CloseClipboard();

    [DllImport(DLL_NAME_KERNEL32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    internal static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool EmptyClipboard();

    [DllImport(DLL_NAME_KERNEL32, EntryPoint = "LoadLibraryW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport(DLL_NAME_KERNEL32, EntryPoint = "RtlMoveMemory", SetLastError = true)]
    internal static extern void MoveMemory(IntPtr dest, IntPtr src, int length);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    internal static extern uint RegisterClipboardFormat(string lpszFormat);

    [DllImport(DLL_NAME_OLE32, ExactSpelling = true)]
    internal static extern int RevokeDragDrop(IntPtr hwnd);

    [DllImport(DLL_NAME_USER32, EntryPoint = "SendMessageW", CharSet = CharSet.Unicode, SetLastError = true)]
    internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    internal static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

    [DllImport(DLL_NAME_USER32, SetLastError = true)]
    internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    #endregion Functions
}
