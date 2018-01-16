using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var pdfFilePaths = new List<string> {
                    @"C:\Temp\TestFile1.pdf",
                    @"C:\Temp\TestFile2.pdf",
                    @"C:\Temp\TestFile3.pdf"
                };

                var outputFilePath = @"C:\Temp\Combined.pdf";

                var mergedPdf = PdfHelper.MergePdfDocuments(pdfFilePaths);

                File.WriteAllBytes(outputFilePath, mergedPdf);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press Return to exit");
            Console.ReadLine();
        }
    }
}
