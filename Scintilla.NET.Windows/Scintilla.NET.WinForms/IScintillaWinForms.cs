#region License
/*
MIT License

Copyright(c) 2023 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System.Drawing;
using System.Windows.Forms;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.Abstractions.Interfaces.Methods;
using ScintillaNet.WinForms.Collections;
using ScintillaNet.WinForms.EventArguments;

namespace ScintillaNet.WinForms;

/// <summary>
/// Interface for the Scintilla WinForms control.
/// Implements the <see cref="IScintillaWinFormsCollections" />
/// Implements the <see cref="IScintillaProperties{TColor}" />
/// Implements the <see cref="IScintillaProperties" />
/// Implements the <see cref="IScintillaMethods" />
/// Implements the <see cref="IScintillaMethodsColor{TColor}" />
/// Implements the <see cref="IScintillaMethodsKeys{TKeys}" />
/// Implements the <see cref="IScintillaMethodsImage{TImage}" />
/// Implements the <see cref="IScintillaWinFormsEvents" />
/// </summary>
/// <seealso cref="IScintillaWinFormsCollections" />
/// <seealso cref="IScintillaProperties{TColor}" />
/// <seealso cref="IScintillaProperties" />
/// <seealso cref="IScintillaMethods" />
/// <seealso cref="IScintillaMethodsColor{TColor}" />
/// <seealso cref="IScintillaMethodsKeys{TKeys}" />
/// <seealso cref="IScintillaMethodsImage{TImage}" />
/// <seealso cref="IScintillaWinFormsEvents" />
public interface IScintillaWinForms: 
    IScintillaWinFormsCollections,
    IScintillaProperties<Color>,
    IScintillaProperties,
    IScintillaMethods,
    IScintillaMethodsColor<Color>,
    IScintillaMethodsKeys<Keys>,
    IScintillaMethodsImage<Image>,
    IScintillaWinFormsEvents,
    IScintillaEvents
{
}

/// <summary>
/// An interface for the Scintilla WinForms events.
/// Implements the <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUIEventArgs,TSCNotificationEventArgs,TAutoCSelectionChangeEventArgs}" />
/// </summary>
/// <seealso cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUIEventArgs,TSCNotificationEventArgs,TAutoCSelectionChangeEventArgs}" />
public interface IScintillaWinFormsEvents : IScintillaEvents<Keys, AutoCSelectionEventArgs, BeforeModificationEventArgs, ModificationEventArgs, ChangeAnnotationEventArgs, CharAddedEventArgs, DoubleClickEventArgs, DwellEventArgs, CallTipClickEventArgs, HotspotClickEventArgs<Keys>, IndicatorClickEventArgs, IndicatorReleaseEventArgs, InsertCheckEventArgs, MarginClickEventArgs, NeedShownEventArgs, StyleNeededEventArgs, UpdateUIEventArgs, SCNotificationEventArgs, AutoCSelectionChangeEventArgs>
{

}

/// <summary>
/// An interface for the Scintilla WinForms collections.
/// Implements the <see cref="IScintillaApi{TMarkerCollection,TStyleCollection,TIndicatorCollection,TLineCollection,TMarginCollection,TSelectionCollection,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TImage,TColor}" />
/// </summary>
/// <seealso cref="IScintillaApi{TMarkerCollection,TStyleCollection,TIndicatorCollection,TLineCollection,TMarginCollection,TSelectionCollection,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TImage,TColor}" />
public interface IScintillaWinFormsCollections : IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection,
    LineCollection, MarginCollection,
    SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color>
{

}