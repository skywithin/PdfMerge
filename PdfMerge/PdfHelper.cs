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
        public static byte[] MergePdfDocuments(string[] pdfFilePaths)
        {
            if (pdfFilePaths == null || pdfFilePaths.Any())
            {
                throw new ArgumentException("File paths not provided");
            }

            List<byte[]> mutiplePdfFileBytes = new List<byte[]>();

            foreach (string pdfFilePath in pdfFilePaths)
            {
                if (File.Exists(pdfFilePath))
                {
                    byte[] fileBytes = File.ReadAllBytes(pdfFilePath);

                    if (fileBytes.Length > 0)
                    {
                        mutiplePdfFileBytes.Add(fileBytes);
                    }
                }
            }

            return PdfHelper.MergePdfDocuments(mutiplePdfFileBytes);
        }

        public static byte[] MergePdfDocuments(List<byte[]> mutiplePdfFileBytes)
        {
            if (mutiplePdfFileBytes == null || mutiplePdfFileBytes.Any())
            {
                throw new ArgumentException("File bytes not provided");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document())
                {
                    using (PdfCopy mergedPdf = new PdfCopy(document, ms))
                    {
                        document.Open();

                        foreach (byte[] pdfFileBytes in mutiplePdfFileBytes)
                        {
                            if (pdfFileBytes != null && pdfFileBytes.Length > 0)
                            {
                                using (PdfReader pdfReader = new PdfReader(pdfFileBytes))
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
