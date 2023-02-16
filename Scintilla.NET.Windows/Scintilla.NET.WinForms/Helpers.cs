﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Classes;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.WinForms.Collections;
using static ScintillaNet.Abstractions.ScintillaConstants;
using static ScintillaNet.Abstractions.Classes.ScintillaApiStructs;
namespace ScintillaNet.WinForms;

internal static class Helpers
{
    #region Fields

    // ReSharper disable five times InconsistentNaming
    internal static bool registeredFormats;
    internal static uint CF_HTML;
    internal static uint CF_RTF;
    // ReSharper disable twice IdentifierTypo
    internal static uint CF_LINESELECT;
    internal static uint CF_VSLINETAG;

    #endregion Fields

    #region Methods

    public static long CopyTo(this Stream source, Stream destination)
    {
        var buffer = new byte[2048];
        int bytesRead;
        long totalBytes = 0;
        while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
        {
            destination.Write(buffer, 0, bytesRead);
            totalBytes += bytesRead;
        }
        return totalBytes;
    }

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

    public static unsafe byte[] ByteToCharStyles(byte* styles, byte* text, int length, Encoding encoding)
    {
        return HelpersGeneral.ByteToCharStyles(styles, text, length, encoding);
    }

    public static unsafe byte[] CharToByteStyles(byte[] styles, byte* text, int length, Encoding encoding)
    {
        return HelpersGeneral.CharToByteStyles(styles, text, length, encoding);
    }

