using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Syntax;

namespace Markdig.Renderers.Docx.Blocks;

public class ListItemRenderer : ContainerBlockParagraphRendererBase<ListItemBlock>
{
    protected override void Write(DocxDocumentRenderer renderer, ListItemBlock obj)
    {
        renderer.ForceCloseParagraph();
        
        var activeList = renderer.ActiveList.Peek();

        renderer.ListLevel++;
        
        var p =WriteAsParagraph(renderer, obj, activeList.IsOrdered ? renderer.Styles.ListOrdered : renderer.Styles.ListBullet);

        if (renderer.ListLevel > 1)
        {
            p.GetOrCreateProperties().NumberingProperties = new NumberingProperties
            {
                NumberingLevelReference = new NumberingLevelReference {Val = renderer.ListLevel - 1}
            };
        }

        renderer.ListLevel--;
    }
}