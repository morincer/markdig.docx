using Markdig.Syntax;
using Microsoft.Extensions.Logging;

namespace Markdig.Renderers.Docx;

public abstract class DocxObjectRenderer<T> : MarkdownObjectRenderer<DocxDocumentRenderer, T> where T : MarkdownObject
{
    protected override void Write(DocxDocumentRenderer renderer, T obj)
    {
        renderer.Log.LogDebug($"Processing {typeof(T).Name} with {GetType().Name}");
    }
}