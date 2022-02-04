// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Docx.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="LineBreakInline"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class LineBreakInlineRenderer : DocxObjectRenderer<LineBreakInline>
    {
        protected override void Write(DocxRenderer renderer, LineBreakInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (obj.IsHard || renderer.SoftBreaksAsHard)
            {
                renderer.Writer.RunBegin();
                renderer.Writer.TextBreakWrite();
                renderer.Writer.RunEnd();
            }
        }
    }
}
