using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.WinForms.Collections;

/// <summary>
/// Represents a selection when there are multiple active selections in a <see cref="Scintilla" /> control.
/// </summary>
public class Selection : SelectionBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Selection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this selection.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="index">The index of this selection within the <see cref="SelectionCollection" /> that created it.</param>
    public Selection(IScintillaApi scintilla, IScintillaLineCollectionGeneral lineCollectionGeneral, int index) : base(scintilla, lineCollectionGeneral, index)
    {
    }
}