// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Docx.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="LiteralInline"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class LiteralInlineRenderer : DocxObjectRenderer<LiteralInline>
    {
        protected override void Write(DocxDocumentRenderer renderer, LiteralInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            if (obj.Content.IsEmpty)
                return;

            var run = new Run(new Text(obj.Content.ToString()) { Space = SpaceProcessingModeValues.Preserve });
            
            if (renderer.TextFormat.TryPeek(out var props))
            {
                run.RunProperties = new RunProperties(props.OuterXml);
            }
            
            renderer.Cursor.Write(run);
        }
    }
}
