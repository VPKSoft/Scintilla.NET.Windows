using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Collections;
using ScintillaNet.Abstractions.Interfaces.Collections;

namespace ScintillaNet.Wpf.Collections;

/// <summary>
/// A multiple selection collection.
/// </summary>
public class SelectionCollection : SelectionCollectionBase<Selection>
{
    /// <summary>
    /// Gets the <see cref="Selection" /> at the specified zero-based index.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="Selection" /> to get.</param>
    /// <returns>The <see cref="Selection" /> at the specified index.</returns>
    public override Selection this[int index]
    {
        get
        {
            index = HelpersGeneral.Clamp(index, 0, Count - 1);
            return new Selection(ScintillaApi, LineCollectionGeneral, index);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionCollection" /> class.
    /// </summary>
    /// <param name="scintilla"></param>
    /// <param name="lineCollectionGeneral">A reference to Scintilla's line collection.</param>
    public SelectionCollection(IScintillaApi scintilla, IScintillaLineCollectionGeneral lineCollectionGeneral) : base(scintilla, lineCollectionGeneral)
    {
    }
}