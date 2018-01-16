using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfMerge
{
    public static class PdfHelper
    {
        /// <summary>
        /// Merges all files provided as a list of file paths.
        /// </summary>
        /// <param name="pdfFilePaths">List of file paths.</param>
        /// <returns>Merged pdf file as byte array.</returns>
        public static byte[] MergePdfDocuments(List<string> pdfFilePaths)
        {
            if (pdfFilePaths == null || pdfFilePaths.Any())
            {
                throw new ArgumentException("File paths not provided");
            }

            var mutiplePdfFileBytes = new List<byte[]>();

            foreach (var pdfFilePath in pdfFilePaths)
            {
                if (File.Exists(pdfFilePath))
                {
                    var fileBytes = File.ReadAllBytes(pdfFilePath);

                    if (fileBytes.Length > 0)
                    {
                        mutiplePdfFileBytes.Add(fileBytes);
                    }
                }
            }

            return MergePdfDocuments(mutiplePdfFileBytes);
        }

        /// <summary>
        /// Merges all files provided as a list of byte arrays.
        /// </summary>
        /// <param name="mutiplePdfFileBytes">List of pdf filea as byte arrays.</param>
        /// <returns>Merged pdf file as byte array.</returns>
        public static byte[] MergePdfDocuments(List<byte[]> mutiplePdfFileBytes)
        {
            if (mutiplePdfFileBytes == null || mutiplePdfFileBytes.Any())
            {
                throw new ArgumentException("File bytes not provided");
            }

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
