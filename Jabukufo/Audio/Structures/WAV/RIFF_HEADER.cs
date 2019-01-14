using Jabukufo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabukufo.Audio.Structures.WAV
{
    public class RIFF_HEADER
    {
        /// <summary>
        /// Contains the letters "RIFF" in ASCII form (0x52494646 big-endian form).
        /// </summary>
        public FourCC ChunkID = new FourCC("RIFF");

        /// <summary>
        /// 36 + SubChunk2Size, or more precisely:
        /// 4 + (8 + SubChunk1Size) + (8 + SubChunk2Size)
        /// This is the size of the rest of the chunk
        /// following this number. This is the size of the
        /// entire file in bytes minus 8 bytes for the
        /// two fields not included in this count:
        /// ChunkID and ChunkSize.
        /// </summary>
        public uint ChunkSize;

        /// <summary>
        /// Contains the letters "WAVE" (0x57415645 big-endian form).
        /// </summary>
        public FourCC Format = new FourCC("WAVE");
    }
}
