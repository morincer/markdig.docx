using System.IO;
using System.Linq;
using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation;
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
        _validator = new OpenXmlValidator();

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

    private void LoadAndValidate(string fileName)
    {
        _log.LogInformation($"Processing {fileName}");
        var markdownFilePath = Path.Combine(_inputDirectory, fileName);
        Assert.True(File.Exists(markdownFilePath), $"{markdownFilePath} not found");
        var outputFilePath = Path.Combine(_outputDirectory, fileName + ".docx");

        var document = DocxTemplate.Standard;

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