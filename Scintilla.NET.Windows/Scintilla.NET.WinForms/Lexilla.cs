#region License
/*
MIT License

Copyright(c) 2023 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ScintillaNet.Abstractions;

namespace ScintillaNet.WinForms;

/// <summary>
/// Linux handler for the Scintilla's Lexilla library.
/// Implements the <see cref="ILexilla" />
/// </summary>
/// <seealso cref="ILexilla" />
public class Lexilla: ILexilla
{
    /// <inheritdoc cref="ILexilla.LexerCount"/>
    public int LexerCount => GetLexerCount();

    /// <summary>
    /// Gets the name of the lexer.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>System.String.</returns>
    public string GetLexerName(uint index)
    {
        var pointer = Marshal.AllocHGlobal(1024);
        GetLexerName(index, pointer, new IntPtr(1024));
        return Marshal.PtrToStringAnsi(pointer) ?? string.Empty;
    }

    /// <summary>
    /// Creates a lexer with the specified name.
    /// </summary>
    /// <param name="lexerName">The name of the lexer to create.</param>
    /// <returns>A <see cref="IntPtr"/> containing the lexer interface pointer.</returns>
    public IntPtr CreateLexer(string lexerName)
    {
        var pointer = Marshal.StringToHGlobalAnsi(lexerName);
        var result = CreateLexerDll(pointer);
        Marshal.FreeHGlobal(pointer);
        return result;
    }

    [DllImport("Lexilla.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, EntryPoint = "CreateLexer")]
    private static extern IntPtr CreateLexerDll(IntPtr lexerName);


    [DllImport("Lexilla.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern int GetLexerCount();

    [DllImport("Lexilla.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    private static extern void GetLexerName(nuint index, IntPtr name, IntPtr bufferLength);

    [DllImport("Lexilla.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public static extern string LexerNameFromId(IntPtr identifier);

    /// <summary>
    /// Gets the lexer names contained in the Lexilla library.
    /// </summary>
    /// <returns>An IEnumerable&lt;System.String&gt; value with the lexer names.</returns>
    public static IEnumerable<string> GetLexerNames()
    {
        var count = GetLexerCount();
        for (var i = 0; i < count; i++)
        {
            yield return GetLexerName(i);
        }
    }

    /// <summary>
    /// Gets the name of the lexer specified by an index number.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The name of the lexer if one was found with the specified index; <c>null</c> otherwise.</returns>
    public static string GetLexerName(int index)
    {
        var pointer = Marshal.AllocHGlobal(1024);
        try
        {
            GetLexerName((uint) index, pointer, new IntPtr(1024));
            return Marshal.PtrToStringAnsi(pointer);
        }
        finally
        {
            Marshal.FreeHGlobal(pointer);
        }
    }
}