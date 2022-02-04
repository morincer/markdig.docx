// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Syntax;

namespace Markdig.Renderers.Docx
{
    /// <summary>
    /// A XAML renderer for a <see cref="ListBlock"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{TObject}" />
    public class ListRenderer : DocxObjectRenderer<ListBlock>
    {
        protected override void Write(DocxRenderer renderer, ListBlock listBlock)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (listBlock == null) throw new ArgumentNullException(nameof(listBlock));

            renderer.Writer.ParagraphBegin();

            renderer.Writer.ListBegin(
                listBlock.IsOrdered ? renderer.Styles.ListOrdered : renderer.Styles.ListBullet, 
                listBlock.OrderedStart);

            foreach (var item in listBlock)
            {
                var listItem = (ListItemBlock)item;

                renderer.Writer.ListItemBegin(renderer.Styles.ListItem);
                renderer.WriteChildren(listItem);
                renderer.Writer.ListItemEnd();

            }
            
            renderer.Writer.ListEnd();
            
            renderer.Writer.ParagraphEnd();
        }
    }
}
