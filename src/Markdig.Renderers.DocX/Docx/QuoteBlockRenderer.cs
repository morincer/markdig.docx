// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Docx
{
    /// <summary>
    /// A XAML renderer for a <see cref="QuoteBlock"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class QuoteBlockRenderer : DocxObjectRenderer<QuoteBlock>
    {
        protected override void Write(DocxRenderer renderer, QuoteBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            
            
            renderer.Writer.ParagraphBegin(renderer.Styles.Quote);
            renderer.WriteChildren(obj);
            renderer.Writer.ParagraphEnd();
        }
    }
}
