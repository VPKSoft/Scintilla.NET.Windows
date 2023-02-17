using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using ScintillaNet.Abstractions.Classes;
using ScintillaNet.Abstractions.Classes.Lexers;
using ScintillaNet.Abstractions.Enumerations;

namespace TestAppWpf;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private bool shown;

    private void MainWindow_OnContentRendered(object? sender, EventArgs e)
    {
        if (IsVisible && !shown)
        {
            shown = true;
            CreateCsStyling();
        }
    }

    private void CreateCsStyling()
    {
        Color FromHex(string value)
        {
            var rText = value.Substring(1, 2);
            var gText = value.Substring(3, 2);
            var bText = value.Substring(5, 2);
            var r = (byte)int.Parse(rText, NumberStyles.HexNumber);
            var g = (byte)int.Parse(gText, NumberStyles.HexNumber);
            var b = (byte)int.Parse(bText, NumberStyles.HexNumber);
            var a = (byte)0xFF;
            if (value.Length > 7)
            {
                var aText = value.Substring(7, 2);
                a = (byte)int.Parse(aText, NumberStyles.HexNumber);
            }

            return Color.FromArgb(a, r, g, b);
        }

        scintilla.Styles[Cpp.Preprocessor].ForeColor = FromHex("#804000");
        scintilla.Styles[Cpp.Preprocessor].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.Default].ForeColor = FromHex("#000000");
        scintilla.Styles[Cpp.Default].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.Word].Bold = true;
        scintilla.Styles[Cpp.Word].ForeColor = FromHex("#0000FF");
        scintilla.Styles[Cpp.Word].BackColor = FromHex("#FFFFFF");

        scintilla.Styles[Cpp.Word2].ForeColor = FromHex("#8000FF");
        scintilla.Styles[Cpp.Word2].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.Number].ForeColor = FromHex("#FF8000");
        scintilla.Styles[Cpp.Number].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.String].ForeColor = FromHex("#000080");
        scintilla.Styles[Cpp.String].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.Character].ForeColor = FromHex("#000000");
        scintilla.Styles[Cpp.Character].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.Operator].Bold = true;
        scintilla.Styles[Cpp.Operator].ForeColor = FromHex("#000080");
        scintilla.Styles[Cpp.Operator].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.Verbatim].ForeColor = FromHex("#000000");
        scintilla.Styles[Cpp.Verbatim].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.Regex].Bold = true;
        scintilla.Styles[Cpp.Regex].ForeColor = FromHex("#000000");
        scintilla.Styles[Cpp.Regex].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.Comment].ForeColor = FromHex("#008000");
        scintilla.Styles[Cpp.Comment].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.CommentLine].ForeColor = FromHex("#008080");
        scintilla.Styles[Cpp.CommentLine].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.CommentDoc].ForeColor = FromHex("#008080");
        scintilla.Styles[Cpp.CommentDoc].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.CommentLineDoc].ForeColor = FromHex("#008080");
        scintilla.Styles[Cpp.CommentLineDoc].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.CommentDocKeyword].Bold = true;
        scintilla.Styles[Cpp.CommentDocKeyword].ForeColor = FromHex("#008080");
        scintilla.Styles[Cpp.CommentDocKeyword].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.CommentDocKeywordError].ForeColor = FromHex("#008080");
        scintilla.Styles[Cpp.CommentDocKeywordError].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.PreprocessorComment].ForeColor = FromHex("#008000");
        scintilla.Styles[Cpp.PreprocessorComment].BackColor = FromHex("#FFFFFF");
        scintilla.Styles[Cpp.PreprocessorCommentDoc].ForeColor = FromHex("#008080");
        scintilla.Styles[Cpp.PreprocessorCommentDoc].BackColor = FromHex("#FFFFFF");

        scintilla.LexerName = "cpp";

        scintilla.SetKeywords(0, "alignof and and_eq bitand bitor break case catch compl const_cast continue default delete do dynamic_cast else false for goto if namespace new not not_eq nullptr operator or or_eq reinterpret_cast return sizeof static_assert static_cast switch this throw true try typedef typeid using while xor xor_eq NULL");
        scintilla.SetKeywords(1, "alignas asm auto bool char char16_t char32_t class clock_t const constexpr decltype double enum explicit export extern final float friend inline int int8_t int16_t int32_t int64_t int_fast8_t int_fast16_t int_fast32_t int_fast64_t intmax_t intptr_t long mutable noexcept override private protected ptrdiff_t public register short signed size_t ssize_t static struct template thread_local time_t typename uint8_t uint16_t uint32_t uint64_t uint_fast8_t uint_fast16_t uint_fast32_t uint_fast64_t uintmax_t uintptr_t union unsigned virtual void volatile wchar_t");
        scintilla.SetKeywords(2, "a addindex addtogroup anchor arg attention author authors b brief bug c callergraph callgraph category cite class code cond copybrief copydetails copydoc copyright date def defgroup deprecated details diafile dir docbookonly dontinclude dot dotfile e else elseif em endcode endcond enddocbookonly enddot endhtmlonly endif endinternal endlatexonly endlink endmanonly endmsc endparblock endrtfonly endsecreflist enduml endverbatim endxmlonly enum example exception extends f$ f[ f] file fn f{ f} headerfile hidecallergraph hidecallgraph hideinitializer htmlinclude htmlonly idlexcept if ifnot image implements include includelineno ingroup interface internal invariant latexinclude latexonly li line link mainpage manonly memberof msc mscfile n name namespace nosubgrouping note overload p package page par paragraph param parblock post pre private privatesection property protected protectedsection protocol public publicsection pure ref refitem related relatedalso relates relatesalso remark remarks result return returns retval rtfonly sa secreflist section see short showinitializer since skip skipline snippet startuml struct subpage subsection subsubsection tableofcontents test throw throws todo tparam typedef union until var verbatim verbinclude version vhdlflow warning weakgroup xmlonly xrefitem");

        scintilla.SetProperty("fold", "1");
        scintilla.SetProperty("fold.compact", "1");
        scintilla.SetProperty("fold.preprocessor", "1");

        // Configure a margin to display folding symbols
        scintilla.Margins[2].Type = MarginType.Symbol;
        scintilla.Margins[2].Mask = MarkerConstants.MaskFolders;
        scintilla.Margins[2].Sensitive = true;
        scintilla.Margins[2].Width = 20;

        // Set colors for all folding markers
        for (int i = 25; i <= 31; i++)
        {
            scintilla.Markers[i].SetForeColor(Colors.LightGray);
            scintilla.Markers[i].SetBackColor(Colors.DarkGray);
        }

        // Configure folding markers with respective symbols
        scintilla.Markers[MarkerConstants.Folder].Symbol = MarkerSymbol.BoxPlus;
        scintilla.Markers[MarkerConstants.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
        scintilla.Markers[MarkerConstants.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
        scintilla.Markers[MarkerConstants.FolderMidTail].Symbol = MarkerSymbol.TCorner;
        scintilla.Markers[MarkerConstants.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
        scintilla.Markers[MarkerConstants.FolderSub].Symbol = MarkerSymbol.VLine;
        scintilla.Markers[MarkerConstants.FolderTail].Symbol = MarkerSymbol.LCorner;

        // Enable automatic folding
        scintilla.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
    }
}
