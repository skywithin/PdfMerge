using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfMerge
{
    public static class PdfHelper
    {
        /// <summary>
        /// Merges all pdf files and writes ouput to disk. If file already exists, it will be overwritten.
        /// </summary>
        /// <param name="pdfFilePaths">List of file paths.</param>
        public static void MergePdfFilesAndWriteToDisk(string[] sourcePdfFilePaths, string outputFilePath)
        {
            var outputBytes = MergePdfFiles(sourcePdfFilePaths);

            if(outputBytes != null && outputBytes.Any())
            {
                File.WriteAllBytes(outputFilePath, outputBytes);
            }
        }

        /// <summary>
        /// Merges all files provided as a list of file paths.
        /// </summary>
        /// <param name="pdfFilePaths">List of file paths.</param>
        /// <returns>Merged pdf file as byte array.</returns>
        public static byte[] MergePdfFiles(string[] sourcePdfFilePaths)
        {
            var mutiplePdfFileBytes = new List<byte[]>();

            foreach (var pdfFilePath in sourcePdfFilePaths)
            {
                if (File.Exists(pdfFilePath))
                {
                    var fileBytes = File.ReadAllBytes(pdfFilePath);

                    if (fileBytes.Any())
                    {
                        mutiplePdfFileBytes.Add(fileBytes);
                    }
                }
            }

            if (mutiplePdfFileBytes.Any())
            {
                return MergeAllPdfFileBytes(mutiplePdfFileBytes);
            }

            return null;
        }

        /// <summary>
        /// Merges all files provided as a list of byte arrays.
        /// </summary>
        /// <param name="mutiplePdfFileBytes">List of pdf filea as byte arrays.</param>
        /// <returns>Merged pdf file as byte array.</returns>
        private static byte[] MergeAllPdfFileBytes(IEnumerable<byte[]> mutiplePdfFileBytes)
        {
            using (var ms = new MemoryStream())
            {
                using (var document = new Document())
                {
                    using (var mergedPdf = new PdfCopy(document, ms))
                    {
                        document.Open();

                        foreach (byte[] pdfFileBytes in mutiplePdfFileBytes)
                        {
                            if (pdfFileBytes != null && pdfFileBytes.Length > 0)
                            {
                                using (var pdfReader = new PdfReader(pdfFileBytes))
                                {
                                    mergedPdf.AddDocument(pdfReader);
                                }
                            }
                        }
                    }
                }
                return ms.ToArray();
            }
        }
    }
}
