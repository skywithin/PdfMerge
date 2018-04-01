using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PdfMerge.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void PdfHelper_MergePdfFilesAndWriteToDisk_CreatesMergedPdfFile()
        {
            var sourcePdfFilePaths = new[]
            {
                "TestData/TestFile1.pdf",
                "TestData/TestFile2.pdf"
            };

            var outputFileName = "Test_MergedFile.pdf";

            PdfHelper.MergePdfFilesAndWriteToDisk(sourcePdfFilePaths, outputFileName);

            Assert.IsTrue(File.Exists(outputFileName));

            var outputFilesBytes = File.ReadAllBytes(outputFileName);

            Assert.IsNotNull(outputFilesBytes);
            Assert.IsTrue(outputFilesBytes.Length > 0);
        }
        
        [TestMethod]
        public void PdfHelper_MergePdfFiles_CreatesBytesArray()
        {
            var sourcePdfFilePaths = new [] 
            {
                "TestData/TestFile1.pdf",
                "TestData/TestFile2.pdf"
            };

            byte[] output = PdfHelper.MergePdfFiles(sourcePdfFilePaths);

            Assert.IsNotNull(output);
            Assert.IsTrue(output.Any());
        }

        [TestMethod]
        public void PdfHelper_MergePdfFiles_AllFilesDoNotExist_ReturnsNull()
        {
            var sourcePdfFilePaths = new[]
            {
                "THIS_FILE_DOES_NOT_EXIST.pdf",
                "THIS_FILE_ALSO_DOES_NOT_EXIST.pdf"
            };

            byte[] output = PdfHelper.MergePdfFiles(sourcePdfFilePaths);

            Assert.IsNull(output);
        }

        [TestMethod]
        public void PdfHelper_MergePdfFiles_SomeFilesDoNotExist_CreatesBytesArray()
        {
            var sourcePdfFilePaths = new[]
            {
                "TestData/TestFile1.pdf",
                "THIS_FILE_DOES_NOT_EXIST.pdf",
                "TestData/TestFile2.pdf"
            };

            byte[] output = PdfHelper.MergePdfFiles(sourcePdfFilePaths);

            Assert.IsNotNull(output);
            Assert.IsTrue(output.Any());
        }

        [TestMethod]
        public void PdfHelper_MergePdfFilesAndWriteToDisk_AllFilesDoNotExist_NothingCreated()
        {
            var sourcePdfFilePaths = new[]
            {
                "THIS_FILE_DOES_NOT_EXIST.pdf",
                "THIS_FILE_ALSO_DOES_NOT_EXIST.pdf"
            };

            var outputFileName = "Test_MergedFile_ShouldNotBeCreated.pdf";

            PdfHelper.MergePdfFilesAndWriteToDisk(sourcePdfFilePaths, outputFileName);

            Assert.IsFalse(File.Exists(outputFileName));
        }

    }
}
