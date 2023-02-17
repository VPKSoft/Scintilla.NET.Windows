using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using ScintillaNet.Abstractions.Interfaces.Collections;
using static ScintillaNet.Abstractions.ScintillaConstants;
using Color = System.Windows.Media.Color;

namespace ScintillaNet.Wpf.Collections;

/// <summary>
/// Represents an indicator in a <see cref="Scintilla" /> control.
/// </summary>
public class Indicator : IndicatorBase<Color>
{
    #region Properties

    /// <summary>
    /// Gets or sets the color used to draw an indicator.
    /// </summary>
    /// <returns>The Color used to draw an indicator. The default varies.</returns>
    /// <remarks>Changing the <see cref="ForeColor" /> property will reset the <see cref="HoverForeColor" />.</remarks>
    /// <seealso cref="HoverForeColor" />
    public override Color ForeColor
    {
        get
        {
            var color = ScintillaApi.DirectMessage(SCI_INDICGETFORE, new IntPtr(Index)).ToInt32();
            return WpfHelpers.FromIntColor(color);
        }
        set
        {
            var color = WpfHelpers.ColorToRgba(value);
            ScintillaApi.DirectMessage(SCI_INDICSETFORE, new IntPtr(Index), new IntPtr(color));
        }
    }

    /// <summary>
    /// Gets or sets the color used to draw an indicator when the mouse or caret is over an indicator.
    /// </summary>
    /// <returns>
    /// The Color used to draw an indicator.
    /// By default, the hover style is equal to the regular <see cref="ForeColor" />.
    /// </returns>
    /// <remarks>Changing the <see cref="ForeColor" /> property will reset the <see cref="HoverForeColor" />.</remarks>
    /// <seealso cref="ForeColor" />
    public override Color HoverForeColor
    {
        get
        {
            var color = ScintillaApi.DirectMessage(SCI_INDICGETHOVERFORE, new IntPtr(Index)).ToInt32();
            return WpfHelpers.FromIntColor(color);
        }
        set
        {
            var color = WpfHelpers.ColorToRgba(value);
            ScintillaApi.DirectMessage(SCI_INDICSETHOVERFORE, new IntPtr(Index), new IntPtr(color));
        }
    }


    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Indicator" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this indicator.</param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    /// <param name="index">The index of this style within the <see cref="IndicatorCollection" /> that created it.</param>
    public Indicator(IScintillaApi scintilla, IScintillaLineCollectionGeneral lineCollectionGeneral, int index) : base(scintilla, lineCollectionGeneral, index)
    {
    }

    #endregion Constructors
}