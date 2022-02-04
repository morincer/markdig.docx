// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Docx
{
    /// <summary>
    /// A XAML renderer for a <see cref="HtmlBlock"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class HtmlBlockRenderer : DocxObjectRenderer<HtmlBlock>
    {
        protected override void Write(DocxRenderer renderer, HtmlBlock obj)
        {
            // TODO
        }
    }
}
