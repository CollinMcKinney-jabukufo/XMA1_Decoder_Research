// http://soundfile.sapp.org/doc/WaveFormat/
// http://wavefilegem.com/how_wave_files_work.html

using System;
using System.Runtime.InteropServices;
using Jabukufo.Audio.Structures.WAV;

namespace TEMP_WAVE 
{
    public class WAVEFile
    {
        /// <summary>
        /// The canonical WAVE format starts with the RIFF header.
        /// </summary>
        public RIFF_Header ChunkDescriptor;

        /// <summary>
        /// The "fmt" sub-chunk.
        /// </summary>
        public FormatSubChunk Format;

        /// <summary>
        /// The "data" sub-chunk.
        /// </summary>
        public DataSubChunk Data;
    }

    /// <summary>
    /// The Format of concern here is "WAVE", which requires two sub-chunks: "fmt" (<see cref="FormatSubChunk"/>) and "data" (<see cref="DataSubChunk"/>)
    /// </summary>
    public class RIFF_Header
    {
        /// <summary>
        /// Contains the letters "RIFF" in ASCII form (0x52494646 big-endian form).
        /// </summary>
        public uint ChunkID;

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
        public uint Format;
    }

    /// <summary>
    /// Describes the format of the sound information in the <see cref="DataSubChunk"/>.
    /// </summary>
    public class FormatSubChunk
    {
        /// <summary>
        /// Contains the letters "fmt " (0x666d7420 big-endian form).
        /// </summary>
        public uint SubChunk1ID;

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

        public WAVE_FORMAT_EXTENSIBLE Extension;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0x01, Size = 0x12)]
    public struct WAVEFORMATEX
    {
        public readonly ushort FormatTag;
        public readonly ushort Channels;
        public readonly uint SamplesPerSec;
        public readonly uint AvgBytesPerSec;
        public readonly ushort BlockAlign;
        public readonly ushort BitsPerSample;
        public readonly ushort bSize;
    }

    public struct WAVEFORMATEXTENSIBLE
    {
        public readonly WAVEFORMATEX Format; 
        public readonly SAMPLEUNION Samples;
        public readonly ChannelMask ChannelMask;
        public readonly KSDATAFORMAT_SUBTYPE SubFormat;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 0x01, Size = 0x00)]
    public struct SAMPLEUNION
    {
        [FieldOffset(0)] public readonly short ValidBitsPerSample;
        [FieldOffset(0)] public readonly short SamplesPerBlock;
        [FieldOffset(0)] public readonly short Reserved;
    }

    public class WAVE_FORMAT_EXTENSIBLE
    {
        public readonly ushort ExtensionSize = 22;   //2     16-bit unsigned integer(value 22)

        /// <summary>
        /// Allows storing samples with bit-depths that are not a byte multiple in size. 
        /// For example, to store 12-bit samples, this value can be set to 12, and the 
        /// bits-per-sample field in the format chunk set to 16. Each sample will still 
        /// take up 16 bytes on disk, but the reader can be informed that only the lower 
        /// 12 bits should be used.
        /// </summary>
        public ushort ValidBitsPerSample;

        /// <summary>
        /// Indicates which audio channels map to which speakers.
        /// </summary>
        public ChannelMask ChannelMask;

        /// <summary>
        /// Identifies the format of the sample data in the data chunk.
        /// This is a replacement for the original format code field, since it will have a format code of 65534.
        /// </summary>
        public KSDATAFORMAT_SUBTYPE SubFormat;
    }

    /// <summary>
    /// Indicates the size of the sound information and contains the raw sound data.
    /// </summary>
    public class DataSubChunk
    {
        /// <summary>
        /// Contains the letters "data" (0x64617461 big-endian form).
        /// </summary>
        public uint SubChunk2ID;

        /// <summary>
        /// == (<see cref="NumSamples"/> * <see cref="FormatSubChunk.NumChannels"/> * <see cref="FormatSubChunk.BitsPerSample"/> / 8).
        /// This is the number of bytes in the data. You can also think of this as the size of the read of the subchunk following this number.
        /// </summary>
        public uint SubChunk2Size;

        /// <summary>
        /// The actual sound data.
        /// </summary>
        public byte[] Data;
    }
}
