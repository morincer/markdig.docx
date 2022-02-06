using Markdig.Syntax;

namespace Markdig.Renderers.Docx.Blocks;

public class QuoteBlockRenderer : ContainerBlockParagraphRendererBase<QuoteBlock>
{
    protected override void Write(DocxDocumentRenderer renderer, QuoteBlock obj)
    {
        base.Write(renderer, obj);
        WriteAsParagraph(renderer, obj, renderer.Styles.Quote);
    }
}