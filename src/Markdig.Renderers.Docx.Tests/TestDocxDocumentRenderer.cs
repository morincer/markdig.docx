using System.IO;
using System.Linq;
using System.Reflection;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;

#pragma warning disable CS8618

namespace Markdig.Renderers.Docx.Tests;

public class TestDocxDocumentRenderer
{
    private OpenXmlValidator _validator;

    private readonly ILoggerFactory _factory = LoggerFactory.Create(builder =>
        builder.AddSerilog()
    );

    private ILogger<TestDocxDocumentRenderer> _log;
    private WordDocumentStyles _styles;
    private string _inputDirectory;
    private string _outputDirectory;
    private ILogger<DocxDocumentRenderer> _rendererLogger;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .CreateLogger();

        _log = _factory.CreateLogger<TestDocxDocumentRenderer>();
        _styles = new();
        _validator = new OpenXmlValidator(FileFormatVersions.Office2016);

        _inputDirectory = Path.GetFullPath("Resources/");
        _outputDirectory = Path.GetFullPath("output/");

        if (Directory.Exists(_outputDirectory))
        {
            Directory.Delete(_outputDirectory, true);
        }

        Directory.CreateDirectory(_outputDirectory);

        this._rendererLogger = _factory.CreateLogger<DocxDocumentRenderer>();
    }

    [Test]
    public void ShouldConvertCodeWithoutErrors()
    {
        LoadAndValidate("code.md");
    }

    [Test]
    public void ShouldConvertEmphasisWithoutErrors()
    {
        LoadAndValidate("emphasis.md");
    }

    [Test]
    public void ShouldConvertHeadingsWithoutErrors()
    {
        LoadAndValidate("headings.md");
    }

    [Test]
    public void ShouldConvertMultiLevelListsWithoutErrors()
    {
        LoadAndValidate("list-multi.md");
    }

    [Test]
    public void ShouldConvertOneLevelListsWithoutErrors()
    {
        LoadAndValidate("list-single.md");
    }

    [Test]
    public void ShouldConvertHyperlinksWithoutErrors()
    {
        LoadAndValidate("hyperlinks.md");
    }

    [Test]
    public void ShouldConvertQuoteWithoutErrors()
    {
        LoadAndValidate("quote.md");
    }

    [Test]
    public void ShouldConvertBreakWithoutErrors()
    {
        LoadAndValidate("break.md");
    }
    
    [Test]
    public void ShouldConvertPictureLinkWithoutErrors()
    {
        LoadAndValidate("picture-link.md");
    }
    
    [Test]
    public void ShouldConvertStandardTemplate()
    {
        LoadAndValidate("standard-template.md");
    }

    [Test]
    public void ShouldRenderIntoUserProvidedPosition()
    {
        var document =
            DocxTemplateHelper.LoadFromResource("Markdig.Renderers.Docx.Tests.Resources.docx.template-to-insert.docx");

        var paragraph = DocxTemplateHelper.FindParagraphContainingText(document, "INSERT");
        Assert.NotNull(paragraph);

        var renderer = new DocxDocumentRenderer(document, _styles, _rendererLogger);
        renderer.Cursor.SetAfter(paragraph);
        Markdown.Convert("**Markdown Paragraph 1 - bold** text\n\nParagraph2", renderer);
        paragraph!.Remove();

        document.SaveAs(Path.Combine(_outputDirectory, "template-to-insert.md.docx"));

        AssertValid(document);
    }

    [Test]
    public void ShouldRenderIntoManyPositions()
    {
        var document =
            DocxTemplateHelper.LoadFromResource("Markdig.Renderers.Docx.Tests.Resources.docx.template-to-insert-multi.docx");

        var paragraph = DocxTemplateHelper.FindParagraphContainingText(document, "INSERT1");
        Assert.NotNull(paragraph);

        var renderer = new DocxDocumentRenderer(document, _styles, _rendererLogger);
        renderer.Cursor.SetAfter(paragraph);
        Markdown.Convert("**Insert 1** here", renderer);
        paragraph!.Remove();

        paragraph = DocxTemplateHelper.FindParagraphContainingText(document, "INSERT2");
        Assert.NotNull(paragraph);
        renderer.Cursor.SetAfter(paragraph);
        Markdown.Convert("**Insert 2** here", renderer);
        paragraph!.Remove();

        document.SaveAs(Path.Combine(_outputDirectory, "template-to-insert-multi.md.docx"));

        AssertValid(document);
    }

    [Test]
    public void ShouldRenderIntoUserProvidedPositionInTable()
    {
        var document =
            DocxTemplateHelper.LoadFromResource("Markdig.Renderers.Docx.Tests.Resources.docx.template-to-insert-table.docx");

        var paragraph = DocxTemplateHelper.FindParagraphContainingText(document, "INSERT");
        Assert.NotNull(paragraph);

        var renderer = new DocxDocumentRenderer(document, _styles, _rendererLogger);
        renderer.Cursor.SetAfter(paragraph);
        Markdown.Convert("**Markdown Paragraph 1 - bold** text\n\nParagraph2", renderer);
        paragraph!.Remove();

        document.SaveAs(Path.Combine(_outputDirectory, "template-to-insert-table.md.docx"));

        AssertValid(document);
    }


    private void LoadAndValidate(string fileName)
    {
        _log.LogInformation($"Processing {fileName}");
        var markdownFilePath = Path.Combine(_inputDirectory, fileName);
        Assert.True(File.Exists(markdownFilePath), $"{markdownFilePath} not found");
        var outputFilePath = Path.Combine(_outputDirectory, fileName + ".docx");

        var document = DocxTemplateHelper.Standard;

        var renderer = new DocxDocumentRenderer(document, _styles, _rendererLogger);
        var pipeline = new MarkdownPipelineBuilder().UseEmphasisExtras().Build();
        Markdown.Convert(File.ReadAllText(markdownFilePath), renderer, pipeline);

        document.SaveAs(outputFilePath);

        AssertValid(document);
        document.Close();
    }


    private void AssertValid(WordprocessingDocument document)
    {
        var validationResult = this._validator.Validate(document).ToList();

        foreach (var info in validationResult)
        {
            _log.LogInformation($"{info.Node?.LocalName}: {info.Description}");
            _log.LogInformation(info.Node?.OuterXml);
        }

        Assert.That(validationResult, Is.Empty);
    }
}