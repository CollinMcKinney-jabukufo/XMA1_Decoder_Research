using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabukufo.Audio.Structures.WAV
{
    /// <summary>
    /// The <see cref="WAVEFORMATEXTENSIBLE"/> structure defines the format of waveform-audio data for formats having 
    /// more than two channels or higher sample resolutions than allowed by <see cref="WAVEFORMATEX"/>. 
    /// It can also be used to define any format that can be defined by <see cref="WAVEFORMATEX"/>.
    /// <see href="https://docs.microsoft.com/en-us/windows/desktop/api/mmreg/ns-mmreg-__unnamed_struct_0">HERE</see>
    /// </summary>
    public struct WAVEFORMATEXTENSIBLE
    {
        /// <summary>
        /// <see cref="WAVEFORMATEX"/> structure that specifies the basic format. 
        /// The <see cref="WAVEFORMATEX.wFormatTag"/> member must be <see cref="CompressionCode.EXTENSIBLE"/>.
        /// The cbSize member must be at least 22.
        /// </summary>
        public WAVEFORMATEX Format;

        #region SampleUnion
        /// <summary>
        /// Reserved for internal use by operating system. Set to 0.
        /// </summary>
        public ushort wReserved;

        /// <summary>
        /// Number of bits of precision in the signal. Usually equal to <see cref="WAVEFORMATEX.wBitsPerSample"/>.
        /// However, <see cref="WAVEFORMATEX.wBitsPerSample"/> is the container size and must be a multiple of 8, whereas 
        /// <see cref="wValidBitsPerSample"/> can be any value not exceeding the container size. 
        /// For example, if the format uses 20-bit samples, <see cref="WAVEFORMATEX.wBitsPerSample"/> must be at least 24, 
        /// but <see cref="wValidBitsPerSample"/> is 20.
        /// </summary>
        public ushort wValidBitsPerSample { get => wReserved; set => wReserved = value; }

        /// <summary>
        /// Number of samples contained in one compressed block of audio data. This value is used in buffer estimation.
        /// This value is used with compressed formats that have a fixed number of samples within each block.
        /// This value can be set to 0 if a variable number of samples is contained in each block of compressed audio data.
        /// In this case, buffer estimation and position information needs to be obtained in other ways.
        /// </summary>
        public ushort wSamplesPerBlock { get => wReserved; set => wReserved = value; }
        #endregion

        /// <summary>
        /// Bitmask specifying the assignment of channels in the stream to speaker positions.
        /// </summary>
        public ChannelMask dwChannelMask;

        /// <summary>
        /// Subformat of the data, such as <see cref="KSDATAFORMAT_SUBTYPE.PCM"/>.
        /// The subformat information is similar to that provided by the tag in the <see cref="WAVEFORMATEX.wFormatTag"/>.
        /// </summary>
        public KSDATAFORMAT_SUBTYPE SubFormat;
    }
}
