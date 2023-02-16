using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Eto.Wpf.Forms;
using ScintillaNet.Abstractions;
using ScintillaNet.Abstractions.Enumerations;
using ScintillaNet.Abstractions.Interfaces;
using ScintillaNet.Abstractions.Interfaces.Methods;
using ScintillaNet.Abstractions.Structs;
using ScintillaNet.EtoForms.WinForms;
using ScintillaNet.WinForms;
using ScintillaNet.WinForms.Collections;
using ScintillaNet.WinForms.EventArguments;
using Control = Eto.Forms.Control;
using Lexilla = ScintillaNet.EtoForms.WinForms.Lexilla;
using TabDrawMode = ScintillaNet.Abstractions.Enumerations.TabDrawMode;

namespace ScintillaNet.EtoForms.Wpf;

/// <summary>
/// Scintilla control handler for WPF.
/// Implements the <see cref="Eto.Wpf.Forms.WindowsFormsHostHandler{TControl, TWidget, TCallback}" />
/// Implements the <see cref="IScintillaControl" />
/// Implements the <see cref="IScintillaApi{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" />
/// Implements the <see cref="IScintillaProperties{TColor}" />
/// Implements the <see cref="IScintillaCollectionProperties{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" />
/// Implements the <see cref="IScintillaMethods" />
/// Implements the <see cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs,TAutoCSelectionChangeEventArgs}" />
/// </summary>
/// <seealso cref="IScintillaApi{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" />
/// <seealso cref="IScintillaProperties{TColor}" />
/// <seealso cref="IScintillaCollectionProperties{TMarkers,TStyles,TIndicators,TLines,TMargins,TSelections,TMarker,TStyle,TIndicator,TLine,TMargin,TSelection,TBitmap,TColor}" />
/// <seealso cref="IScintillaMethods" />
/// <seealso cref="IScintillaEvents{TKeys,TAutoCSelectionEventArgs,TBeforeModificationEventArgs,TModificationEventArgs,TChangeAnnotationEventArgs,TCharAddedEventArgs,TDoubleClickEventArgs,TDwellEventArgs,TCallTipClickEventArgs,THotspotClickEventArgs,TIndicatorClickEventArgs,TIndicatorReleaseEventArgs,TInsertCheckEventArgs,TMarginClickEventArgs,TNeedShownEventArgs,TStyleNeededEventArgs,TUpdateUiEventArgs,TScNotificationEventArgs,TAutoCSelectionChangeEventArgs}" />
public class ScintillaControlHandler :  WindowsFormsHostHandler<ScintillaWinForms, ScintillaControl, Control.ICallback>, IScintillaControl,
    IScintillaWinForms
{
    readonly IntPtr editor;
    private readonly ScintillaWinForms nativeControl;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ScintillaControlHandler"/> class.
    /// </summary>
    public ScintillaControlHandler()
    {
        nativeControl = new ScintillaWinForms();
        WinFormsControl = nativeControl;
        editor = nativeControl.SciPointer;
        NativeControl = nativeControl;
    }
    
    private static Lexilla? lexillaInstance;

    /// <summary>
    /// Gets the singleton instance of the <see cref="Lexilla"/> class.
    /// </summary>
    /// <value>The singleton instance of the <see cref="Lexilla"/> class.</value>
    private static ILexilla LexillaSingleton
    {
        get
        {
            lexillaInstance ??= new Lexilla();
            return lexillaInstance;
        }
    }

    /// <inheritdoc cref="IScintillaControl.SetParameter"/>
    public IntPtr SetParameter(int message, IntPtr wParam, IntPtr lParam)
    {
        return WinFormsControl.DirectMessage(editor, message, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int)"/>
    public IntPtr DirectMessage(int msg)
    {
        return WinFormsControl.DirectMessage(msg, IntPtr.Zero, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam)
    {
        return WinFormsControl.DirectMessage(msg, wParam, IntPtr.Zero);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(int msg, IntPtr wParam, IntPtr lParam)
    {
        return WinFormsControl.DirectMessage(msg, wParam, lParam);
    }

    /// <inheritdoc cref="IScintillaControl.DirectMessage(int, IntPtr, IntPtr)"/>
    public IntPtr DirectMessage(IntPtr sciPtr, int msg, IntPtr wParam, IntPtr lParam)
    {
        return WinFormsControl.DirectMessage(sciPtr, msg, wParam, lParam);
    }

    /// <inheritdoc />
    public void LoadLexerLibrary(string path) => nativeControl.LoadLexerLibrary(path);

    /// <summary>
    /// Removes the specified marker from all lines.
    /// </summary>
    /// <param name="marker">The zero-based index to remove from all lines, or -1 to remove all markers from all lines.</param>
    public void MarkerDeleteAll(int marker)
    {
        WinFormsControl.MarkerDeleteAll(marker);
    }

    /// <inheritdoc />
    public void MarkerDeleteHandle(MarkerHandle markerHandle) => nativeControl.MarkerDeleteHandle(markerHandle);

    /// <inheritdoc />
    public void MarkerEnableHighlight(bool enabled) => nativeControl.MarkerEnableHighlight(enabled);

    /// <inheritdoc />
    public int MarkerLineFromHandle(MarkerHandle markerHandle) => nativeControl.MarkerLineFromHandle(markerHandle);

    /// <inheritdoc />
    public void MultiEdgeAddLine(int column, Color edgeColor) => nativeControl.MultiEdgeAddLine(column, edgeColor);

    /// <inheritdoc />
    public void MultiEdgeClearAll() => nativeControl.MultiEdgeClearAll();

    /// <inheritdoc />
    public void MultipleSelectAddEach() => nativeControl.MultipleSelectAddEach();

    /// <inheritdoc />
    public void MultipleSelectAddNext() => nativeControl.MultipleSelectAddNext();

    /// <inheritdoc />
    public void Paste() => nativeControl.Paste();

    /// <inheritdoc />
    public int PointXFromPosition(int pos) => nativeControl.PointXFromPosition(pos);

    /// <inheritdoc />
    public int PointYFromPosition(int pos) => nativeControl.PointYFromPosition(pos);

    /// <inheritdoc />
    public string PropertyNames() => nativeControl.PropertyNames();

    /// <inheritdoc />
    public PropertyType PropertyType(string name) => nativeControl.PropertyType(name);

    /// <inheritdoc />
    public void Redo() => nativeControl.Redo();

    /// <inheritdoc />
    public void RegisterRgbaImage(int type, Image image) => nativeControl.RegisterRgbaImage(type, image);

    /// <inheritdoc />
    public void ReleaseDocument(Document document) => nativeControl.ReleaseDocument(document);

    /// <inheritdoc />
    public void ReplaceSelection(string text) => nativeControl.ReplaceSelection(text);

    /// <inheritdoc />
    public int ReplaceTarget(string text) => nativeControl.ReplaceTarget(text);

    /// <inheritdoc />
    public int ReplaceTargetRe(string text) => nativeControl.ReplaceTargetRe(text);

    /// <inheritdoc />
    public void RotateSelection() => nativeControl.RotateSelection();

    /// <inheritdoc />
    public void ScrollCaret() => nativeControl.ScrollCaret();

    /// <inheritdoc />
    public void ScrollRange(int start, int end) => nativeControl.ScrollRange(start, end);

    /// <inheritdoc />
    public int SearchInTarget(string text) => nativeControl.SearchInTarget(text);

    /// <inheritdoc />
    public void SelectAll() => nativeControl.SelectAll();

    /// <inheritdoc />
    public void SetAdditionalSelBack(Color color) => nativeControl.SetAdditionalSelBack(color);

    /// <inheritdoc />
    public void SetAdditionalSelFore(Color color) => nativeControl.SetAdditionalSelFore(color);

    /// <inheritdoc />
    public void SetEmptySelection(int pos) => nativeControl.SetEmptySelection(pos);

    /// <inheritdoc />
    public void SetXCaretPolicy(CaretPolicy caretPolicy, int caretSlop) =>
        nativeControl.SetXCaretPolicy(caretPolicy, caretSlop);

    /// <inheritdoc />
    public void SetYCaretPolicy(CaretPolicy caretPolicy, int caretSlop) =>
        nativeControl.SetYCaretPolicy(caretPolicy, caretSlop);

    /// <inheritdoc />
    public void SetFoldFlags(FoldFlags flags) => nativeControl.SetFoldFlags(flags);

    /// <inheritdoc />
    public void SetFoldMarginColor(bool use, Color color) => nativeControl.SetFoldMarginColor(use, color);

    /// <inheritdoc />
    public void SetFoldMarginHighlightColor(bool use, Color color) =>
        nativeControl.SetFoldMarginHighlightColor(use, color);

    /// <inheritdoc />
    public void SetIdentifiers(int style, string identifiers) => nativeControl.SetIdentifiers(style, identifiers);

    /// <inheritdoc />
    public void SetKeywords(int set, string keywords) => nativeControl.SetKeywords(set, keywords);

    /// <inheritdoc />
    public void SetProperty(string name, string value) => nativeControl.SetProperty(name, value);

    /// <inheritdoc />
    public void SetSavePoint() => nativeControl.SetSavePoint();

    /// <inheritdoc />
    public void SetSel(int anchorPos, int currentPos) => nativeControl.SetSel(anchorPos, currentPos);

    /// <inheritdoc />
    public void SetSelection(int caret, int anchor) => nativeControl.SetSelection(caret, anchor);

    /// <inheritdoc />
    public void SetSelectionBackColor(bool use, Color color) => nativeControl.SetSelectionBackColor(use, color);

    /// <inheritdoc />
    public void SetSelectionForeColor(bool use, Color color) => nativeControl.SetSelectionForeColor(use, color);

    /// <inheritdoc />
    public void SetStyling(int length, int style) => nativeControl.SetStyling(length, style);

    /// <inheritdoc />
    public void SetTargetRange(int start, int end) => nativeControl.SetTargetRange(start, end);

    /// <inheritdoc />
    public void SetWhitespaceBackColor(bool use, Color color) => nativeControl.SetWhitespaceBackColor(use, color);

    /// <inheritdoc />
    public void SetWhitespaceForeColor(bool use, Color color) => nativeControl.SetWhitespaceForeColor(use, color);

    /// <inheritdoc />
    public void ShowLines(int lineStart, int lineEnd) => nativeControl.ShowLines(lineStart, lineEnd);

    /// <inheritdoc />
    public void StartStyling(int position) => nativeControl.StartStyling(position);

    /// <inheritdoc />
    public void StyleClearAll() => nativeControl.StyleClearAll();

    /// <inheritdoc />
    public void StyleResetDefault() => nativeControl.StyleResetDefault();

    /// <inheritdoc />
    public void SwapMainAnchorCaret() => nativeControl.SwapMainAnchorCaret();

    /// <inheritdoc />
    public void TargetFromSelection() => nativeControl.TargetFromSelection();

    /// <inheritdoc />
    public void TargetWholeDocument() => nativeControl.TargetWholeDocument();

    /// <inheritdoc />
    public int TextWidth(int style, string text) => nativeControl.TextWidth(style, text);

    /// <inheritdoc />
    public void Undo() => nativeControl.Undo();

    /// <inheritdoc />
    public void UsePopup(bool enablePopup) => nativeControl.UsePopup(enablePopup);

    /// <inheritdoc />
    public void UsePopup(PopupMode popupMode) => nativeControl.UsePopup(popupMode);

    /// <inheritdoc />
    public int WordEndPosition(int position, bool onlyWordCharacters) => nativeControl.WordEndPosition(position, onlyWordCharacters);

    /// <inheritdoc />
    public int WordStartPosition(int position, bool onlyWordCharacters) =>
        nativeControl.WordStartPosition(position, onlyWordCharacters);

    /// <inheritdoc />
    public void ZoomIn() => nativeControl.ZoomIn();

    /// <inheritdoc />
    public void ZoomOut() => nativeControl.ZoomOut();

    /// <inheritdoc />
    public void SetRepresentation(string encodedString, string representationString) =>
        nativeControl.SetRepresentation(encodedString, representationString);

    /// <inheritdoc />
    public string GetRepresentation(string encodedString) => nativeControl.GetRepresentation(encodedString);

    /// <inheritdoc />
    public void ClearRepresentation(string encodedString) => nativeControl.ClearRepresentation(encodedString);

    /// <inheritdoc />
    public Encoding Encoding => WinFormsControl.Encoding;

    /// <inheritdoc />
    public int TextLength => WinFormsControl.TextLength;

    /// <inheritdoc />
    public string GetTag(int tagNumber) => nativeControl.GetTag(tagNumber);

    /// <summary>
    /// Gets a range of text from the document.
    /// </summary>
    /// <param name="position">The zero-based starting character position of the range to get.</param>
    /// <param name="length">The number of characters to get.</param>
    /// <returns>A string representing the text range.</returns>
    public string GetTextRange(int position, int length) => nativeControl.GetTextRange(position, length);

    /// <inheritdoc />
    public string GetTextRangeAsHtml(int position, int length) => nativeControl.GetTextRangeAsHtml(position, length);

    /// <inheritdoc />
    public string GetWordFromPosition(int position) => nativeControl.GetWordFromPosition(position);

    /// <inheritdoc />
    public void GotoPosition(int position) => nativeControl.GotoPosition(position);

    /// <inheritdoc />
    public void HideLines(int lineStart, int lineEnd) => nativeControl.HideLines(lineStart, lineEnd);

    /// <inheritdoc />
    public uint IndicatorAllOnFor(int position) => nativeControl.IndicatorAllOnFor(position);

    /// <inheritdoc />
    public void IndicatorClearRange(int position, int length) => nativeControl.IndicatorClearRange(position, length);

    /// <inheritdoc />
    public void IndicatorFillRange(int position, int length) => nativeControl.IndicatorFillRange(position, length);

    /// <inheritdoc />
    public void InsertText(int position, string text) => nativeControl.InsertText(position, text);

    /// <inheritdoc />
    public bool IsRangeWord(int start, int end) => nativeControl.IsRangeWord(start, end);

    /// <inheritdoc />
    public int LineFromPosition(int position) => nativeControl.LineFromPosition(position);

    /// <inheritdoc />
    public void LineScroll(int lines, int columns) => nativeControl.LineScroll(lines, columns);

    /// <inheritdoc />
    public void AddRefDocument(Document document) => nativeControl.AddRefDocument(document);

    /// <inheritdoc />
    public void AddSelection(int caret, int anchor) => nativeControl.AddSelection(caret, anchor);

    /// <inheritdoc />
    public void AddText(string text) => nativeControl.AddText(text);

    /// <inheritdoc />
    public int AllocateSubStyles(int styleBase, int numberStyles) => nativeControl.AllocateSubStyles(styleBase, numberStyles);

    /// <inheritdoc />
    public void AnnotationClearAll() => nativeControl.AnnotationClearAll();

    /// <inheritdoc />
    public void AppendText(string text) => nativeControl.AppendText(text);

    /// <inheritdoc />
    public void AssignCmdKey(Keys keyDefinition, Command sciCommand) => nativeControl.AssignCmdKey(keyDefinition, sciCommand);

    /// <inheritdoc />
    public void AutoCCancel() => nativeControl.AutoCCancel();

    /// <inheritdoc />
    public void AutoCComplete() => nativeControl.AutoCComplete();

    /// <inheritdoc />
    public void AutoCSelect(string select) => nativeControl.AutoCSelect(select);

    /// <inheritdoc />
    public void AutoCSetFillUps(string chars) => nativeControl.AutoCSetFillUps(chars);

    /// <inheritdoc />
    public void AutoCShow(int lenEntered, string list) => nativeControl.AutoCShow(lenEntered, list);

    /// <inheritdoc />
    public void AutoCStops(string chars) => nativeControl.AutoCStops(chars);

    /// <inheritdoc />
    public void BeginUndoAction() => nativeControl.BeginUndoAction();

    /// <inheritdoc />
    public void BraceBadLight(int position) => nativeControl.BraceBadLight(position);

    /// <inheritdoc />
    public void BraceHighlight(int position1, int position2) => nativeControl.BraceHighlight(position1, position2);

    /// <inheritdoc />
    public int BraceMatch(int position) => nativeControl.BraceMatch(position);

    /// <inheritdoc />
    public void CallTipCancel() => nativeControl.CallTipCancel();

    /// <inheritdoc />
    public void CallTipSetForeHlt(Color color) => nativeControl.CallTipSetForeHlt(color);

    /// <inheritdoc />
    public void CallTipSetHlt(int hlStart, int hlEnd) => nativeControl.CallTipSetHlt(hlStart, hlEnd);

    /// <inheritdoc />
    public void CallTipSetPosition(bool above) => nativeControl.CallTipSetPosition(above);

    /// <inheritdoc />
    public void CallTipShow(int posStart, string definition) => nativeControl.CallTipShow(posStart, definition);

    /// <inheritdoc />
    public void CallTipTabSize(int tabSize) => nativeControl.CallTipTabSize(tabSize);

    /// <inheritdoc />
    public void ChangeLexerState(int startPos, int endPos) => nativeControl.ChangeLexerState(startPos, endPos);

    /// <inheritdoc />
    public int CharPositionFromPoint(int x, int y) => nativeControl.CharPositionFromPoint(x, y);

    /// <inheritdoc />
    public int CharPositionFromPointClose(int x, int y) => nativeControl.CharPositionFromPointClose(x, y);

    /// <inheritdoc />
    public void ChooseCaretX() => nativeControl.ChooseCaretX();

    /// <inheritdoc />
    public void Clear() => nativeControl.Clear();

    /// <inheritdoc />
    public void ClearAll() => nativeControl.ClearAll();

    /// <inheritdoc />
    public void ClearCmdKey(Keys keyDefinition) => nativeControl.ClearCmdKey(keyDefinition);

    /// <inheritdoc />
    public void ClearAllCmdKeys() => nativeControl.ClearAllCmdKeys();

    /// <inheritdoc />
    public void ClearDocumentStyle() => nativeControl.ClearDocumentStyle();

    /// <inheritdoc />
    public void ClearRegisteredImages() => nativeControl.ClearRegisteredImages();

    /// <inheritdoc />
    public void ClearSelections() => nativeControl.ClearSelections();

    /// <inheritdoc />
    public void Colorize(int startPos, int endPos) => nativeControl.Colorize(startPos, endPos);

    /// <inheritdoc />
    public void ConvertEols(Eol eolMode) => nativeControl.ConvertEols(eolMode);

    /// <inheritdoc />
    public void Copy() => nativeControl.Copy();

    /// <inheritdoc />
    public void Copy(CopyFormat format) => nativeControl.Copy(format);

    /// <inheritdoc />
    public void CopyAllowLine() => nativeControl.CopyAllowLine();

    /// <inheritdoc />
    public void CopyAllowLine(CopyFormat format) => nativeControl.CopyAllowLine(format);

    /// <inheritdoc />
    public void CopyRange(int start, int end) => nativeControl.CopyRange(start, end);

    /// <inheritdoc />
    public void CopyRange(int start, int end, CopyFormat format) => nativeControl.CopyRange(start, end, format);

    /// <inheritdoc />
    public Document CreateDocument() => nativeControl.CreateDocument();

    /// <inheritdoc />
    public ILoader CreateLoader(int length) => nativeControl.CreateLoader(length);

    /// <inheritdoc />
    public void Cut() => nativeControl.Cut();

    /// <inheritdoc />
    public void DeleteRange(int position, int length) => nativeControl.DeleteRange(position, length);

    /// <inheritdoc />
    public string DescribeKeywordSets() => nativeControl.DescribeKeywordSets();

    /// <inheritdoc />
    public string DescribeProperty(string name) => nativeControl.DescribeProperty(name);

    /// <inheritdoc />
    public int DocLineFromVisible(int displayLine) => nativeControl.DocLineFromVisible(displayLine);

    /// <inheritdoc />
    public void DropSelection(int selection) => nativeControl.DropSelection(selection);

    /// <inheritdoc />
    public void EmptyUndoBuffer() => nativeControl.EmptyUndoBuffer();

    /// <inheritdoc />
    public void EndUndoAction() => nativeControl.EndUndoAction();

    /// <inheritdoc />
    public void ExecuteCmd(Command sciCommand) => nativeControl.ExecuteCmd(sciCommand);

    /// <summary>
    /// Performs the specified fold action on the entire document.
    /// </summary>
    /// <param name="action">One of the <see cref="T:Scintilla.NET.Abstractions.Enumerations.FoldAction" /> enumeration values.</param>
    /// <remarks>When using <see cref="F:Scintilla.NET.Abstractions.Enumerations.FoldAction.Toggle" /> the first fold header in the document is examined to decide whether to expand or contract.</remarks>
    public void FoldAll(FoldAction action) => nativeControl.FoldAll(action);

    /// <inheritdoc />
    public void InitDocument(Eol eolMode = Eol.CrLf, bool useTabs = false, int tabWidth = 4, int indentWidth = 0) =>
        nativeControl.InitDocument(eolMode, useTabs, tabWidth, indentWidth);

    /// <inheritdoc />
    public void FoldDisplayTextSetStyle(FoldDisplayText style) => nativeControl.FoldDisplayTextSetStyle(style);

    /// <inheritdoc />
    public void FreeSubStyles() => nativeControl.FreeSubStyles();

    /// <inheritdoc />
    public int GetCharAt(int position) => nativeControl.GetCharAt(position);

    /// <inheritdoc />
    public int GetColumn(int position) => nativeControl.GetColumn(position);

    /// <inheritdoc />
    public int GetEndStyled() => nativeControl.GetEndStyled();

    /// <inheritdoc />
    public int GetPrimaryStyleFromStyle(int style) => nativeControl.GetPrimaryStyleFromStyle(style);

    /// <inheritdoc />
    public string GetScintillaProperty(string name) => nativeControl.GetScintillaProperty(name);

    /// <inheritdoc />
    public string GetPropertyExpanded(string name) => nativeControl.GetPropertyExpanded(name);

    /// <inheritdoc />
    public int GetPropertyInt(string name, int defaultValue) => nativeControl.GetPropertyInt(name, defaultValue);

    /// <inheritdoc />
    public int GetStyleAt(int position) => nativeControl.GetStyleAt(position);

    /// <inheritdoc />
    public int GetStyleFromSubStyle(int subStyle) => nativeControl.GetStyleFromSubStyle(subStyle);

    /// <inheritdoc />
    public int GetSubStylesLength(int styleBase) => nativeControl.GetSubStylesLength(styleBase);

    /// <inheritdoc />
    public int GetSubStylesStart(int styleBase) => nativeControl.GetSubStylesStart(styleBase);

    /// <summary>
    /// Gets the Lexilla library access.
    /// </summary>
    /// <value>The lexilla library access.</value>
    public ILexilla Lexilla => LexillaSingleton;

    /// <inheritdoc />
    public object NativeControl { get; }

    /// <inheritdoc />
    public event EventHandler<EventArgs>? AutoCCancelled
    {
        add => nativeControl.AutoCCancelled += value;
        remove => nativeControl.AutoCCancelled -= value;
    }

    /// <inheritdoc />
    public event EventHandler<EventArgs>? AutoCCharDeleted
    {
        add => nativeControl.AutoCCharDeleted += value;
        remove => nativeControl.AutoCCharDeleted -= value;
    }


    /// <inheritdoc />
    public event EventHandler<AutoCSelectionEventArgs>? AutoCCompleted
    {
        add => nativeControl.AutoCCompleted += value;
        remove => nativeControl.AutoCCompleted -= value;
    }

    /// <inheritdoc />
    public event EventHandler<AutoCSelectionEventArgs>? AutoCSelection
    {
        add => nativeControl.AutoCSelection += value;
        remove => nativeControl.AutoCSelection -= value;
    }

    /// <inheritdoc />
    public event EventHandler<BeforeModificationEventArgs>? BeforeDelete
    {
        add => nativeControl.BeforeDelete += value;
        remove => nativeControl.BeforeDelete -= value;
    }

    /// <inheritdoc />
    public event EventHandler<BeforeModificationEventArgs>? BeforeInsert
    {
        add => nativeControl.BeforeInsert += value;
        remove => nativeControl.BeforeInsert -= value;
    }

    /// <inheritdoc />
    public event EventHandler<ChangeAnnotationEventArgs>? ChangeAnnotation
    {
        add => nativeControl.ChangeAnnotation += value;
        remove => nativeControl.ChangeAnnotation -= value;
    }

    /// <inheritdoc />
    public event EventHandler<CharAddedEventArgs>? CharAdded
    {
        add => nativeControl.CharAdded += value;
        remove => nativeControl.CharAdded -= value;
    }

    /// <inheritdoc />
    public event EventHandler<ModificationEventArgs>? Delete
    {
        add => nativeControl.Delete += value;
        remove => nativeControl.Delete -= value;
    }

    /// <inheritdoc />
    public event EventHandler<DoubleClickEventArgs>? DoubleClick
    {
        add => nativeControl.DoubleClick += value;
        remove => nativeControl.DoubleClick -= value;
    }

    /// <inheritdoc />
    public event EventHandler<DwellEventArgs>? DwellEnd
    {
        add => nativeControl.DwellEnd += value;
        remove => nativeControl.DwellEnd -= value;
    }

    /// <inheritdoc />
    public event EventHandler<CallTipClickEventArgs>? CallTipClick
    {
        add => nativeControl.CallTipClick += value;
        remove => nativeControl.CallTipClick -= value;
    }

    /// <inheritdoc />
    public event EventHandler<DwellEventArgs>? DwellStart
    {
        add => nativeControl.DwellStart += value;
        remove => nativeControl.DwellStart -= value;
    }

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs<Keys>>? HotspotClick
    {
        add => nativeControl.HotspotClick += value;
        remove => nativeControl.HotspotClick -= value;
    }

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs<Keys>>? HotspotDoubleClick
    {
        add => nativeControl.HotspotDoubleClick += value;
        remove => nativeControl.HotspotDoubleClick -= value;
    }

    /// <inheritdoc />
    public event EventHandler<HotspotClickEventArgs<Keys>>? HotspotReleaseClick
    {
        add => nativeControl.HotspotReleaseClick += value;
        remove => nativeControl.HotspotReleaseClick -= value;
    }

    /// <inheritdoc />
    public event EventHandler<IndicatorClickEventArgs>? IndicatorClick
    {
        add => nativeControl.IndicatorClick += value;
        remove => nativeControl.IndicatorClick -= value;
    }

    /// <inheritdoc />
    public event EventHandler<IndicatorReleaseEventArgs>? IndicatorRelease
    {
        add => nativeControl.IndicatorRelease += value;
        remove => nativeControl.IndicatorRelease -= value;
    }

    /// <inheritdoc />
    public event EventHandler<ModificationEventArgs>? Insert
    {
        add => nativeControl.Insert += value;
        remove => nativeControl.Insert -= value;
    }

    /// <inheritdoc />
    public event EventHandler<InsertCheckEventArgs>? InsertCheck
    {
        add => nativeControl.InsertCheck += value;
        remove => nativeControl.InsertCheck -= value;
    }

    /// <inheritdoc />
    public event EventHandler<MarginClickEventArgs>? MarginClick
    {
        add => nativeControl.MarginClick += value;
        remove => nativeControl.MarginClick -= value;
    }

    /// <inheritdoc />
    public event EventHandler<MarginClickEventArgs>? MarginRightClick
    {
        add => nativeControl.MarginRightClick += value;
        remove => nativeControl.MarginRightClick -= value;
    }

    /// <inheritdoc />
    public event EventHandler<EventArgs>? ModifyAttempt
    {
        add => nativeControl.ModifyAttempt += value;
        remove => nativeControl.ModifyAttempt -= value;
    }

    /// <inheritdoc />
    public event EventHandler<NeedShownEventArgs>? NeedShown
    {
        add => nativeControl.NeedShown += value;
        remove => nativeControl.NeedShown -= value;
    }

    /// <inheritdoc />
    public event EventHandler<SCNotificationEventArgs>? SCNotification
    {
        add => nativeControl.SCNotification += value;
        remove => nativeControl.SCNotification -= value;
    }

    /// <inheritdoc />
    public event EventHandler<EventArgs>? Painted
    {
        add => nativeControl.Painted += value;
        remove => nativeControl.Painted -= value;
    }

    /// <inheritdoc />
    public event EventHandler<EventArgs>? SavePointLeft
    {
        add => nativeControl.SavePointLeft += value;
        remove => nativeControl.SavePointLeft -= value;
    }

    /// <inheritdoc />
    public event EventHandler<EventArgs>? SavePointReached
    {
        add => nativeControl.SavePointReached += value;
        remove => nativeControl.SavePointReached -= value;
    }

    /// <inheritdoc />
    public event EventHandler<StyleNeededEventArgs>? StyleNeeded
    {
        add => nativeControl.StyleNeeded += value;
        remove => nativeControl.StyleNeeded -= value;
    }

    /// <inheritdoc />
    public event EventHandler<UpdateUIEventArgs>? UpdateUi
    {
        add => nativeControl.UpdateUi += value;
        remove => nativeControl.UpdateUi -= value;
    }

    /// <inheritdoc />
    public event EventHandler<AutoCSelectionChangeEventArgs>? AutoCSelectionChange
    {
        add => nativeControl.AutoCSelectionChange += value;
        remove => nativeControl.AutoCSelectionChange -= value;
    }

    /// <inheritdoc />
    public event EventHandler<EventArgs>? ZoomChanged
    {
        add => nativeControl.ZoomChanged += value;
        remove => nativeControl.ZoomChanged -= value;
    }

    /// <inheritdoc cref="Scintilla.Markers" />
    public MarkerCollection Markers => nativeControl.Markers;

    /// <summary>
    /// Gets a collection representing style definitions in a <see cref="T:Scintilla.NET.Abstractions.IScintillaApi`15" /> control.
    /// </summary>
    /// <value>The styles.</value>
    public StyleCollection Styles => nativeControl.Styles;

    /// <summary>
    /// Gets a collection of objects for working with indicators.
    /// </summary>
    /// <value>The indicators.</value>
    public IndicatorCollection Indicators => nativeControl.Indicators;

    /// <summary>
    /// Gets a collection representing lines of text in the <see cref="T:Scintilla.NET.Abstractions.IScintillaApi`15" /> control interface.
    /// </summary>
    /// <value>The lines.</value>
    public LineCollection Lines => nativeControl.Lines;

    /// <summary>
    /// Gets a collection representing margins in a <see cref="T:Scintilla.NET.Abstractions.IScintillaApi`15" /> control interface.
    /// </summary>
    /// <value>The margins.</value>
    public MarginCollection Margins => nativeControl.Margins;

    /// <summary>
    /// Gets a collection representing multiple selections in a <see cref="T:Scintilla.NET.Abstractions.IScintillaApi`15" /> control interface.
    /// </summary>
    /// <value>The selections.</value>
    public SelectionCollection Selections => nativeControl.Selections;

    /// <inheritdoc />
    public BiDirectionalDisplayType BiDirectionality { get => nativeControl.BiDirectionality; set => nativeControl.BiDirectionality = value; }

    /// <inheritdoc />
    public Color AdditionalCaretForeColor { get => nativeControl.AdditionalCaretForeColor; set => nativeControl.AdditionalCaretForeColor = value; }

    /// <inheritdoc />
    public bool AdditionalCaretsBlink { get => nativeControl.AdditionalCaretsBlink; set => nativeControl.AdditionalCaretsBlink = value; }

    /// <inheritdoc />
    public bool AdditionalCaretsVisible { get => nativeControl.AdditionalCaretsVisible; set => nativeControl.AdditionalCaretsVisible = value; }

    /// <inheritdoc />
    public int AnchorPosition { get => nativeControl.AnchorPosition; set => nativeControl.AnchorPosition = value; }

    /// <inheritdoc />
    public int AdditionalSelAlpha { get => nativeControl.AdditionalSelAlpha; set => nativeControl.AdditionalSelAlpha = value; }

    /// <inheritdoc />
    public bool AdditionalSelectionTyping { get => nativeControl.AdditionalSelectionTyping; set => nativeControl.AdditionalSelectionTyping = value; }

    /// <inheritdoc />
    public Annotation AnnotationVisible { get => nativeControl.AnnotationVisible; set => nativeControl.AnnotationVisible = value; }

    /// <inheritdoc />
    public bool AutoCActive => nativeControl.AutoCActive;

    /// <inheritdoc />
    public bool AutoCAutoHide { get => nativeControl.AutoCAutoHide; set => nativeControl.AutoCAutoHide = value; }

    /// <inheritdoc />
    public bool AutoCCancelAtStart { get => nativeControl.AutoCCancelAtStart; set => nativeControl.AutoCCancelAtStart = value; }

    /// <inheritdoc />
    public int AutoCCurrent => nativeControl.AutoCCurrent;

    /// <inheritdoc />
    public bool AutoCChooseSingle { get => nativeControl.AutoCChooseSingle; set => nativeControl.AutoCChooseSingle = value; }

    /// <inheritdoc />
    public bool AutoCDropRestOfWord { get => nativeControl.AutoCDropRestOfWord; set => nativeControl.AutoCDropRestOfWord = value; }

    /// <inheritdoc />
    public bool AutoCIgnoreCase { get => nativeControl.AutoCIgnoreCase; set => nativeControl.AutoCIgnoreCase = value; }

    /// <inheritdoc />
    public int AutoCMaxHeight { get => nativeControl.AutoCMaxHeight; set => nativeControl.AutoCMaxHeight = value; }

    /// <inheritdoc />
    public int AutoCMaxWidth { get => nativeControl.AutoCMaxWidth; set => nativeControl.AutoCMaxWidth = value; }

    /// <inheritdoc />
    public Order AutoCOrder { get => nativeControl.AutoCOrder; set => nativeControl.AutoCOrder = value; }

    /// <inheritdoc />
    public int AutoCPosStart => nativeControl.AutoCPosStart;

    /// <inheritdoc />
    public Document Document { get => nativeControl.Document; set => nativeControl.Document = value; }

    /// <inheritdoc />
    public int RectangularSelectionAnchor { get => nativeControl.RectangularSelectionAnchor; set => nativeControl.RectangularSelectionAnchor = value; }

    /// <inheritdoc />
    public int RectangularSelectionCaret { get => nativeControl.RectangularSelectionCaret; set => nativeControl.RectangularSelectionCaret = value; }

    /// <inheritdoc />
    public char AutoCSeparator { get => nativeControl.AutoCSeparator; set => nativeControl.AutoCSeparator = value; }

    /// <inheritdoc />
    public char AutoCTypeSeparator { get => nativeControl.AutoCTypeSeparator; set => nativeControl.AutoCTypeSeparator = value; }

    /// <inheritdoc />
    public AutomaticFold AutomaticFold { get => nativeControl.AutomaticFold; set => nativeControl.AutomaticFold = value; }

    /// <inheritdoc />
    public bool BackspaceUnIndents { get => nativeControl.BackspaceUnIndents; set => nativeControl.BackspaceUnIndents = value; }

    /// <inheritdoc />
    public bool BufferedDraw { get => nativeControl.BufferedDraw; set => nativeControl.BufferedDraw = value; }

    /// <inheritdoc />
    public bool CallTipActive => nativeControl.CallTipActive;

    /// <inheritdoc />
    public bool CanPaste => nativeControl.CanPaste;

    /// <inheritdoc />
    public bool CanRedo => nativeControl.CanRedo;

    /// <inheritdoc />
    public bool CanUndo => nativeControl.CanUndo;

    /// <inheritdoc />
    public Color CaretForeColor { get => nativeControl.CaretForeColor; set => nativeControl.CaretForeColor = value; }

    /// <inheritdoc />
    public Color CaretLineBackColor { get => nativeControl.CaretLineBackColor; set => nativeControl.CaretLineBackColor = value; }

    /// <inheritdoc />
    public int CaretLineBackColorAlpha { get => nativeControl.CaretLineBackColorAlpha; set => nativeControl.CaretLineBackColorAlpha = value; }

    /// <inheritdoc />
    public int CaretLineFrame { get => nativeControl.CaretLineFrame; set => nativeControl.CaretLineFrame = value; }

    /// <inheritdoc />
    public bool CaretLineVisible { get => nativeControl.CaretLineVisible; set => nativeControl.CaretLineVisible = value; }

    /// <inheritdoc />
    public bool CaretLineVisibleAlways { get => nativeControl.CaretLineVisibleAlways; set => nativeControl.CaretLineVisibleAlways = value; }

    /// <inheritdoc />
    public Layer CaretLineLayer { get => nativeControl.CaretLineLayer; set => nativeControl.CaretLineLayer = value; }

    /// <inheritdoc />
    public int CaretPeriod { get => nativeControl.CaretPeriod; set => nativeControl.CaretPeriod = value; }

    /// <inheritdoc />
    public CaretStyle CaretStyle { get => nativeControl.CaretStyle; set => nativeControl.CaretStyle = value; }

    /// <inheritdoc />
    public int CaretWidth { get => nativeControl.CaretWidth; set => nativeControl.CaretWidth = value; }

    /// <inheritdoc />
    public int CurrentLine=> nativeControl.CurrentLine;

    /// <inheritdoc />
    public int CurrentPosition { get => nativeControl.CurrentPosition; set => nativeControl.CurrentPosition = value; }

    /// <inheritdoc />
    public int DistanceToSecondaryStyles => nativeControl.DistanceToSecondaryStyles;

    /// <inheritdoc />
    public Color EdgeColor { get => nativeControl.EdgeColor; set => nativeControl.EdgeColor = value; }

    /// <inheritdoc />
    public int EdgeColumn { get => nativeControl.EdgeColumn; set => nativeControl.EdgeColumn = value; }

    /// <inheritdoc />
    public EdgeMode EdgeMode { get => nativeControl.EdgeMode; set => nativeControl.EdgeMode = value; }

    /// <inheritdoc />
    public bool EndAtLastLine { get => nativeControl.EndAtLastLine; set => nativeControl.EndAtLastLine = value; }

    /// <inheritdoc />
    public Eol EolMode { get => nativeControl.EolMode; set => nativeControl.EolMode = value; }

    /// <inheritdoc />
    public int ExtraAscent { get => nativeControl.ExtraAscent; set => nativeControl.ExtraAscent = value; }

    /// <inheritdoc />
    public int ExtraDescent { get => nativeControl.ExtraDescent; set => nativeControl.ExtraDescent = value; }

    /// <inheritdoc />
    public int FirstVisibleLine { get => nativeControl.FirstVisibleLine; set => nativeControl.FirstVisibleLine = value; }

    /// <inheritdoc />
    public FontQuality FontQuality { get => nativeControl.FontQuality; set => nativeControl.FontQuality = value; }

    /// <inheritdoc />
    public int HighlightGuide { get => nativeControl.HighlightGuide; set => nativeControl.HighlightGuide = value; }

    /// <inheritdoc />
    public bool HScrollBar { get => nativeControl.HScrollBar; set => nativeControl.HScrollBar = value; }

    /// <inheritdoc />
    public IdleStyling IdleStyling { get => nativeControl.IdleStyling; set => nativeControl.IdleStyling = value; }

    /// <inheritdoc />
    public int IndentWidth { get => nativeControl.IndentWidth; set => nativeControl.IndentWidth = value; }

    /// <inheritdoc />
    public IndentView IndentationGuides { get => nativeControl.IndentationGuides; set => nativeControl.IndentationGuides = value; }

    /// <inheritdoc />
    public int IndicatorCurrent { get => nativeControl.IndicatorCurrent; set => nativeControl.IndicatorCurrent = value; }

    /// <inheritdoc />
    public int IndicatorValue { get => nativeControl.IndicatorValue; set => nativeControl.IndicatorValue = value; }

    /// <inheritdoc />
    public bool InternalFocusFlag { get => nativeControl.InternalFocusFlag; set => nativeControl.InternalFocusFlag = value; }

    /// <inheritdoc />
    public string? LexerName { get => nativeControl.LexerName; set => nativeControl.LexerName = value; }

    /// <inheritdoc />
    public Layer SelectionLayer { get => nativeControl.SelectionLayer; set => nativeControl.SelectionLayer = value; }

    /// <inheritdoc />
    public int SelectionEnd { get => nativeControl.SelectionEnd; set => nativeControl.SelectionEnd = value; }

    /// <inheritdoc />
    public int SelectionStart { get => nativeControl.SelectionStart; set => nativeControl.SelectionStart = value; }

    /// <inheritdoc />
    [Obsolete("This property will get more obsolete as time passes as the Scintilla v.5+ now uses strings to define lexers. Please use the LexerName property instead.")]
    public Lexer Lexer { get => nativeControl.Lexer; set => nativeControl.Lexer = value; }

    /// <inheritdoc />
    public string LexerLanguage { get => nativeControl.LexerLanguage; set => nativeControl.LexerLanguage = value; }

    /// <inheritdoc />
    public LineEndType LineEndTypesActive => nativeControl.LineEndTypesActive;

    /// <inheritdoc />
    public LineEndType LineEndTypesAllowed { get => nativeControl.LineEndTypesAllowed; set => nativeControl.LineEndTypesAllowed = value; }

    /// <inheritdoc />
    public LineEndType LineEndTypesSupported => nativeControl.LineEndTypesSupported;

    /// <inheritdoc />
    public int LinesOnScreen => nativeControl.LinesOnScreen;

    /// <inheritdoc />
    public int MainSelection { get => nativeControl.MainSelection; set => nativeControl.MainSelection = value; }

    /// <inheritdoc />
    public bool Modified => nativeControl.Modified;

    /// <inheritdoc />
    public int MouseDwellTime { get => nativeControl.MouseDwellTime; set => nativeControl.MouseDwellTime = value; }

    /// <inheritdoc />
    public bool MouseSelectionRectangularSwitch { get => nativeControl.MouseSelectionRectangularSwitch; set => nativeControl.MouseSelectionRectangularSwitch = value; }

    /// <inheritdoc />
    public bool MultipleSelection { get => nativeControl.MultipleSelection; set => nativeControl.MultipleSelection = value; }

    /// <inheritdoc />
    public MultiPaste MultiPaste { get => nativeControl.MultiPaste; set => nativeControl.MultiPaste = value; }

    /// <inheritdoc />
    public bool OverType { get => nativeControl.OverType; set => nativeControl.OverType = value; }

    /// <inheritdoc />
    public bool PasteConvertEndings { get => nativeControl.PasteConvertEndings; set => nativeControl.PasteConvertEndings = value; }

    /// <inheritdoc />
    public Phases PhasesDraw { get => nativeControl.PhasesDraw; set => nativeControl.PhasesDraw = value; }

    /// <inheritdoc />
    public bool ReadOnly { get => nativeControl.ReadOnly; set => nativeControl.ReadOnly = value; }

    /// <inheritdoc />
    public int RectangularSelectionAnchorVirtualSpace { get => nativeControl.RectangularSelectionAnchorVirtualSpace; set => nativeControl.RectangularSelectionAnchorVirtualSpace = value; }

    /// <inheritdoc />
    public int RectangularSelectionCaretVirtualSpace { get => nativeControl.RectangularSelectionCaretVirtualSpace; set => nativeControl.RectangularSelectionCaretVirtualSpace = value; }

    /// <inheritdoc />
    public int ScrollWidth { get => nativeControl.ScrollWidth; set => nativeControl.ScrollWidth = value; }

    /// <inheritdoc />
    public bool ScrollWidthTracking { get => nativeControl.ScrollWidthTracking; set => nativeControl.ScrollWidthTracking = value; }

    /// <inheritdoc />
    public SearchFlags SearchFlags { get => nativeControl.SearchFlags; set => nativeControl.SearchFlags = value; }

    /// <inheritdoc />
    public string SelectedText => nativeControl.SelectedText;

    /// <inheritdoc />
    public bool SelectionEolFilled { get => nativeControl.SelectionEolFilled; set => nativeControl.SelectionEolFilled = value; }

    /// <inheritdoc />
    public Status Status { get => nativeControl.Status; set => nativeControl.Status = value; }

    /// <inheritdoc />
    public TabDrawMode TabDrawMode { get => nativeControl.TabDrawMode; set => nativeControl.TabDrawMode = value; }

    /// <inheritdoc />
    public bool TabIndents { get => nativeControl.TabIndents; set => nativeControl.TabIndents = value; }

    /// <inheritdoc />
    public int TabWidth { get => nativeControl.TabWidth; set => nativeControl.TabWidth = value; }

    /// <inheritdoc />
    public int TargetEnd { get => nativeControl.TargetEnd; set => nativeControl.TargetEnd = value; }

    /// <inheritdoc />
    public int TargetStart { get => nativeControl.TargetStart; set => nativeControl.TargetStart = value; }

    /// <inheritdoc />
    public string TargetText => nativeControl.TargetText;

    /// <inheritdoc />
    public Technology Technology { get => nativeControl.Technology; set => nativeControl.Technology = value; }

    /// <inheritdoc />
    public string Text { get => nativeControl.Text; set => nativeControl.Text = value; }

    /// <inheritdoc />
    public bool UseTabs { get => nativeControl.UseTabs; set => nativeControl.UseTabs = value; }

    /// <inheritdoc />
    public bool ViewEol { get => nativeControl.ViewEol; set => nativeControl.ViewEol = value; }

    /// <inheritdoc />
    public WhitespaceMode ViewWhitespace { get => nativeControl.ViewWhitespace; set => nativeControl.ViewWhitespace = value; }

    /// <inheritdoc />
    public VirtualSpace VirtualSpaceOptions { get => nativeControl.VirtualSpaceOptions; set => nativeControl.VirtualSpaceOptions = value; }

    /// <inheritdoc />
    public bool VScrollBar { get => nativeControl.VScrollBar; set => nativeControl.VScrollBar = value; }

    /// <inheritdoc />
    public int VisibleLineCount => nativeControl.VisibleLineCount;

    /// <inheritdoc />
    public string WhitespaceChars
    {
        get => nativeControl.WhitespaceChars;
        set => nativeControl.WhitespaceChars = value;
    }

    /// <inheritdoc />
    public int WhitespaceSize { get => nativeControl.WhitespaceSize; set => nativeControl.WhitespaceSize = value; }

    /// <inheritdoc />
    public string WordChars { get => nativeControl.WordChars; set => nativeControl.WordChars = value; }

    /// <inheritdoc />
    public WrapIndentMode WrapIndentMode { get => nativeControl.WrapIndentMode; set => nativeControl.WrapIndentMode = value; }

    /// <inheritdoc />
    public WrapMode WrapMode { get => nativeControl.WrapMode; set => nativeControl.WrapMode = value; }

    /// <inheritdoc />
    public int WrapStartIndent { get => nativeControl.WrapStartIndent; set => nativeControl.WrapStartIndent = value; }

    /// <inheritdoc />
    public WrapVisualFlags WrapVisualFlags { get => nativeControl.WrapVisualFlags; set => nativeControl.WrapVisualFlags = value; }

    /// <inheritdoc />
    public WrapVisualFlagLocation WrapVisualFlagLocation { get => nativeControl.WrapVisualFlagLocation; set => nativeControl.WrapVisualFlagLocation = value; }

    /// <inheritdoc />
    public int XOffset { get => nativeControl.XOffset; set => nativeControl.XOffset = value; }

    /// <inheritdoc />
    public int Zoom { get => nativeControl.Zoom; set => nativeControl.Zoom = value; }
}