    internal static void CopyHtml(IScintillaApi scintilla, StyleData[] styles, List<ArraySegment<byte>> styledSegments)
    {
        // NppExport -> NppExport.cpp
        // NppExport -> HTMLExporter.cpp
        // http://blogs.msdn.com/b/jmstall/archive/2007/01/21/html-clipboard.aspx
        // http://blogs.msdn.com/b/jmstall/archive/2007/01/21/sample-code-html-clipboard.aspx
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms649015.aspx

        try
        {
            // Write HTML
            using var ms = new NativeMemoryStream(styledSegments.Sum(s => s.Count));
            using var tw = new StreamWriter(ms, new UTF8Encoding(false));
            const int INDEX_START_HTML = 23;
            const int INDEX_START_FRAGMENT = 65;
            const int INDEX_END_FRAGMENT = 87;
            const int INDEX_END_HTML = 41;

            tw.WriteLine("Version:0.9");
            tw.WriteLine("StartHTML:00000000");
            tw.WriteLine("EndHTML:00000000");
            tw.WriteLine("StartFragment:00000000");
            tw.WriteLine("EndFragment:00000000");
            tw.Flush();

            // Patch header
            var pos = ms.Position;
            ms.Seek(INDEX_START_HTML, SeekOrigin.Begin);
            byte[] bytes;
            ms.Write(bytes = Encoding.ASCII.GetBytes(ms.Length.ToString("D8")), 0, bytes.Length);
            ms.Seek(pos, SeekOrigin.Begin);

            tw.WriteLine("<html>");
            tw.WriteLine("<head>");
            tw.WriteLine(@"<meta charset=""utf-8"" />");
            tw.WriteLine(@"<title>ScintillaNET v{0}</title>", scintilla.GetType().Assembly.GetName().Version!.ToString(3));
            tw.WriteLine("</head>");
            tw.WriteLine("<body>");
            tw.Flush();

            // Patch header
            pos = ms.Position;
            ms.Seek(INDEX_START_FRAGMENT, SeekOrigin.Begin);
            ms.Write(bytes = Encoding.ASCII.GetBytes(ms.Length.ToString("D8")), 0, bytes.Length);
            ms.Seek(pos, SeekOrigin.Begin);
            tw.WriteLine("<!--StartFragment -->");

            // Write the styles.
            // We're doing the style tag in the body to include it in the "fragment".
            tw.WriteLine(@"<style type=""text/css"" scoped="""">");
            tw.Write("div#segments {");
            tw.Write(" float: left;");
            tw.Write(" white-space: pre;");
            tw.Write(" line-height: {0}px;", scintilla.DirectMessage(SCI_TEXTHEIGHT, new IntPtr(0)).ToInt32());
            tw.Write(" background-color: #{0:X2}{1:X2}{2:X2};", (styles[StyleConstants.Default].BackColor >> 0) & 0xFF, (styles[StyleConstants.Default].BackColor >> 8) & 0xFF, (styles[StyleConstants.Default].BackColor >> 16) & 0xFF);
            tw.WriteLine(" }");

            for (var i = 0; i < styles.Length; i++)
            {
                if (!styles[i].Used)
                {
                    continue;
                }

                tw.Write("span.s{0} {{", i);
                tw.Write(@" font-family: ""{0}"";", styles[i].FontName);
                tw.Write(" font-size: {0}pt;", styles[i].SizeF);
                tw.Write(" font-weight: {0};", styles[i].Weight);
                if (styles[i].Italic != 0)
                {
                    tw.Write(" font-style: italic;");
                }

                if (styles[i].Underline != 0)
                {
                    tw.Write(" text-decoration: underline;");
                }

                tw.Write(" background-color: #{0:X2}{1:X2}{2:X2};", (styles[i].BackColor >> 0) & 0xFF, (styles[i].BackColor >> 8) & 0xFF, (styles[i].BackColor >> 16) & 0xFF);
                tw.Write(" color: #{0:X2}{1:X2}{2:X2};", (styles[i].ForeColor >> 0) & 0xFF, (styles[i].ForeColor >> 8) & 0xFF, (styles[i].ForeColor >> 16) & 0xFF);
                switch ((StyleCase)styles[i].Case)
                {
                    case StyleCase.Upper:
                        tw.Write(" text-transform: uppercase;");
                        break;
                    case StyleCase.Lower:
                        tw.Write(" text-transform: lowercase;");
                        break;
                }

                if (styles[i].Visible == 0)
                {
                    tw.Write(" visibility: hidden;");
                }

                tw.WriteLine(" }");
            }

            tw.WriteLine("</style>");
            tw.Write(@"<div id=""segments""><span class=""s{0}"">", StyleConstants.Default);
            tw.Flush();

            var tabSize = scintilla.DirectMessage(SCI_GETTABWIDTH).ToInt32();
            var tab = new string(' ', tabSize);

            tw.AutoFlush = true;
            var lastStyle = StyleConstants.Default;
            var unicodeLineEndings = (scintilla.DirectMessage(SCI_GETLINEENDTYPESACTIVE).ToInt32() & SC_LINE_END_TYPE_UNICODE) > 0;
            foreach (var seg in styledSegments)
            {
                var endOffset = seg.Offset + seg.Count;
                for (var i = seg.Offset; i < endOffset; i += 2)
                {
                    var ch = seg.Array![i];
                    var style = seg.Array[i + 1];

                    if (lastStyle != style)
                    {
                        tw.Write(@"</span><span class=""s{0}"">", style);
                        lastStyle = style;
                    }

                    switch (ch)
                    {
                        case (byte)'<':
                            tw.Write("&lt;");
                            break;

                        case (byte)'>':
                            tw.Write("&gt;");
                            break;

                        case (byte)'&':
                            tw.Write("&amp;");
                            break;

                        case (byte)'\t':
                            tw.Write(tab);
                            break;

                        case (byte)'\r':
                            if (i + 2 < endOffset)
                            {
                                if (seg.Array[i + 2] == (byte)'\n')
                                {
                                    i += 2;
                                }
                            }

                            // Either way, this is a line break
                            goto case (byte)'\n';

                        case 0xC2:
                            if (unicodeLineEndings && i + 2 < endOffset)
                            {
                                if (seg.Array[i + 2] == 0x85) // NEL \u0085
                                {
                                    i += 2;
                                    goto case (byte)'\n';
                                }
                            }

                            // Not a Unicode line break
                            goto default;

                        case 0xE2:
                            if (unicodeLineEndings && i + 4 < endOffset)
                            {
                                if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA8) // LS \u2028
                                {
                                    i += 4;
                                    goto case (byte)'\n';
                                }
                                else if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA9) // PS \u2029
                                {
                                    i += 4;
                                    goto case (byte)'\n';
                                }
                            }

                            // Not a Unicode line break
                            goto default;

                        case (byte)'\n':
                            // All your line breaks are belong to us
                            tw.Write("\r\n");
                            break;

                        default:

                            if (ch == 0)
                            {
                                // Scintilla behavior is to allow control characters except for
                                // NULL which will cause the Clipboard to truncate the string.
                                tw.Write(" "); // Replace with space
                                break;
                            }

                            ms.WriteByte(ch);
                            break;
                    }
                }
            }

            tw.AutoFlush = false;
            tw.WriteLine("</span></div>");
            tw.Flush();

            // Patch header
            pos = ms.Position;
            ms.Seek(INDEX_END_FRAGMENT, SeekOrigin.Begin);
            ms.Write(bytes = Encoding.ASCII.GetBytes(ms.Length.ToString("D8")), 0, bytes.Length);
            ms.Seek(pos, SeekOrigin.Begin);
            tw.WriteLine("<!--EndFragment-->");

            tw.WriteLine("</body>");
            tw.WriteLine("</html>");
            tw.Flush();

            // Patch header
            pos = ms.Position;
            ms.Seek(INDEX_END_HTML, SeekOrigin.Begin);
            ms.Write(bytes = Encoding.ASCII.GetBytes(ms.Length.ToString("D8")), 0, bytes.Length);
            ms.Seek(pos, SeekOrigin.Begin);

            // Terminator
            ms.WriteByte(0);

            GetString(ms.Pointer, (int)ms.Length, Encoding.UTF8);
            if (NativeMethods.SetClipboardData(CF_HTML, ms.Pointer) != IntPtr.Zero)
            {
                ms.FreeOnDispose = false; // Clipboard will free memory
            }
        }
        catch (Exception ex)
        {
            // Yes, we swallow any exceptions. That may seem like code smell but this matches
            // the behavior of the Clipboard class, Windows Forms controls, and native Scintilla.
            Debug.Fail(ex.Message, ex.ToString());
        }
    }

    

    public static byte[] GetBytes(string text, Encoding encoding, bool zeroTerminated)
    {
        return HelpersGeneral.GetBytes(text, encoding, zeroTerminated);
    }

    public static byte[] GetBytes(char[] text, int length, Encoding encoding, bool zeroTerminated)
    {
        return HelpersGeneral.GetBytes(text, length, encoding, zeroTerminated);
    }

    public static string GetHtml(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color> scintilla, int startBytePos, int endBytePos)
    {
        // If we ever allow more than UTF-8, this will have to be revisited
        Debug.Assert(scintilla.DirectMessage(SCI_GETCODEPAGE).ToInt32() == SC_CP_UTF8);

        if (startBytePos == endBytePos)
        {
            return string.Empty;
        }

        var styledSegments = GetStyledSegments(scintilla, false, false, startBytePos, endBytePos, out var styles);

        using var ms = new NativeMemoryStream(styledSegments.Sum(s => s.Count));
        using var sw = new StreamWriter(ms, new UTF8Encoding(false));
        // Write the styles
        sw.WriteLine(@"<style type=""text/css"" scoped="""">");
        sw.Write("div#segments {");
        sw.Write(" float: left;");
        sw.Write(" white-space: pre;");
        sw.Write(" line-height: {0}px;", scintilla.DirectMessage(SCI_TEXTHEIGHT, new IntPtr(0)).ToInt32());
        sw.Write(" background-color: #{0:X2}{1:X2}{2:X2};", (styles[StyleConstants.Default].BackColor >> 0) & 0xFF, (styles[StyleConstants.Default].BackColor >> 8) & 0xFF, (styles[StyleConstants.Default].BackColor >> 16) & 0xFF);
        sw.WriteLine(" }");

        for (var i = 0; i < styles.Length; i++)
        {
            if (!styles[i].Used)
            {
                continue;
            }

            sw.Write("span.s{0} {{", i);
            sw.Write(@" font-family: ""{0}"";", styles[i].FontName);
            sw.Write(" font-size: {0}pt;", styles[i].SizeF);
            sw.Write(" font-weight: {0};", styles[i].Weight);
            if (styles[i].Italic != 0)
            {
                sw.Write(" font-style: italic;");
            }

            if (styles[i].Underline != 0)
            {
                sw.Write(" text-decoration: underline;");
            }

            sw.Write(" background-color: #{0:X2}{1:X2}{2:X2};", (styles[i].BackColor >> 0) & 0xFF, (styles[i].BackColor >> 8) & 0xFF, (styles[i].BackColor >> 16) & 0xFF);
            sw.Write(" color: #{0:X2}{1:X2}{2:X2};", (styles[i].ForeColor >> 0) & 0xFF, (styles[i].ForeColor >> 8) & 0xFF, (styles[i].ForeColor >> 16) & 0xFF);
            switch ((StyleCase)styles[i].Case)
            {
                case StyleCase.Upper:
                    sw.Write(" text-transform: uppercase;");
                    break;
                case StyleCase.Lower:
                    sw.Write(" text-transform: lowercase;");
                    break;
            }

            if (styles[i].Visible == 0)
            {
                sw.Write(" visibility: hidden;");
            }

            sw.WriteLine(" }");
        }

        sw.WriteLine("</style>");

        var unicodeLineEndings = (scintilla.DirectMessage(SCI_GETLINEENDTYPESACTIVE).ToInt32() & SC_LINE_END_TYPE_UNICODE) > 0;
        var tabSize = scintilla.DirectMessage(SCI_GETTABWIDTH).ToInt32();
        var tab = new string(' ', tabSize);
        var lastStyle = StyleConstants.Default;

        // Write the styled text
        sw.Write(@"<div id=""segments""><span class=""s{0}"">", StyleConstants.Default);
        sw.Flush();
        sw.AutoFlush = true;

        foreach (var seg in styledSegments)
        {
            var endOffset = seg.Offset + seg.Count;
            for (var i = seg.Offset; i < endOffset; i += 2)
            {
                var ch = seg.Array![i];
                var style = seg.Array[i + 1];

                if (lastStyle != style)
                {
                    sw.Write(@"</span><span class=""s{0}"">", style);
                    lastStyle = style;
                }

                switch (ch)
                {
                    case (byte)'<':
                        sw.Write("&lt;");
                        break;

                    case (byte)'>':
                        sw.Write("&gt;");
                        break;

                    case (byte)'&':
                        sw.Write("&amp;");
                        break;

                    case (byte)'\t':
                        sw.Write(tab);
                        break;

                    case (byte)'\r':
                        if (i + 2 < endOffset)
                        {
                            if (seg.Array[i + 2] == (byte)'\n')
                            {
                                i += 2;
                            }
                        }

                        // Either way, this is a line break
                        goto case (byte)'\n';

                    case 0xC2:
                        if (unicodeLineEndings && i + 2 < endOffset)
                        {
                            if (seg.Array[i + 2] == 0x85) // NEL \u0085
                            {
                                i += 2;
                                goto case (byte)'\n';
                            }
                        }

                        // Not a Unicode line break
                        goto default;

                    case 0xE2:
                        if (unicodeLineEndings && i + 4 < endOffset)
                        {
                            if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA8) // LS \u2028
                            {
                                i += 4;
                                goto case (byte)'\n';
                            }
                            else if (seg.Array[i + 2] == 0x80 && seg.Array[i + 4] == 0xA9) // PS \u2029
                            {
                                i += 4;
                                goto case (byte)'\n';
                            }
                        }

                        // Not a Unicode line break
                        goto default;

                    case (byte)'\n':
                        // All your line breaks are belong to us
                        sw.Write("\r\n");
                        break;

                    default:

                        if (ch == 0)
                        {
                            // Replace NUL with space
                            sw.Write(" ");
                            break;
                        }

                        ms.WriteByte(ch);
                        break;
                }
            }
        }

        sw.AutoFlush = false;
        sw.WriteLine("</span></div>");
        sw.Flush();

        return GetString(ms.Pointer, (int)ms.Length, Encoding.UTF8);
    }

