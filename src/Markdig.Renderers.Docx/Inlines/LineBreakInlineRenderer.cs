using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Docx.Inlines;

public class LineBreakInlineRenderer : DocxObjectRenderer<LineBreakInline>
{
    protected override void Write(DocxDocumentRenderer renderer, LineBreakInline obj)
    {
        base.Write(renderer, obj);
        if (obj.IsHard || renderer.SoftBreaksAsHard)
        {
            renderer.Cursor.Write(new Run(new Break()));
        }
        else
        {
            renderer.Cursor.Write(new Run(new Text(" ") { Space = SpaceProcessingModeValues.Preserve }));
        }

    }
}