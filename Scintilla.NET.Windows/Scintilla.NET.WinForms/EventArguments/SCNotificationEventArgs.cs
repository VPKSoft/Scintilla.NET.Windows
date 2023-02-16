using ScintillaNet.Abstractions.EventArguments;
using static ScintillaNet.Abstractions.Classes.ScintillaApiStructs;

namespace ScintillaNet.WinForms.EventArguments;

/// <summary>
/// Notifications are sent (fired) from the Scintilla control to its container when an event has occurred that may interest the container. This class cannot be inherited.
/// Implements the <see cref="SCNotificationEventArgsBase" />
/// </summary>
/// <seealso cref="SCNotificationEventArgsBase" />
// ReSharper disable once InconsistentNaming, part of the API
public sealed class SCNotificationEventArgs : SCNotificationEventArgsBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SCNotificationEventArgs"/> class.
    /// </summary>
    /// <param name="scn">The Scintilla notification data structure.</param>
    public SCNotificationEventArgs(SCNotification scn) : base(scn)
    {
    }
}