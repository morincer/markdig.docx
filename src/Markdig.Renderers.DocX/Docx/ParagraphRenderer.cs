using Markdig.Syntax;

namespace Markdig.Renderers.Docx
{
    /// <summary>
    /// A XAML renderer for a <see cref="ParagraphBlock"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class ParagraphRenderer : DocxObjectRenderer<ParagraphBlock>
    {
        protected override void Write(DocxRenderer renderer, ParagraphBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            
            renderer.Writer.ParagraphBegin(renderer.Styles.Paragraph);
            renderer.WriteLeafInline(obj);
            renderer.Writer.ParagraphEnd();
        }
    }
}
