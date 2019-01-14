using Jabukufo.Bits;
using Jabukufo.Common;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    /// <summary>
    /// 'fmt ' chunk: A description of the XMA data's structure
    /// and characteristics (length, channels, sample rate, loops, block size, etc).
    /// </summary>
    public class CHUNK_Format
    {
        public static readonly FourCC Tag = new FourCC("fmt ");
        public CHUNK_HEADER Header;

        public XMAWAVEFORMAT XMAWAVEFormat;

        public CHUNK_Format(BitStream xmaStream, XMAFILE xmaFile)
        {
            Debug.WriteLine(typeof(CHUNK_Format).FullName);
            Debug.Indent();

            this.Header = new CHUNK_HEADER(xmaStream, CHUNK_Format.Tag);

            this.XMAWAVEFormat = new XMAWAVEFORMAT(xmaStream);

            Debug.Unindent();
        }
    }
}
