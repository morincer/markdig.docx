using DocumentFormat.OpenXml.Math;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Docx.Inlines;

public class DelimiterInlineRenderer : DocxObjectRenderer<DelimiterInline>
{
    protected override void Write(DocxDocumentRenderer renderer, DelimiterInline obj)
    {
        base.Write(renderer, obj);
        renderer.Cursor.Write(new Run(new Text(obj.ToLiteral())));
    }
}