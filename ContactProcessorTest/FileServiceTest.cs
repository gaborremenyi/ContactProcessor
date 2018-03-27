using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ContactProcessor.IO;
using Microsoft.Practices.Unity;
using System.Reflection;

namespace ContactProcessorTest
{
    /// <summary>
    /// Summary description for FileServiceTest
    /// </summary>
    [TestClass]
    public class FileServiceTest
    {
        const string READ_FILE_NAME = "read.txt";
        const string WRITE_FILE_NAME = "write.txt";
        const string FILE_CONTENT = "Test";

        IFileService fileService;
        string folderPath, readFilePath, writeFilePath;

        public FileServiceTest()
        {
            fileService = new FileService();
            folderPath = AppDomain.CurrentDomain.BaseDirectory;
            readFilePath = Path.Combine(folderPath, READ_FILE_NAME);
            writeFilePath = Path.Combine(folderPath, WRITE_FILE_NAME);
        }

        [TestInitialize()]
        public void FileServiceInitialize()
        {
            File.WriteAllText(readFilePath, FILE_CONTENT);
        }

        [TestCleanup()]
        public void FileServiceCleanup()
        {
            File.Delete(readFilePath);
            File.Delete(writeFilePath);
        }

        [TestMethod]
        public void ReadFileContentTest()
        {
            string content = fileService.ReadFileContent(folderPath, READ_FILE_NAME);
            Assert.AreEqual(FILE_CONTENT, content);
        }

        [TestMethod]
        public void WriteFileTest()
        {
            byte[] file = File.ReadAllBytes(readFilePath);
            fileService.WriteFile(folderPath, WRITE_FILE_NAME, file);
            Assert.IsTrue(File.Exists(writeFilePath));
        }
    }
}
