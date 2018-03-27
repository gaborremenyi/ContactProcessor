namespace ContactProcessor.IO
{
    public interface IFileService
    {
        string ReadFileContent(string folderPath, string fileName);

        void WriteFile(string folderPath, string fileName, byte[] content);

        void AppendToFile(string folderPath, string fileName, string content);
    }
}