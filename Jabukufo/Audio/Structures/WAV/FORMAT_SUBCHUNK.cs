using Jabukufo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabukufo.Audio.Structures.WAV
{
    class FORMAT_SUBCHUNK
    {
        /// <summary>
        /// Contains the letters "fmt " (0x666d7420 big-endian form).
        /// </summary>
        public FourCC SubChunk1ID;

        /// <summary>
        /// 16 for PCM. This is the size of the rest of the Subchunk which follows this number.
        /// </summary>
        public uint SubChunk1Size;

        /// <summary>
        /// 1(i.e.Linear quantization) Values other than 1 indicate some form of compression.
        /// </summary>
        public ushort AudioFormat;

        /// <summary>
        /// Mono = 1, Stereo = 2, etc.
        /// </summary>
        public ushort NumChannels;

        /// <summary>
        /// 8000, 44100, etc.
        /// </summary>
        public uint SampleRate;

        /// <summary>
        /// == <see cref="SampleRate"/> * <see cref="NumChannels"/> * <see cref="BitsPerSample"/> / 8
        /// </summary>
        public uint ByteRate;

        /// <summary>
        /// == <see cref="NumChannels"/> * <see cref="BitsPerSample"/> / 8
        /// The number of bytes for one sample including all channels.
        /// </summary>
        public ushort BlockAlign;

        /// <summary>
        /// 8 bits = 8, 16 bits = 16, etc.
        /// </summary>
        public ushort BitsPerSample;

        public WAVEFORMATEXTENSIBLE Extension;
    }
}
