using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Syntax;
using Microsoft.Extensions.Logging;

namespace Markdig.Renderers.Docx.Blocks;

public abstract class ParagraphRendererBase<T> : DocxObjectRenderer<T> where T : Block
{
    protected Paragraph WriteAsParagraph(DocxDocumentRenderer renderer, T obj, string? styleId)
    {
        try
        {
            var p = new Paragraph();
            p.SetStyle(styleId);

            if (renderer.NoParagraph == 0)
            {
                renderer.Cursor.Write(p);
                renderer.Cursor.GoInto(p);
                renderer.Log.LogDebug($"Paragraph start {styleId}");
            }
            else
            {
                renderer.Log.LogDebug($"Paragraph as run start {styleId}");
            }
            
            renderer.NoParagraph++;

            RenderContents(renderer, obj);

            /* Paragraph has been closed by somebody else during render (for example, nested list item) - let them handle
             the consequences */
            if (renderer.NoParagraph == 0) return p;

            renderer.NoParagraph--;

            if (renderer.NoParagraph == 0)
            {
                renderer.Cursor.GoOut();
            }

            return p;
        }
        finally
        {
            renderer.Log.LogDebug("Paragraph end");
        }
    }

    protected abstract void RenderContents(DocxDocumentRenderer renderer, T block);
}