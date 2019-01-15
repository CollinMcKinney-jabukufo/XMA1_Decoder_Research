using Jabukufo.Bits;
using Jabukufo.Common;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    public class CHUNK_Riff
    {
        public static readonly FourCC Tag = new FourCC("RIFF");
        public static readonly FourCC FormatTag = new FourCC("WAVE");
        public CHUNK_HEADER Header;

        /// <summary>
        /// Contains the letters "WAVE" (0x57415645 big-endian form).
        /// </summary>
        public FourCC Format;

        public CHUNK_Riff(BitStream metaStream, XMAFILE xmaFile)
        {
            Debug.WriteLine(typeof(CHUNK_Riff).FullName);
            Debug.Indent();

            this.Header = new CHUNK_HEADER(metaStream, CHUNK_Riff.Tag);

            this.Format = metaStream.ReadValue<FourCC>(Endianness.BE);
            Debug.WriteLine($"{nameof(Format)}: {Format}");
            Assert.Debug(this.Format == CHUNK_Riff.FormatTag);

            Debug.Unindent();
        }
    }
}
