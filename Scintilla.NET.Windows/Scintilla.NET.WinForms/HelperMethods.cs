using System.Linq;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.WinForms;

/// <summary>
/// Helper methods for the <see cref="Scintilla"/> control.
/// </summary>
public static class HelperMethods
{
    /// <summary>
    /// Gets the folding state of the control as a delimited string containing line indexes.
    /// </summary>
    /// <param name="lines">The line collection of the Scintilla control instance.</param>
    /// <param name="separator">The string to use as a separator.</param>
    /// <returns>The folding state of the control.</returns>
    public static string GetFoldingState<TLine, TLines>(this TLines lines, string separator = ";")
    where TLines : IScintillaLineCollection<TLine>
    where TLine: IScintillaLine
    {
        return string.Join(separator,
            lines.Where(f => !f.Expanded).Select(f => f.Index).OrderBy(f => f).ToArray());
    }

    /// <summary>
    /// Sets the folding state of the state of the control with specified index string.
    /// </summary>
    /// <param name="scintilla">The <see cref="Scintilla"/> control instance.</param>
    /// <param name="lines">The line collection of the Scintilla control instance.</param>
    /// <param name="foldingState">A string containing the folded line indexes separated with the <paramref name="separator"/> to restore the folding.</param>
    /// <param name="separator">The string to use as a separator.</param>
    public static void SetFoldingState<TLine, TLines>(this IScintillaApi scintilla, TLines lines, string foldingState, string separator = ";")
        where TLines : IScintillaLineCollection<TLine>
        where TLine: IScintillaLine
    {
        scintilla.FoldAll(FoldAction.Expand);
        foreach (var index in foldingState.Split(separator).Select(int.Parse))
        {
            if (index < 0 || index >= lines.Count)
            {
                continue;
            }
            lines[index].ToggleFold();
        }
    }
}