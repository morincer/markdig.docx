using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Docx.Inlines;

public class CodeInlineRenderer : DocxObjectRenderer<CodeInline>
{
    protected override void Write(DocxDocumentRenderer renderer, CodeInline obj)
    {
        base.Write(renderer, obj);
        var run = new Run();
        run.SetStyle(renderer.Styles.CodeInline);
        run.AppendChild(new Text(obj.Content));
        renderer.Cursor.Write(run);
    }
}