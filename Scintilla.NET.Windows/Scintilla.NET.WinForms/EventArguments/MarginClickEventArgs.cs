using System;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.EventArguments;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.MarginClick" /> event.
/// </summary>
public class MarginClickEventArgs<TKeys> : MarginClickEventArgsBase<TKeys>
    where TKeys : Enum
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MarginClickEventArgs{TKeys}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the margin click.</param>
    /// <param name="bytePosition">The zero-based byte position within the document where the line adjacent to the clicked margin starts.</param>
    /// <param name="margin">The zero-based index of the clicked margin.</param>
    public MarginClickEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        TKeys modifiers, 
        int bytePosition, 
        int margin) : base(scintilla, lineCollectionGeneral, modifiers, bytePosition, margin)
    {
    }
}