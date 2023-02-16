using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.EventArguments;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.InsertCheck" /> event.
/// </summary>
public class InsertCheckEventArgs : InsertCheckEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InsertCheckEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where text is being inserted.</param>
    /// <param name="byteLength">The length in bytes of the inserted text.</param>
    /// <param name="text">A pointer to the text being inserted.</param>
    public InsertCheckEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition, 
        int byteLength, 
        nint text) : base(scintilla, lineCollectionGeneral, bytePosition, byteLength, text)
    {
    }
}