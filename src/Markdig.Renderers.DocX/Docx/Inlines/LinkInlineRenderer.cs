// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Docx.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="LinkInline"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class LinkInlineRenderer : DocxObjectRenderer<LinkInline>
    {
        protected override void Write(DocxRenderer renderer, LinkInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var url = obj.GetDynamicUrl?.Invoke() ?? obj.Url;

            if (obj.IsImage)
            {
                renderer.Writer.WriteImageHyperlink(url);
            }
            else
            {
                renderer.Writer.HyperlinkBegin(url);
                renderer.WriteChildren(obj);
                renderer.Writer.HyperlinkEnd();
                
            }
        }
    }
}
