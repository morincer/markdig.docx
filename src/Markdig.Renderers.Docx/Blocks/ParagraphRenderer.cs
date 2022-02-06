using Markdig.Syntax;

namespace Markdig.Renderers.Docx.Blocks;

public class ParagraphRenderer : LeafBlockParagraphRendererBase<ParagraphBlock>
{
    protected override void Write(DocxDocumentRenderer renderer, ParagraphBlock obj)
    {
        WriteAsParagraph(renderer, obj, renderer.Styles.Paragraph);
    }
}