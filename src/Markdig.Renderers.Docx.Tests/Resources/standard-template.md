# Markdown to DOCX Empty Document Template

This document is used to generate an empty document template for the docx conversion.

The page content is ignored and only used to show/modify all the styles used by the markdown conversion process.

**DO NOT CHANGE PAGE CONTENT IN THIS DOCUMENT**

Changes you can make that will be used to generate DOCX from markdown:

1.	All styles inherit from docx style "MD Normal" you should first adjust the font family and font size of this style, if desired.

## Heading Styles

# Heading 1 docx style "MD Heading 1"

## Heading 2 docx style "MD Heading 2"

### Heading 3 docx style "MD Heading 3"

#### Heading 4 docx style "MD Heading 4"

##### Heading 5 docx style "MD Heading 5"

###### Heading 6 docx style "MD Heading 6"

Horizontal Line Style
HORIZONTAL_LINE_STYLE docx style named "MD Horizontal Line" generates the following horizontal rule

-----------

## Paragraph Styles

Style named "MD Paragraph Text Body" is used for text and list item content

* list item
* list item

> BLOCK_QUOTE_STYLE docx style named "Quotations" is used for block quotes

```
PREFORMATTED_TEXT_STYLE docx style named "Preformatted Text" used for code fence and indented code
```

Character styles used in text formatting:

* INLINE_CODE_STYLE - docx style named "Source Text" `plain sample plain`
* HYPERLINK_STYLE - docx style named "Hyperlink" [plain sample plain](http://www.google.com)

Bullet list (Given by numbering list style BulletList or default numbering list style if one is not given)

* Bullet Level 1
  * Bullet Level 2
    * Bullet Level 3
      * Bullet Level 4
        * Bullet Level 5
          * Bullet Level 6
            * Bullet Level 7
              * Bullet Level 8
                * Bullet Level 9

Numbered List (Given by numbering list style NumberedList or default numbering list style if one is not given)

1. Numbered Level 1
   1. Numbered Level 2
      1. Numbered Level 3
         1. Numbered Level 4
            1. Numbered Level 5
               1. Numbered Level 6
                  1. Numbered Level 7
                     1. Numbered Level 8
                        1. Numbered Level 9
