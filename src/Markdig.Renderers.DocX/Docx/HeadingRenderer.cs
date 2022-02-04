// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Docx
{
    /// <summary>
    /// An XAML renderer for a <see cref="HeadingBlock"/>.
    /// </summary>
    /// <seealso cref="DocxObjectRenderer{TObject}" />
    public class HeadingRenderer : DocxObjectRenderer<HeadingBlock>
    {
        protected override void Write(DocxRenderer renderer, HeadingBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            string? styleId = null;


            styleId = renderer.Styles.Headings.GetValueOrDefault(obj.Level, renderer.Styles.UndefinedHeading);

            renderer.Writer.ParagraphBegin(styleId);
            renderer.WriteLeafInline(obj);
            renderer.Writer.ParagraphEnd();
        }
    }
}
