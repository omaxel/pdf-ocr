# pdf-ocr
Recognize page content of a PDF as text [Tesseract](https://github.com/charlesw/tesseract) and [Ghostscript](https://www.ghostscript.com/).

## Prerequisites
* Install [Visual Studio 2015 Runtime](https://www.microsoft.com/en-us/download/details.aspx?id=48145) (both x86 & x64)
* Install [Ghostscript](https://www.ghostscript.com/download/gsdnld.html) (x86 or x64, depending on your computer)

## Installation
* Clone or download this repository.
* Open the solution in Visual Studio and run `Install-Package Tesseract -Version 3.0.2` from the `Package Manager Console`.
* Download language data files for tesseract 3.04 from the [tessdata repository](https://github.com/tesseract-ocr/tessdata/archive/3.04.00.zip) and add them to the `tessdata` of your project. Set `Copy to output directory` to `Always` for all the copied files. You can copy only the language files you are interested in (e.g. all the files that starts with `eng` for English language).

## Configuration

##### Input PDF file
*Variable name*: `inputPdfFile`.

*Default*: `test.pdf`, included in the repository.

*Description*: The PDF file whose selected page's content will be recognized as text.

##### Page number
*Variable name*: `pageNumber`.

*Default*: `1`

*Description*: The number of the page whose content will be recognized as text.

##### Recognition language
*Variable name*: `ocrLanguage`.

*Default*: `"eng"`

*Description*: The language used from tesseract to recognize text. When you change this value, make shure you add the language data files to the tessdata folder. See [Installation section](#Installation).


##### DPI converting PDF page to image
*Variable name*: `pdfToImageDPI`.

*Default*: `150`

*Description*: Tesseract can't recognize text from PDF pages. This is way we have to convert the PDF page to an image. This property indicates the DPI when making this convertion.


## Tesseract usage
If you need more information on Tesseract usage, please visit [its own repository](https://github.com/charlesw/tesseract).