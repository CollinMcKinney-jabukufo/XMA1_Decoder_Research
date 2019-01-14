using Jabukufo.Audio.Structures.WAV;
using Jabukufo.Bits;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    /// <summary>
    /// Represents the XMA wave format.
    /// </summary>
    public class XMAWAVEFORMAT
    {
        /// <summary>
        /// Audio format type (always <see cref="CompressionCode.XMA"/>).
        /// Little-Endian
        /// </summary>
        public CompressionCode FormatTag;

        /// <summary>
        /// Bit depth, currently required to be <see cref="Constants.XMA_OUTPUT_SAMPLE_BITS"/>.
        /// Little-Endian
        /// </summary>
        public ushort BitsPerSample;

        /// <summary>
        /// Options for XMA encoder/decoder.
        /// </summary>
        public ushort EncodeOptions;

        /// <summary>
        /// Largest skip used in interleaving streams.
        /// </summary>
        public ushort LargestSkip;

        /// <summary>
        /// Number of interleaved audio streams.
        /// </summary>
        public ushort NumStreams;

        /// <summary>
        /// Number of loop repetitions, with 255 indicating infinite looping.
        /// </summary>
        public byte LoopCount;

        /// <summary>
        /// Version of the encoder that generated this data.
        /// </summary>
        public byte Version;

        /// <summary>
        /// Array of XMASTREAMFORMAT structures describing the format for each stream.
        /// This array can grow based on the value of NumStreams).
        /// </summary>
        public XMASTREAMFORMAT[] XmaStreams;

        public XMAWAVEFORMAT(BitStream xmaStream)
        {
            Debug.WriteLine(typeof(XMAWAVEFORMAT).FullName);
            Debug.Indent();

            this.FormatTag = xmaStream.ReadValue<CompressionCode>();
            Debug.WriteLine($"{nameof(FormatTag)}: {FormatTag}");
            Assert.Debug(this.FormatTag == CompressionCode.XMA);

            this.BitsPerSample = xmaStream.ReadValue<ushort>();
            Debug.WriteLine($"{nameof(BitsPerSample)}: {BitsPerSample}");
            Assert.Debug(this.BitsPerSample == Constants.XMA_OUTPUT_SAMPLE_BITS);

            this.EncodeOptions = xmaStream.ReadValue<ushort>();
            Debug.WriteLine($"{nameof(EncodeOptions)}: {EncodeOptions}");

            this.LargestSkip = xmaStream.ReadValue<ushort>();
            Debug.WriteLine($"{nameof(LargestSkip)}: {LargestSkip}");

            this.NumStreams = xmaStream.ReadValue<ushort>();
            Debug.WriteLine($"{nameof(NumStreams)}: {NumStreams}");

            this.LoopCount = xmaStream.ReadValue<byte>();
            Debug.WriteLine($"{nameof(LoopCount)}: {LoopCount}");

            this.Version = xmaStream.ReadValue<byte>();
            Debug.WriteLine($"{nameof(Version)}: {Version}");

            this.XmaStreams = new XMASTREAMFORMAT[this.NumStreams];
            Debug.WriteLine(nameof(this.XmaStreams));
            Debug.Indent();
            for (var i = 0; i < this.XmaStreams.Length; i++)
            {
                Debug.Write($"[{i.ToString().PadLeft(this.NumStreams.ToString().Length, '0')}] ");
                this.XmaStreams[i] = new XMASTREAMFORMAT(xmaStream);
            }
            Debug.Unindent();

            Debug.Unindent();
        }
    }
}
