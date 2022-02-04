// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Docx
{
    /// <summary>
    /// A XAML renderer for a <see cref="CodeBlock"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class CodeBlockRenderer : DocxObjectRenderer<CodeBlock>
    {
        protected override void Write(DocxRenderer renderer, CodeBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            renderer.Writer.ParagraphBegin(renderer.Styles.CodeBlock, true);
            renderer.WriteRawLines(obj);
            renderer.Writer.ParagraphEnd();
        }
    }
}
