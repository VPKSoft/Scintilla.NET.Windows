using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.EventArguments;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.Insert" /> and <see cref="Scintilla.Delete" /> events.
/// </summary>
public class ModificationEventArgs : ModificationEventArgsBase
{
    /// <summary>
    /// Gets the number of lines added or removed.
    /// </summary>
    /// <returns>The number of lines added to the document when text is inserted, or the number of lines removed from the document when text is deleted.</returns>
    /// <remarks>When lines are deleted the return value will be negative.</remarks>
    public int LinesAdded { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModificationEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="source">The source of the modification.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where text was modified.</param>
    /// <param name="byteLength">The length in bytes of the inserted or deleted text.</param>
    /// <param name="text">>A pointer to the text inserted or deleted.</param>
    /// <param name="linesAdded">The number of lines added or removed (delta).</param>
    public ModificationEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        ModificationSource source, 
        int bytePosition, 
        int byteLength, 
        nint text, 
        int linesAdded) : base(scintilla, lineCollectionGeneral, source, bytePosition, byteLength, text)
    {
        LinesAdded = linesAdded;
    }
}