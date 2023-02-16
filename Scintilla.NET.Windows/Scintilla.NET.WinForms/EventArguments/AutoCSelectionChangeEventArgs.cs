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
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.EventArguments;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Provides data for the Scintilla.AutoCSelectionChange event.
/// </summary>
public class AutoCSelectionChangeEventArgs : AutoCSelectionChangeEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutoCSelectionChangeEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>/// 
    /// <param name="text">A pointer to the selected auto-completion text.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where the list was displayed.</param>
    /// <param name="listType">The list type of the user list, or 0 for an auto-completion.</param>    
    public AutoCSelectionChangeEventArgs(
        IScintillaApi scintilla,
        IScintillaLineCollectionGeneral lineCollectionGeneral, 
        IntPtr text, 
        int bytePosition, 
        int listType) : base(scintilla, lineCollectionGeneral, text, bytePosition, listType)
    {
    }
}