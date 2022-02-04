using Markdig.Syntax;

namespace Markdig.Renderers.Docx
{
    /// <summary>
    /// A base class for XAML rendering <see cref="Block"/> and <see cref="Syntax.Inlines.Inline"/> Markdown objects.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <seealso cref="IMarkdownObjectRenderer" />
    public abstract class DocxObjectRenderer<TObject> : MarkdownObjectRenderer<DocxRenderer, TObject>
        where TObject : MarkdownObject
    {
        
    }
}
