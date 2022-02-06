using Markdig.Syntax;

namespace Markdig.Renderers.Docx;

public abstract class DocxObjectRenderer<T> : MarkdownObjectRenderer<DocxDocumentRenderer, T> where T : MarkdownObject
{
    
}