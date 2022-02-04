namespace Markdig.Renderers.Docx;

public class DocxStyles
{
    public Dictionary<int, string?> Headings { get; } = new()
    {
        [0] = "Title",
        [1] = "Heading1",
        [2] = "Heading2",
        [3] = "Heading3",
        [4] = "Heading4",
        [5] = "Heading5",
    };

    public string UndefinedHeading { get; set; } = "Heading5";

    public string Paragraph { get; set; } = "Paragraph";

    public string CodeBlock { get; set; } = "Code";

    public string UnknownFormatting { get; set; } = "Unknown";
    public string Hyperlink { get; set; } = "Hyperlink";
    public string? ListOrdered { get; set; } = "OrderedList";
    public string? ListBullet { get; set; } = "BulletList";
    public string? ListItem { get; set; } = "ListItem";

    public string? Quote { get; set; } = "Quote";
}