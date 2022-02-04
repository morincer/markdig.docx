using System.CodeDom.Compiler;
using Markdig.Renderers;

namespace Markdig.DocX.Tests;

public class TracingWriter : IDocxWriter
{
    public IndentedTextWriter TextWriter { get; }

    public TracingWriter(TextWriter textWriter)
    {
        TextWriter = new IndentedTextWriter(textWriter);
    }
    
    public void DocumentBegin()
    {
        TextWriter.WriteLine("DOCUMENT BEGIN");
        TextWriter.Indent++;
    }

    public void DocumentEnd()
    {
        TextWriter.Indent--;
    }

    public void ParagraphBegin(string? styleId, bool preformatted = false)
    {
        TextWriter.WriteLine($"PARA BEGIN {(preformatted ? "preformatted" : "")} {styleId ?? ""}");
        TextWriter.Indent++;
    }

    public void ParagraphEnd()
    {
        TextWriter.Indent--;
    }

    public void RunBegin(TextFormat? format = null, string? styleId = null)
    {
        TextWriter.WriteLine($"RUN {format} {styleId}");
        TextWriter.Indent++;
    }

    public void TextWrite(string contentText)
    {
        TextWriter.WriteLine($"TXT: {contentText}");
    }

    public void RunEnd()
    {
        TextWriter.Indent--;
    }

    public void TextBreakWrite()
    {
        TextWriter.WriteLine("BR");
    }

    public void WriteImageHyperlink(string? url)
    {
        TextWriter.WriteLine($"Hyperlink Image {url}");
    }

    public void HyperlinkBegin(string? url)
    {
        TextWriter.WriteLine($"HREF {url}");
        TextWriter.Indent++;
    }
    
    public void HyperlinkEnd()
    {
        TextWriter.Indent--;
    }

    public void ListBegin(string? styleId, string? startFrom)
    {
        TextWriter.WriteLine($"LIST {styleId} {startFrom}");
        TextWriter.Indent++;
    }

    public void ListItemBegin(string? styleId)
    {
        TextWriter.WriteLine($"LI {styleId}");
        TextWriter.Indent++;
    }

    public void ListItemEnd()
    {
        TextWriter.Indent--;
    }

    public void ListEnd()
    {
        TextWriter.Indent--;
    }

    public void HorizontalSeparatorWrite()
    {
        TextWriter.WriteLine("HR ----");
    }
}