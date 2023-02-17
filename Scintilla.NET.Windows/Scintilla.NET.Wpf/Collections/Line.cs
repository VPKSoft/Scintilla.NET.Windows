using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.Wpf.Collections;

/// <summary>
/// Represents a line of text in a <see cref="Scintilla" /> control.
/// </summary>
public class Line : LineBase
{
    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Line" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this line.</param>
    /// <param name="styleCollectionGeneral">A reference to Scintilla's style collection.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="markerCollectionGeneral">A reference to Scintilla's marker collection.</param>
    /// <param name="index">The index of this line within the <see cref="LineCollection" /> that created it.</param>
    public Line(IScintillaApi scintilla,
        IScintillaStyleCollectionGeneral styleCollectionGeneral,
        IScintillaLineCollectionGeneral lineCollectionGeneral,
        IScintillaMarkerCollectionGeneral markerCollectionGeneral,
        int index) : base(scintilla, styleCollectionGeneral, lineCollectionGeneral, markerCollectionGeneral, index)
    {
    }

    #endregion Constructors
}