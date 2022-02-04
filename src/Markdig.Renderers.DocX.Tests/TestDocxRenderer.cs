using Markdig.Renderers;
using Markdig.Renderers.Docx;
using Moq;
using NUnit.Framework;

namespace Markdig.DocX.Tests;

[TestFixture]
public class TestDocxRenderer
{
    [Test]
    public void ShouldRenderDocumentWithoutExceptions()
    {
        var writer = new TracingWriter(Console.Out);

        var styles = new DocxStyles();
        var renderer = new DocxRenderer(writer, styles);

        Markdown.Convert(File.ReadAllText("data/simple.md"), renderer);
    }
}