    public static string GetString(IntPtr bytes, int length, Encoding encoding)
    {
        return HelpersGeneral.GetString(bytes, length, encoding);
    }

    internal static List<ArraySegment<byte>> GetStyledSegments(IScintillaApi<MarkerCollection, StyleCollection, IndicatorCollection, LineCollection, MarginCollection, SelectionCollection, Marker, Style, Indicator, Line, Margin, Selection, Image, Color> scintilla, bool currentSelection, bool currentLine, int startBytePos, int endBytePos, out StyleData[] styles)
    {
        var segments = new List<ArraySegment<byte>>();
        if (currentSelection)
        {
            // Get each selection as a segment.
            // Rectangular selections are ordered top to bottom and have line breaks appended.
            var ranges = new List<Tuple<int, int>>();
            var selCount = scintilla.DirectMessage(SCI_GETSELECTIONS).ToInt32();
            for (var i = 0; i < selCount; i++)
            {
                var selStartBytePos = scintilla.DirectMessage(SCI_GETSELECTIONNSTART, new IntPtr(i)).ToInt32();
                var selEndBytePos = scintilla.DirectMessage(SCI_GETSELECTIONNEND, new IntPtr(i)).ToInt32();

                ranges.Add(Tuple.Create(selStartBytePos, selEndBytePos));
            }

            var selIsRect = scintilla.DirectMessage(SCI_SELECTIONISRECTANGLE) != IntPtr.Zero;
            if (selIsRect)
            {
                ranges = ranges.OrderBy(r => r.Item1).ToList(); // Sort top to bottom
            }

            foreach (var range in ranges)
            {
                var styledText = GetStyledText(scintilla, range.Item1, range.Item2, selIsRect);
                segments.Add(styledText);
            }
        }
        else if (currentLine)
        {
            // Get the current line
            var mainSelection = scintilla.DirectMessage(SCI_GETMAINSELECTION).ToInt32();
            var mainCaretPos = scintilla.DirectMessage(SCI_GETSELECTIONNCARET, new IntPtr(mainSelection)).ToInt32();
            var lineIndex = scintilla.DirectMessage(SCI_LINEFROMPOSITION, new IntPtr(mainCaretPos)).ToInt32();
            var lineStartBytePos = scintilla.DirectMessage(SCI_POSITIONFROMLINE, new IntPtr(lineIndex)).ToInt32();
            var lineLength = scintilla.DirectMessage(SCI_POSITIONFROMLINE, new IntPtr(lineIndex)).ToInt32();

            var styledText = GetStyledText(scintilla, lineStartBytePos, lineStartBytePos + lineLength, false);
            segments.Add(styledText);
        }
        else // User-specified range
        {
            Debug.Assert(startBytePos != endBytePos);
            var styledText = GetStyledText(scintilla, startBytePos, endBytePos, false);
            segments.Add(styledText);
        }

        // Build a list of (used) styles
        styles = new StyleData[STYLE_MAX + 1];

        styles[StyleConstants.Default].Used = true;
        styles[StyleConstants.Default].FontName = scintilla.Styles[StyleConstants.Default].Font;
        styles[StyleConstants.Default].SizeF = scintilla.Styles[StyleConstants.Default].SizeF;
        styles[StyleConstants.Default].Weight = scintilla.DirectMessage(SCI_STYLEGETWEIGHT, new IntPtr(StyleConstants.Default), IntPtr.Zero).ToInt32();
        styles[StyleConstants.Default].Italic = scintilla.DirectMessage(SCI_STYLEGETITALIC, new IntPtr(StyleConstants.Default), IntPtr.Zero).ToInt32();
        styles[StyleConstants.Default].Underline = scintilla.DirectMessage(SCI_STYLEGETUNDERLINE, new IntPtr(StyleConstants.Default), IntPtr.Zero).ToInt32();
        styles[StyleConstants.Default].BackColor = scintilla.DirectMessage(SCI_STYLEGETBACK, new IntPtr(StyleConstants.Default), IntPtr.Zero).ToInt32();
        styles[StyleConstants.Default].ForeColor = scintilla.DirectMessage(SCI_STYLEGETFORE, new IntPtr(StyleConstants.Default), IntPtr.Zero).ToInt32();
        styles[StyleConstants.Default].Case = scintilla.DirectMessage(SCI_STYLEGETCASE, new IntPtr(StyleConstants.Default), IntPtr.Zero).ToInt32();
        styles[StyleConstants.Default].Visible = scintilla.DirectMessage(SCI_STYLEGETVISIBLE, new IntPtr(StyleConstants.Default), IntPtr.Zero).ToInt32();

        foreach (var seg in segments)
        {
            for (var i = 0; i < seg.Count; i += 2)
            {
                var style = seg.Array![i + 1];
                if (!styles[style].Used)
                {
                    styles[style].Used = true;
                    styles[style].FontName = scintilla.Styles[style].Font;
                    styles[style].SizeF = scintilla.Styles[style].SizeF;
                    styles[style].Weight = scintilla.DirectMessage(SCI_STYLEGETWEIGHT, new IntPtr(style), IntPtr.Zero).ToInt32();
                    styles[style].Italic = scintilla.DirectMessage(SCI_STYLEGETITALIC, new IntPtr(style), IntPtr.Zero).ToInt32();
                    styles[style].Underline = scintilla.DirectMessage(SCI_STYLEGETUNDERLINE, new IntPtr(style), IntPtr.Zero).ToInt32();
                    styles[style].BackColor = scintilla.DirectMessage(SCI_STYLEGETBACK, new IntPtr(style), IntPtr.Zero).ToInt32();
                    styles[style].ForeColor = scintilla.DirectMessage(SCI_STYLEGETFORE, new IntPtr(style), IntPtr.Zero).ToInt32();
                    styles[style].Case = scintilla.DirectMessage(SCI_STYLEGETCASE, new IntPtr(style), IntPtr.Zero).ToInt32();
                    styles[style].Visible = scintilla.DirectMessage(SCI_STYLEGETVISIBLE, new IntPtr(style), IntPtr.Zero).ToInt32();
                }
            }
        }

        return segments;
    }

