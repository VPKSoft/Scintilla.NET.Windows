﻿using System.Windows.Media.Imaging;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using Color = System.Windows.Media.Color;

namespace ScintillaNet.Wpf.Collections;

/// <summary>
/// An immutable collection of markers in a <see cref="Scintilla" /> control.
/// </summary>
public class MarkerCollection : MarkerCollectionBase<Marker, BitmapImage, Color>
{
    /// <summary>
    /// Gets a <see cref="Marker" /> object at the specified index.
    /// </summary>
    /// <param name="index">The marker index.</param>
    /// <returns>An object representing the marker at the specified <paramref name="index" />.</returns>
    /// <remarks>Markers 25 through 31 are used by Scintilla for folding.</remarks>
    public override Marker this[int index]
    {
        get
        {
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Marker(ScintillaApi, index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkerCollection" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that created this collection.</param>
    public MarkerCollection(IScintillaApi scintilla) : base(scintilla)
    {

    }
}