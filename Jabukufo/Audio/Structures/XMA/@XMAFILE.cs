using Jabukufo.Bits;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    public unsafe class XMAFILE
    {
        public CHUNK_Riff RiffSubchunk;
        public CHUNK_Format FormatSubchunk;
        public CHUNK_Data DataSubchunk;
        public CHUNK_Seek SeekSubchunk;

        public XMAFILE(byte[] xmaFileData)
        {
            var metaStream = new BitStream(xmaFileData);

            this.RiffSubchunk   = new CHUNK_Riff(metaStream, this);
            this.FormatSubchunk = new CHUNK_Format(metaStream, this);
            this.DataSubchunk   = new CHUNK_Data(metaStream, this);
            this.SeekSubchunk   = new CHUNK_Seek(metaStream, this);

            return;
        }
    }
}
