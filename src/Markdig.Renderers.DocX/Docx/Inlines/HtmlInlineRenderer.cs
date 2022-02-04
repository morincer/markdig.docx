// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Docx.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="HtmlInline"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class HtmlInlineRenderer : DocxObjectRenderer<HtmlInline>
    {
        protected override void Write(DocxRenderer renderer, HtmlInline obj)
        {
            // HTML inlines are not supported
        }
    }
}
