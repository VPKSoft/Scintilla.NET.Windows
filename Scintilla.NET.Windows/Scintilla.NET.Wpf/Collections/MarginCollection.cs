using System.ComponentModel;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using static ScintillaNet.Abstractions.ScintillaConstants;
using Color = System.Windows.Media.Color;

namespace ScintillaNet.Wpf.Collections;

/// <summary>
/// An immutable collection of margins in a <see cref="Scintilla" /> control.
/// </summary>
public class MarginCollection : MarginCollectionBase<Margin, Color>
{
    /// <summary>
    /// Gets or sets the number of margins in the <see cref="MarginCollection" />.
    /// </summary>
    /// <returns>The number of margins in the collection. The default is 5.</returns>
    [DefaultValue(SC_MAX_MARGIN + 1)]
    [Description("The maximum number of margins.")]
    public override int Capacity
    {
        get => base.Capacity;

        set => base.Capacity = value;
    }

    /// <summary>
    /// Gets or sets the width in pixels of the left margin padding.
    /// </summary>
    /// <returns>The left margin padding measured in pixels. The default is 1.</returns>
    [DefaultValue(1)]
    [Description("The left margin padding in pixels.")]
    public override int Left
    {
        get => base.Left;

        set => base.Left = value;
    }

    /// <summary>
    /// Gets or sets the width in pixels of the right margin padding.
    /// </summary>
    /// <returns>The right margin padding measured in pixels. The default is 1.</returns>
    [DefaultValue(1)]
    [Description("The right margin padding in pixels.")]
    public override int Right
    {
        get => base.Right;

        set => base.Right = value;
    }

    /// <summary>
    /// Gets a <see cref="Margin" /> object at the specified index.
    /// </summary>
    /// <param name="index">The margin index.</param>
    /// <returns>An object representing the margin at the specified <paramref name="index" />.</returns>
    /// <remarks>By convention margin 0 is used for line numbers and the two following for symbols.</remarks>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Margin this[int index]
    {
        get
        {
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Margin(ScintillaApi, index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MarginCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    public MarginCollection(IScintillaApi scintilla) : base(scintilla)
    {
    }
}