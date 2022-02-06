﻿using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Syntax;

namespace Markdig.Renderers.Docx.Blocks;

public class HeadingRenderer : LeafBlockParagraphRendererBase<HeadingBlock>
{
    protected override void Write(DocxDocumentRenderer renderer, HeadingBlock obj)
    {
        base.Write(renderer, obj);
        
        var styleId = renderer.Styles.Headings.GetValueOrDefault(obj.Level, renderer.Styles.UndefinedHeading);
        WriteAsParagraph(renderer, obj, styleId);
    }
}