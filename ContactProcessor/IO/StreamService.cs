using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ContactProcessor.IO
{
    public class StreamService : IStreamService
    {
        public byte[] StreamToByteArray(Stream stream, int contentLenght)
        {
            using (var binaryReader = new BinaryReader(stream))
            {
                return binaryReader.ReadBytes(contentLenght);
            }
        }
    }
}