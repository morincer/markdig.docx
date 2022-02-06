using System.IO;
using System.Linq;
using System.Reflection;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;

namespace Markdig.Renderers.Docx.Tests;

public class TestDocxDocumentRenderer
{
    private static string[] _files = {
        /*"headings.md",
        "paragraph.md",
        "code.md",
        "emphasis.md",
        "list-single.md",*/
        "list-multi.md",
    };
    
    private OpenXmlValidator validator;

    private ILoggerFactory _factory = LoggerFactory.Create(builder =>
        builder.AddSerilog()
    );

    private ILogger<TestDocxDocumentRenderer> _log;
    private WordDocumentStyles styles;
    public TestDocxDocumentRenderer()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .CreateLogger();

        _log = _factory.CreateLogger<TestDocxDocumentRenderer>();
        styles = new();
        validator = new OpenXmlValidator();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ShouldConvertFilesWithoutErrors()
    {
        var inputDirectory = Path.GetFullPath("Resources/");
        var outputDirectory = Path.GetFullPath("output/");
        if (Directory.Exists(outputDirectory))
        {
            Directory.Delete(outputDirectory, true);
        }

        Directory.CreateDirectory(outputDirectory);

        var logger = _factory.CreateLogger<DocxDocumentRenderer>();
        
        foreach (var fileName in _files)
        {
            _log.LogInformation($"Processing {fileName}");
            var markdownFilePath = Path.Combine(inputDirectory, fileName);
            Assert.True(File.Exists(markdownFilePath), $"{markdownFilePath} not found");
            var outputFilePath = Path.Combine(outputDirectory, fileName + ".docx");

            var document = DocxTemplate.Standard;

            var renderer = new DocxDocumentRenderer(document, styles, logger);
            var pipeline = new MarkdownPipelineBuilder().UseEmphasisExtras().Build();
            Markdown.Convert(File.ReadAllText(markdownFilePath), renderer, pipeline);

            document.SaveAs(outputFilePath);
            
            AssertValid(document);
            document.Close();
            
        }
    }
    
    private void AssertValid(WordprocessingDocument document)
    {
        var validationResult = this.validator.Validate(document).ToList();

        foreach (var info in validationResult)
        {
            _log.LogInformation($"{info.Node?.LocalName}: {info.Description}");
            _log.LogInformation(info.Node?.OuterXml);
        }

        Assert.That(validationResult, Is.Empty);
    }
}