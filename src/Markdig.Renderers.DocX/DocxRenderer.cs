using Markdig.Helpers;
using Markdig.Renderers.Docx;
using Markdig.Renderers.Docx.Inlines;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using HeadingRenderer = Markdig.Renderers.Docx.HeadingRenderer;

namespace Markdig.Renderers
{
    /// <summary>
    /// XAML renderer for a Markdown <see cref="MarkdownDocument"/> object.
    /// </summary>
    /// <seealso cref="Renderers.TextRendererBase{T}" />
    public class DocxRenderer : RendererBase
    {
        public IDocxWriter Writer { get; }
        public DocxStyles Styles { get; }
        public bool SoftBreaksAsHard { get; set; } = false;

        public TextFormat? StickyFormat { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocxRenderer"/> class.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public DocxRenderer(IDocxWriter writer, DocxStyles styles)
        {
            Writer = writer;
            Styles = styles;
            
            // Default block renderers
            ObjectRenderers.Add(new CodeBlockRenderer());
            ObjectRenderers.Add(new ListRenderer());
            ObjectRenderers.Add(new HeadingRenderer());
            ObjectRenderers.Add(new HtmlBlockRenderer());
            ObjectRenderers.Add(new ParagraphRenderer());
            ObjectRenderers.Add(new QuoteBlockRenderer());
            ObjectRenderers.Add(new ThematicBreakRenderer());

            // Default inline renderers
            ObjectRenderers.Add(new AutolinkInlineRenderer());
            ObjectRenderers.Add(new CodeInlineRenderer());
            ObjectRenderers.Add(new DelimiterInlineRenderer());
            ObjectRenderers.Add(new EmphasisInlineRenderer());
            ObjectRenderers.Add(new LineBreakInlineRenderer());
            ObjectRenderers.Add(new HtmlInlineRenderer());
            ObjectRenderers.Add(new HtmlEntityInlineRenderer());
            ObjectRenderers.Add(new LinkInlineRenderer());
            ObjectRenderers.Add(new LiteralInlineRenderer());

        }

        public override object Render(MarkdownObject markdownObject)
        {
            if (markdownObject is MarkdownDocument)
            {
                Writer.DocumentBegin();
                Write(markdownObject);
                Writer.DocumentEnd();
            }
            else
            {
                Write(markdownObject);
            }

            return this;
        }

        public void WriteLeafInline(LeafBlock leafBlock)
        {
            if (leafBlock is null) throw new ArgumentException($"Leaf block is empty");
            var inline = (Inline) leafBlock.Inline!;

            while (inline != null)
            {
                Write(inline);
                inline = inline.NextSibling;
            }
        }

        public void WriteRawLines(LeafBlock leafBlock)
        {
            if (leafBlock == null) throw new ArgumentException("Leaf block is null");

            var lines = leafBlock.Lines;

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines.Lines[i];
                Writer.RunBegin(StickyFormat);
                Writer.TextWrite(line.ToString() ?? "");
                Writer.RunEnd();
                if (i < lines.Count - 1)
                {
                    Writer.TextBreakWrite();
                }
            }
        }

        public void WriteRunText(string? text,
            string? styleId = null)
        {
            Writer.RunBegin(StickyFormat, styleId);
            Writer.TextWrite(text ?? "");
            Writer.RunEnd();
        }
    }
}