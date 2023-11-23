using System.Diagnostics;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Renderers.Docx.Blocks;
using Markdig.Renderers.Docx.Inlines;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;


namespace Markdig.Renderers.Docx;

public class DocxDocumentRenderer : RendererBase
{
    public WordprocessingDocument Document { get; }
    
    public DocumentTreeCursor Cursor { get; set; }

    internal int NoParagraph { get; set; }= 0;
    
    internal HashSet<string> UsedStyles { get; private set; } = new();
    
    public DocumentStyles Styles { get; }

    internal ILogger<DocxDocumentRenderer> Log { get; }
    internal Stack<RunProperties> TextFormat { get; } = new();
    internal Stack<string> TextStyle { get; } = new();
    
    internal Stack<ListInfo> ActiveList { get; } = new();

    public bool SoftBreaksAsHard { get; set; }

    public DocxDocumentRenderer(WordprocessingDocument document) : this(document, new DocumentStyles(), NullLogger<DocxDocumentRenderer>.Instance)
    {
        
    }
    public DocxDocumentRenderer(WordprocessingDocument document, 
        DocumentStyles styles, 
        ILogger<DocxDocumentRenderer> log)
    {
        Log = log;
        
        Document = document;

        Debug.Assert(Document.MainDocumentPart != null, "Document.MainDocumentPart != null");
        Debug.Assert(Document.MainDocumentPart.Document.Body != null, "Document.MainDocumentPart.Document.Body != null");
        
        Cursor = new DocumentTreeCursor(Document.MainDocumentPart.Document.Body, 
            Document.MainDocumentPart.Document.Body.Elements<Paragraph>().LastOrDefault());
        
        Styles = styles;
            
        // Default block renderers
        ObjectRenderers.Add(new CodeBlockRenderer());
        ObjectRenderers.Add(new ListRenderer());
        ObjectRenderers.Add(new HeadingRenderer());
        // ObjectRenderers.Add(new HtmlBlockRenderer());
        ObjectRenderers.Add(new ParagraphRenderer());
        ObjectRenderers.Add(new ListItemRenderer());
        ObjectRenderers.Add(new QuoteBlockRenderer());
        ObjectRenderers.Add(new ThematicBreakRenderer());

        // Default inline renderers
        ObjectRenderers.Add(new AutolinkInlineRenderer());
        ObjectRenderers.Add(new CodeInlineRenderer());
        ObjectRenderers.Add(new DelimiterInlineRenderer());
        ObjectRenderers.Add(new EmphasisInlineRenderer());
        ObjectRenderers.Add(new LineBreakInlineRenderer());
        // ObjectRenderers.Add(new HtmlInlineRenderer());
        ObjectRenderers.Add(new HtmlEntityInlineRenderer());
        ObjectRenderers.Add(new LinkInlineRenderer());
        ObjectRenderers.Add(new LiteralInlineRenderer());
    }

    public void ForceCloseParagraph()
    {
        Paragraph? topParagraphOnStack = null;
        while (NoParagraph > 0)
        {
            topParagraphOnStack = Cursor.Container as Paragraph;
            Cursor.PopAndAdvanceAfter(null);
            NoParagraph--;
        }

        if (topParagraphOnStack != null)
        {
            Cursor.SetAfter(topParagraphOnStack);
        }
    }

    public override object Render(MarkdownObject markdownObject)
    {
        if (markdownObject is MarkdownDocument)
        {
            DocumentStart();
        }
        
        Write(markdownObject);

        if (markdownObject is MarkdownDocument)
        {
            DocumentEnd();
        }

        return this;
    }

    private void DocumentEnd()
    {
        
    }

    private void DocumentStart()
    {
        
    }


    
    
    
}