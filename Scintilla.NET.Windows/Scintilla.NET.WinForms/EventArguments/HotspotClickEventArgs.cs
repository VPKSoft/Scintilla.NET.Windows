using System;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.EventArguments;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.HotspotClick" />, <see cref="Scintilla.HotspotDoubleClick" />,
/// and <see cref="Scintilla.HotspotReleaseClick" /> events.
/// </summary>
public class HotspotClickEventArgs<TKeys> : HotspotClickEventArgsBase<TKeys>
    where TKeys : Enum
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HotspotClickEventArgs{TKeys}" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the click.</param>
    /// <param name="bytePosition">The zero-based byte position of the clicked text.</param>
    public HotspotClickEventArgs(
        IScintillaApi scintilla, 
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        TKeys modifiers, 
        int bytePosition) : base(scintilla, lineCollectionGeneral, modifiers, bytePosition)
    {
    }
}