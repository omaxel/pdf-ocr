using Ghostscript.NET.Rasterizer;
using System;
using System.IO;
using Tesseract;

/**
 * Recognizing text on a PDF page using Tesseract and Ghostscript
 * 
 * For more information visit: https://github.com/OmarMuscatello/pdf-ocr
 */

namespace pdf_ocr
{
    class Program
    {
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string TempDir = Path.Combine(BaseDirectory, "Temp");

        #region Settings

        /// <summary>
        /// The input PDF file
        /// </summary>
        private static string inputPdfFile = Path.Combine(BaseDirectory, "test.pdf");

        /// <summary>
        /// The page from which recognize text
        /// </summary>
        private const int pageNumber = 1;

        /// <summary>
        /// Language of text recognition. Depends on the files you have in the tessadata directory. See installation instruction on GitHub repository.
        /// </summary>
        private const string ocrLanguage = "eng";

        /// <summary>
        /// Pixel density used to convert the PDF file to image
        /// </summary>
        private const int pdfToImageDPI = 150;

        #endregion

        private static void Main(string[] args)
        {
            Directory.CreateDirectory(TempDir);

            Console.WriteLine($"Recognizing text on page {pageNumber} of file {inputPdfFile}");
            Console.WriteLine();

            //Converting the PDF page to an image
            var tempImageFile = GetImageFromPdf(inputPdfFile, pageNumber);

            //Recognizing text from the generated image
            var recognizedText = GetTextFromImage(tempImageFile);

            Console.WriteLine($"Recognized text on page");
            Console.WriteLine($"==========================");
            Console.WriteLine(recognizedText);
            Console.WriteLine($"==========================");

            Directory.Delete(TempDir, true);

            Console.ReadKey();
        }

        /// <summary>
        /// Get an image from a PDF page.
        /// </summary>
        /// <param name="pdfPath"></param>
        /// <param name="pageNumber"></param>
        /// <returns>The path of the generated image.</returns>
        private static string GetImageFromPdf(string pdfPath, int pageNumber)
        {
            var outputFilePath = GetTempFile(".png");

            var ghostscriptRasterizer = new GhostscriptRasterizer();
            ghostscriptRasterizer.Open(pdfPath);

            using (var img = ghostscriptRasterizer.GetPage(pdfToImageDPI, pdfToImageDPI, pageNumber))
            {
                img.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Png);
                img.Dispose();
            }

            ghostscriptRasterizer.Close();

            return outputFilePath;
        }

        /// <summary>
        /// Get text from the specified image file.
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        private static string GetTextFromImage(string imagePath)
        {
            var result = "";

            using (var img = Pix.LoadFromFile(imagePath))
            {
                using (var tesseractEngine = new TesseractEngine("tessdata", ocrLanguage, EngineMode.Default))
                {
                    using (var page = tesseractEngine.Process(img))
                    {
                        result = page.GetText();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the path of a new file in the <see cref="TempDir"/> directory. The file is not created.
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        private static string GetTempFile(string extension = ".tmp")
        {
            return Path.Combine(TempDir, Guid.NewGuid().ToString() + extension);
        }
    }
}
