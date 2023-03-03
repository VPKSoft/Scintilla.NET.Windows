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

using System.IO;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace ScintillaNet.Wpf;

/// <summary>
/// Helper methods for Scintilla WPF interaction.
/// </summary>
public static class WpfHelpers
{
    /// <summary>
    /// Converts the specified <see cref="Color"/> into RGBA integer value.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted RGBA integer value.</returns>
    public static int ColorToRgba(Color value)
    {
        var r = value.R;
        var g = value.G << 8;
        var b = value.B << 16;
        var intColor = r | g | b | (value.A << 24);
        return intColor;
    }

    /// <summary>
    /// Converts the specified RGBA integer value into a <see cref="Color"/>
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <returns>A color converted from the RGBA integer value.</returns>
    public static Color FromIntColor(int value)
    {
        var a = (byte)((value >> 24) & 0xFF); // Opacity
        var b = (byte)((value >> 16) & 0xFF);
        var g = (byte)((value >> 8) & 0xFF);
        var r = (byte)(value & 0xFF);

        return Color.FromArgb(a, r, g, b);
    }

    /// <summary>
    /// Converts a <see cref="BitmapImage"/> into a <see cref="Bitmap"/>.
    /// </summary>
    /// <param name="bitmapImage">The bitmap image to convert.</param>
    /// <returns>The converted bitmap.</returns>
    public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
    {
        // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

        using var outStream = new MemoryStream();
        BitmapEncoder enc = new BmpBitmapEncoder();
        enc.Frames.Add(BitmapFrame.Create(bitmapImage));
        enc.Save(outStream);
        var bitmap = new Bitmap(outStream);

        return new Bitmap(bitmap);
    }

    /// <summary>
    /// Converts a <see cref="Bitmap"/> into a ARGB byte array.
    /// </summary>
    /// <param name="image">The bitmap image to convert.</param>
    /// <returns>The bitmap converted into ARGB <see cref="byte"/>[] array.</returns>
    public static byte[] BitmapToArgb(Bitmap image)
    {
        // This code originally used Image.LockBits and some fast byte copying, however, the endianness
        // of the image formats was making my brain hurt. For now I'm going to use the slow but simple
        // GetPixel approach.

        var bytes = new byte[4 * image.Width * image.Height];

        var i = 0;
        for (var y = 0; y < image.Height; y++)
        {
            for (var x = 0; x < image.Width; x++)
            {
                var color = image.GetPixel(x, y);
                bytes[i++] = color.R;
                bytes[i++] = color.G;
                bytes[i++] = color.B;
                bytes[i++] = color.A;
            }
        }

        return bytes;
    }

    /// <summary>
    /// Converts a WinForms <see cref="System.Drawing.Color"/> into a WPF <see cref="Color"/>
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>The resulting converted <see cref="Color"/> value.</returns>
    public static Color FromWinForms(System.Drawing.Color color) => Color.FromArgb(color.A, color.R, color.G, color.B);

    /// <summary>
    /// Converts a WPF <see cref="Color"/> into a WinForms <see cref="System.Drawing.Color"/>.
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>The resulting converted <see cref="Color"/> value.</returns>
    public static System.Drawing.Color ToWinForms(Color color) =>
        System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
}