    private static unsafe ArraySegment<byte> GetStyledText(IScintillaApi scintilla, int startBytePos, int endBytePos, bool addLineBreak)
    {
        Debug.Assert(endBytePos > startBytePos);

        // Make sure the range is styled
        scintilla.DirectMessage(SCI_COLOURISE, new IntPtr(startBytePos), new IntPtr(endBytePos));

        var byteLength = endBytePos - startBytePos;
        var buffer = new byte[byteLength * 2 + (addLineBreak ? 4 : 0) + 2];
        fixed (byte* bp = buffer)
        {
            var tr = stackalloc Sci_TextRange[1];
            tr->chrg.cpMin = startBytePos;
            tr->chrg.cpMax = endBytePos;
            tr->lpstrText = new IntPtr(bp);

            scintilla.DirectMessage(SCI_GETSTYLEDTEXT, IntPtr.Zero, new IntPtr(tr));
            byteLength *= 2;
        }

        // Add a line break?
        // We do this when this range is part of a rectangular selection.
        if (addLineBreak)
        {
            var style = buffer[byteLength - 1];

            buffer[byteLength++] = (byte)'\r';
            buffer[byteLength++] = style;
            buffer[byteLength++] = (byte)'\n';
            buffer[byteLength++] = style;

            // Fix-up the NULL terminator just in case
            buffer[byteLength] = 0;
            buffer[byteLength + 1] = 0;
        }

        return new ArraySegment<byte>(buffer, 0, byteLength);
    }

