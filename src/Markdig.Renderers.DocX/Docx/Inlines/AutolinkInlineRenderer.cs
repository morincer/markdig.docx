// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Docx.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="AutolinkInline"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class AutolinkInlineRenderer : DocxObjectRenderer<AutolinkInline>
    {
        protected override void Write(DocxRenderer renderer, AutolinkInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var url = obj.Url;
            if (obj.IsEmail)
            {
                url = "mailto:" + url;
            }

            renderer.Writer.HyperlinkBegin(url);
            renderer.WriteRunText(obj.Url);
            renderer.Writer.HyperlinkEnd();
        }
    }
}
