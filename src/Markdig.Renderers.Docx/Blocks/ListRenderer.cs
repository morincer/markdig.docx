using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Syntax;

namespace Markdig.Renderers.Docx.Blocks;

public class ListRenderer : DocxObjectRenderer<ListBlock>
{
    protected override void Write(DocxDocumentRenderer renderer, ListBlock obj)
    {
        if (!obj.IsOrdered)
        {
            renderer.BulletListNumberingInstance ??= renderer.Document.AddBulletedListNumbering();
        }
        else
        {
            if (renderer.NumberListAbstractNum == null)
            {
                var numberingInstance = renderer.Document.AddOrderedListNumbering();
                renderer.NumberListAbstractNum = renderer.Document.GetOrCreateNumbering()
                    .NumberingDefinitionsPart!.Numbering
                    .Elements<AbstractNum>()
                    .FirstOrDefault(n => n.AbstractNumberId! == numberingInstance.AbstractNumId!.Val!);
                
            }
            
            Debug.Assert(renderer.NumberListAbstractNum != null, "renderer.NumberListAbstractNum != null");

            renderer.NumberListNumberingInstance = renderer.Document.AddOrderedListNumbering(
                int.TryParse(obj.OrderedStart, out var startFrom) ? startFrom : 1,
                renderer.NumberListAbstractNum
            );
        }
        
        renderer.ActiveList.Push(obj);
        renderer.WriteChildren(obj);
        renderer.ActiveList.Pop();
    }
}