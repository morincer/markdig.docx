// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Docx.Inlines
{
    /// <summary>
    /// A XAML renderer for an <see cref="EmphasisInline"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class EmphasisInlineRenderer : DocxObjectRenderer<EmphasisInline>
    {
        protected override void Write(DocxRenderer renderer, EmphasisInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            TextFormat? format = null;
            
            switch (obj.DelimiterChar)
            {
                case '*':
                case '_':
                    format = obj.DelimiterCount == 2 ? TextFormat.Bold : TextFormat.Italic;
                    break;
                case '~':
                    format = obj.DelimiterCount == 2 ? TextFormat.Strikethrough : TextFormat.Subscript;
                    break;
                case '^':
                    format = TextFormat.Superscript;
                    break;
                case '+':
                    format = TextFormat.Inserted;
                    break;
                case '=':
                    format = TextFormat.Marked;
                    break;
            }
            
            renderer.StickyFormat = format;
            renderer.WriteChildren(obj);
            renderer.StickyFormat = null;
        }
    }
}