using Jabukufo.Bits;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    /// <summary>
    /// Represents the XMA stream format.
    /// </summary>
    public class XMASTREAMFORMAT
    {
        /// <summary>
        /// Used by the XMA encoder.
        /// </summary>
        public uint PseudoBytesPerSec;

        /// <summary>
        /// The stream's decoded sample rate.
        /// </summary>
        public uint SampleRate;

        /// <summary>
        /// Bit offset of the frame containing the loop start point, relative to the beginning of the stream.
        /// </summary>
        public uint LoopStart;

        /// <summary>
        /// Bit offset of the frame containing the loop end.
        /// </summary>
        public uint LoopEnd;

        /// <summary>
        /// Two 4-bit numbers specifying the exact location of
        /// the loop points within the frames that contain them.
        ///   SubframeEnd: Subframe of the loop end frame where
        ///                the loop ends.  Ranges from 0 to 3.
        ///   SubframeSkip: Subframes to skip in the start frame to
        ///                 reach the loop.  Ranges from 0 to 4.
        /// </summary>
        public byte SubframeData;

        /// <summary>
        /// Read <see cref="XMASTREAMFORMAT.SubframeData"/> summary.
        /// </summary>
        public int SubframeEnd => SubframeData >> 4;

        /// <summary>
        /// Read <see cref="XMASTREAMFORMAT.SubframeData"/> summary.
        /// </summary>
        public int SubframeSkip => SubframeData & 0x0F;

        /// <summary>
        /// Number of channels in the stream (1 or 2).
        /// </summary>
        public byte ChannelCount;

        /// <summary>
        /// Channel assignments for the channels in the stream.
        /// *   If the XMA stream is stereo, the lower 8 bits and upper 8 bits
        ///     of ChannelMask are the same as the lower 8 bits of the dwChannelMask 
        ///     member of the WAVEFORMATEXTENSIBLE structure.
        ///     
        /// *   If the XMA stream is mono, only the lower 8 bits are the same as the 
        ///     lower 8 bits of the dwChannelMask member of WAVEFORMATEXTENSIBLE.
        /// </summary>
        public XMACHANNELMASK ChannelMask;

        public XMASTREAMFORMAT(BitContext metaContext)
        {
            Debug.WriteLine(typeof(XMASTREAMFORMAT).FullName);
            Debug.Indent();

            this.PseudoBytesPerSec = metaContext.ReadValue<uint>();
            Debug.WriteLine($"{nameof(this.PseudoBytesPerSec)}: {this.PseudoBytesPerSec}");

            this.SampleRate = metaContext.ReadValue<uint>();
            Debug.WriteLine($"{nameof(this.SampleRate)}: {this.SampleRate}");

            this.LoopStart = metaContext.ReadValue<uint>();
            Debug.WriteLine($"{nameof(this.LoopStart)}: {this.LoopStart}");

            this.LoopEnd = metaContext.ReadValue<uint>();
            Debug.WriteLine($"{nameof(this.LoopEnd)}: {this.LoopEnd}");


            this.SubframeData = metaContext.ReadValue<byte>();
            Debug.WriteLine($"{nameof(this.SubframeData)}: {this.SubframeData}");
            Debug.WriteLine($"{nameof(this.SubframeEnd)}: {this.SubframeEnd}");
            Debug.WriteLine($"{nameof(this.SubframeSkip)}: {this.SubframeSkip}");
            Assert.Debug(this.SubframeEnd >= 0 && this.SubframeEnd <= 3);
            Assert.Debug(this.SubframeSkip >= 0 && this.SubframeSkip <= 4);


            this.ChannelCount = metaContext.ReadValue<byte>();
            Debug.WriteLine($"{typeof(XMASTREAMFORMAT).FullName}.{nameof(this.ChannelCount)}: {this.ChannelCount}");
            Assert.Debug((this.ChannelCount == 1) || (this.ChannelCount == 2));

            this.ChannelMask = metaContext.ReadValue<XMACHANNELMASK>();
            Debug.WriteLine($"{nameof(this.ChannelMask)}.{nameof(this.ChannelMask.Lower)}: {this.ChannelMask.Lower}");
            Debug.WriteLine($"{nameof(this.ChannelMask)}.{nameof(this.ChannelMask.Upper)}: {this.ChannelMask.Upper}");

            Debug.Unindent();
        }
    }
}
