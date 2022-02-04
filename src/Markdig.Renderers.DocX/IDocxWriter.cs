namespace Markdig.Renderers;

public enum TextFormat
{
    Bold, Italic, Strikethrough, Subscript, Superscript,
    Inserted,
    Marked
}
public interface IDocxWriter
{
    void DocumentBegin();
    void DocumentEnd();
    void ParagraphBegin(string? styleId = null, bool preformatted = false);
    void ParagraphEnd();
    
    void RunBegin(TextFormat? format = null, string? styleId = null);
    void TextWrite(string contentText);
    void RunEnd();
    void TextBreakWrite();
    void WriteImageHyperlink(string? url);
    void HyperlinkBegin(string? url);
    void HyperlinkEnd();
    void ListBegin(string? styleId, string? startFrom);
    void ListItemBegin(string? styleId);
    void ListItemEnd();
    void ListEnd();
    void HorizontalSeparatorWrite();
}