    public static int TranslateKeys(Keys keys)
    {
        int keyCode;

        // For some reason Scintilla uses different values for these keys...
        switch (keys & Keys.KeyCode)
        {
            case Keys.Down:
                keyCode = SCK_DOWN;
                break;
            case Keys.Up:
                keyCode = SCK_UP;
                break;
            case Keys.Left:
                keyCode = SCK_LEFT;
                break;
            case Keys.Right:
                keyCode = SCK_RIGHT;
                break;
            case Keys.Home:
                keyCode = SCK_HOME;
                break;
            case Keys.End:
                keyCode = SCK_END;
                break;
            case Keys.Prior:
                keyCode = SCK_PRIOR;
                break;
            case Keys.Next:
                keyCode = SCK_NEXT;
                break;
            case Keys.Delete:
                keyCode = SCK_DELETE;
                break;
            case Keys.Insert:
                keyCode = SCK_INSERT;
                break;
            case Keys.Escape:
                keyCode = SCK_ESCAPE;
                break;
            case Keys.Back:
                keyCode = SCK_BACK;
                break;
            case Keys.Tab:
                keyCode = SCK_TAB;
                break;
            case Keys.Return:
                keyCode = SCK_RETURN;
                break;
            case Keys.Add:
                keyCode = SCK_ADD;
                break;
            case Keys.Subtract:
                keyCode = SCK_SUBTRACT;
                break;
            case Keys.Divide:
                keyCode = SCK_DIVIDE;
                break;
            case Keys.LWin:
                keyCode = SCK_WIN;
                break;
            case Keys.RWin:
                keyCode = SCK_RWIN;
                break;
            case Keys.Apps:
                keyCode = SCK_MENU;
                break;
            case Keys.Oem2:
                keyCode = (byte)'/';
                break;
            case Keys.Oem3:
                keyCode = (byte)'`';
                break;
            case Keys.Oem4:
                keyCode = '[';
                break;
            case Keys.Oem5:
                keyCode = '\\';
                break;
            case Keys.Oem6:
                keyCode = ']';
                break;
            default:
                keyCode = (int)(keys & Keys.KeyCode);
                break;
        }

        // No translation necessary for the modifiers. Just add them back in.
        var keyDefinition = keyCode | (int)(keys & Keys.Modifiers);
        return keyDefinition;
    }

    #endregion Methods

    #region Types

    internal struct StyleData
    {
        public bool Used;
        public string FontName;
        public int FontIndex; // RTF Only
        public float SizeF;
        public int Weight;
        public int Italic;
        public int Underline;
        public int BackColor;
        public int BackColorIndex; // RTF Only
        public int ForeColor;
        public int ForeColorIndex; // RTF Only
        public int Case; // HTML only
        public int Visible; // HTML only
    }

    #endregion Types
}