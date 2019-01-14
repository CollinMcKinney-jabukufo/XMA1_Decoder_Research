using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabukufo.Audio.Structures.WAV
{
    /// <summary>
    /// The <see cref="WAVEFORMATEX"/> structure specifies the data format of a wave audio stream.
    /// <see href="https://docs.microsoft.com/en-us/windows/desktop/api/mmreg/ns-mmreg-twaveformatex">HERE</see>
    /// </summary>
    public struct WAVEFORMATEX
    {
        /// <summary>
        /// Waveform-audio format type. Format tags are registered with Microsoft Corporation for many compression algorithms.
        /// A complete list of format tags can be found in the Mmreg.h header file. 
        /// For one- or two-channel PCM data, this value should be <see cref="CompressionCode.PCM"/>.
        /// </summary>
        public CompressionCode wFormatTag;

        /// <summary>
        /// Number of channels in the waveform-audio data. Monaural data uses one channel and stereo data uses two channels.
        /// </summary>
        public ushort nChannels;

        /// <summary>
        /// Sample rate, in samples per second (hertz). If <see cref="wFormatTag"/> is <see cref="CompressionCode.PCM"/>, 
        /// then common values for nSamplesPerSec are 8.0 kHz, 11.025 kHz, 22.05 kHz, and 44.1 kHz. 
        /// For non-PCM formats, this member must be computed according to the manufacturer's specification of the format tag.
        /// </summary>
        public uint nSamplesPerSec;

        /// <summary>
        /// Required average data-transfer rate, in bytes per second, for the format tag.
        /// If <see cref="wFormatTag"/> is <see cref="CompressionCode.PCM"/>, nAvgBytesPerSec should be equal to the
        /// product of nSamplesPerSec and nBlockAlign. 
        /// For non-PCM formats, this member must be computed according to the manufacturer's specification of the format tag.
        /// </summary>
        public uint nAvgBytesPerSec;

        /// <summary>
        /// Block alignment, in bytes. The block alignment is the minimum atomic unit of data for 
        /// the <see cref="wFormatTag"/> format type. If <see cref="wFormatTag"/> is <see cref="CompressionCode.PCM"/> or
        /// <see cref="CompressionCode.EXTENSIBLE"/>, nBlockAlign must be equal to the product of 
        /// <see cref="nChannels"/> and <see cref="wBitsPerSample"/> divided by 8 (bits per byte).
        /// For non-PCM formats, this member must be computed according to the manufacturer's specification of the format tag.
        ///
        /// Software must process a multiple of <see cref="nBlockAlign"/> bytes of data at a time.
        /// Data written to and read from a device must always start at the beginning of a block.
        /// For example, it is illegal to start playback of PCM data in the middle of a sample (that is, on a non-block-aligned boundary).
        /// </summary>
        public ushort nBlockAlign;

        /// <summary>
        /// Bits per sample for the <see cref="wFormatTag"/> format type. If <see cref="wFormatTag"/> is <see cref="CompressionCode.PCM"/>, 
        /// then <see cref="wBitsPerSample"/> should be equal to 8 or 16. 
        /// For non-PCM formats, this member must be set according to the manufacturer's specification of the format tag. 
        /// If <see cref="wFormatTag"/> is <see cref="CompressionCode.EXTENSIBLE"/>, this value can be any integer multiple of 8.
        /// Some compression schemes cannot define a value for <see cref="wBitsPerSample"/>, so this member can be zero.
        /// </summary>
        public ushort wBitsPerSample;

        /// <summary>
        /// Size, in bytes, of extra format information appended to the end of the WAVEFORMATEX structure.
        /// This information can be used by non-PCM formats to store extra attributes for the wFormatTag.
        /// If no extra information is required by the wFormatTag, this member must be set to zero.
        /// For WAVE_FORMAT_PCM formats (and only WAVE_FORMAT_PCM formats), this member is ignored.
        /// </summary>
        public ushort cbSize;
    }
}
