using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.EventArguments;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.NeedShown" /> event.
/// </summary>
public class NeedShownEventArgs : NeedShownEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NeedShownEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where text needs to be shown.</param>
    /// <param name="byteLength">The length in bytes of the text that needs to be shown.</param>
    public NeedShownEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        int bytePosition, 
        int byteLength) : base(scintilla, lineCollectionGeneral, bytePosition, byteLength)
    {
    }
}