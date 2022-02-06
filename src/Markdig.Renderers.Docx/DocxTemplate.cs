using System.Reflection;
using DocumentFormat.OpenXml.Packaging;

namespace Markdig.Renderers.Docx;

public class DocxTemplate
{
    public static WordprocessingDocument Standard
    {
        get
        {
            var templateResource = "Markdig.Renderers.Docx.Resources.markdown-template.docx";
            return LoadFromResource(templateResource);
        }
    }

    public static WordprocessingDocument LoadFromResource(string templateResource)
    {
        var stream = Assembly.GetExecutingAssembly()
            .GetManifestResourceStream(templateResource)!;
        var ms = new MemoryStream();
        stream.CopyTo(ms);
        
        var document = WordprocessingDocument.Open(ms, true);
        document.MainDocumentPart!.Document.Body!.RemoveAllChildren();
        
        return document;
    }
}