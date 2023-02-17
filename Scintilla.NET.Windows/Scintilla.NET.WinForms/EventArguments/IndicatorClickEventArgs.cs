using System;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.EventArguments;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.IndicatorClick" /> event.
/// </summary>
public class IndicatorClickEventArgs<TKeys> : IndicatorClickEventArgsBase<TKeys>
    where TKeys : Enum
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IndicatorClickEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// <param name="modifiers">The modifier keys that where held down at the time of the click.</param>
    public IndicatorClickEventArgs(IScintillaApi scintilla, TKeys modifiers) : base(scintilla, modifiers)
    {
    }
}