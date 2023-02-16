using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.EventArguments;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Provides data for the <see cref="Scintilla.UpdateUi" /> event.
/// </summary>
// ReSharper disable once InconsistentNaming, part of the API
public class UpdateUIEventArgs : UpdateUIEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUIEventArgs" /> class.
    /// </summary>
    /// <param name="scintillaApi">The <see cref="IScintillaApi" /> control interface that generated this event.</param>
    /// <param name="change">A bitwise combination of <see cref="UpdateChange" /> values specifying the reason to update the UI.</param>
    public UpdateUIEventArgs(IScintillaApi scintillaApi, UpdateChange change) : base(scintillaApi, change)
    {
    }
}