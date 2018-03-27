using System.IO;

namespace ContactProcessor.IO
{
    public interface IStreamService
    {
        byte[] StreamToByteArray(Stream stream, int contentLenght);
    }
}