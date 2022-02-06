using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Syntax.Inlines;
using Microsoft.Extensions.Logging;

namespace Markdig.Renderers.Docx.Inlines;

public class LinkInlineRenderer : DocxObjectRenderer<LinkInline>
{
    private int _hyperlinkIdCounter = 1;

    protected override void Write(DocxDocumentRenderer renderer, LinkInline obj)
    {
        base.Write(renderer, obj);
        renderer.Log.LogDebug($"Rendering link to {obj.Url}");
        
        Uri? uri = null;

        var isAbsoluteUri = Uri.TryCreate(obj.Url, UriKind.Absolute, out uri);

        if (!isAbsoluteUri)
        {
            Uri.TryCreate(obj.Url, UriKind.Relative, out uri);
        }

        if (uri == null)
        {
            renderer.WriteChildren(obj);
        }
        else
        {
            var linkId = $"L{_hyperlinkIdCounter++}";
            Debug.Assert(renderer.Document.MainDocumentPart != null, "Document.MainDocumentPart != null");

            renderer.Document.MainDocumentPart.AddHyperlinkRelationship(uri, isAbsoluteUri, linkId);
            var hl = new Hyperlink()
            {
                Id = linkId,
            };
            renderer.Cursor.Write(hl);
            renderer.Cursor.GoInto(hl);
            
            renderer.TextStyle.Push(renderer.Styles.Hyperlink);
            renderer.WriteChildren(obj);
            renderer.TextStyle.Pop();
            
            renderer.Cursor.GoOut();
        }

    }
}