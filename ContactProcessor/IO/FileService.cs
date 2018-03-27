using System.IO;

namespace ContactProcessor.IO
{
    public class FileService : IFileService
    {
        public string ReadFileContent(string folderPath, string fileName)
        {
            string filePath = Path.Combine(folderPath, fileName);
            return File.ReadAllText(filePath);
        }

        public void WriteFile(string folderPath, string fileName, byte[] content)
        {
            string filePath = Path.Combine(folderPath, fileName);
            File.WriteAllBytes(filePath, content);
        }

        public void AppendToFile(string folderPath, string fileName, string content)
        {
            string filePath = Path.Combine(folderPath, fileName);
            File.AppendAllText(filePath, content);
        }
    }
}