// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Docx
{
    /// <summary>
    /// A XAML renderer for a <see cref="ThematicBreakBlock"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class ThematicBreakRenderer : DocxObjectRenderer<ThematicBreakBlock>
    {
        protected override void Write(DocxRenderer renderer, ThematicBreakBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            renderer.Writer.ParagraphBegin();
            renderer.Writer.HorizontalSeparatorWrite();
            renderer.Writer.ParagraphEnd();
        }
    }
}
