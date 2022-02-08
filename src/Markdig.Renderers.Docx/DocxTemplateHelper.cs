using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Markdig.Renderers.Docx;

public class DocxTemplateHelper
{
    public static WordprocessingDocument Standard
    {
        get
        {
            var templateResource = "Markdig.Renderers.Docx.Resources.markdown-template.docx";
            return LoadFromResource(templateResource, true);
        }
    }

    public static WordprocessingDocument LoadFromResource(string templateResource, bool clean = false)
    {
        var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(templateResource);

        if (stream == null)
        {
            stream = Assembly.GetCallingAssembly().GetManifestResourceStream(templateResource);
        }

        if (stream == null)
        {
            throw new FileNotFoundException($"Failed to load resource from {templateResource}");
        }
        
        var ms = new MemoryStream();
        stream.CopyTo(ms);
        
        var document = WordprocessingDocument.Open(ms, true);
        
        if (clean)
        {
            document.MainDocumentPart!.Document.Body!.RemoveAllChildren();
            if (document.MainDocumentPart?.NumberingDefinitionsPart?.Numbering != null)
            {
                document.MainDocumentPart.NumberingDefinitionsPart.Numbering.RemoveAllChildren<NumberingInstance>();
            }
        }

        return document;
    }

    public static Paragraph? FindParagraphContainingText(WordprocessingDocument document, string text)
    {
        if (document.MainDocumentPart == null || document.MainDocumentPart.Document.Body == null) return null;

        var textElement = document.MainDocumentPart.Document.Body
            .Descendants<Text>().FirstOrDefault(t => t.Text.Contains(text));

        if (textElement == null) return null;

        var p = textElement.Ancestors<Paragraph>().FirstOrDefault();
        return p;
    }
}