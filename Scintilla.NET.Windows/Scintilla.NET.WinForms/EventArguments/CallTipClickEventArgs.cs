using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.EventArguments;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.CallTipClick" /> event.
/// </summary>
public class CallTipClickEventArgs: CallTipClickEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DwellEventArgs" /> class.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla" /> control that generated this event.</param>
    /// /// <param name="callTipClickType">Type of the call tip click.</param>
    public CallTipClickEventArgs(
        IScintillaApi scintilla, CallTipClickType callTipClickType) : base(scintilla, callTipClickType)
    {
    }
}