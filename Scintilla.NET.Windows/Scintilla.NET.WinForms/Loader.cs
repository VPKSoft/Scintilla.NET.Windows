using System.Text;
using ScintillaNet.Abstractions.Classes;

namespace ScintillaNet.WinForms;

internal sealed class Loader : LoaderBase
{
    /// <inheritdoc />
    public Loader(nint ptr, Encoding encoding) : base(ptr, encoding)
    {
    }
}