using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Syntax;

namespace Markdig.Renderers.Docx.Blocks;

public class ThematicBreakRenderer : LeafBlockParagraphRendererBase<ThematicBreakBlock>
{
    protected override void Write(DocxDocumentRenderer renderer, ThematicBreakBlock obj)
    {
        base.Write(renderer, obj);
        var p = WriteAsParagraph(renderer, obj, renderer.Styles.HorizontalLine);
        if (!p.Elements<Run>().Any())
        {
            p.AppendChild(new Run(new Text("")));
        }
    }
}