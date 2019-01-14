using System;

namespace Jabukufo.Audio.Structures.WAV
{
    /// <summary>
    /// Identifies the format of the sample data in the data chunk.
    /// This is a replacement for the original format code field, since it will have a format code of 65534. 
    /// <see href="https://github.com/tpn/winsdk-10/blob/master/Include/10.0.10240.0/shared/ksmedia.h">HERE</see>
    /// </summary>
    public class KSDATAFORMAT_SUBTYPE
    {
        private readonly Guid Value;
        public byte[] Bytes => this.Value.ToByteArray();
        public KSDATAFORMAT_SUBTYPE(Guid guid) => this.Value = guid;
        public static implicit operator KSDATAFORMAT_SUBTYPE(Guid guid) => new KSDATAFORMAT_SUBTYPE(guid);

        #region Major Format Code GUID's
        public static Guid WAVEFORMATEX => new Guid("00000000-0000-0010-8000-00aa00389b71");
        public static Guid ANALOG       => new Guid("6dba3190-67bd-11cf-a0f7-0020afd156e4");
        public static Guid PCM          => new Guid("00000001-0000-0010-8000-00aa00389b71");
        public static Guid IEEE_FLOAT   => new Guid("00000003-0000-0010-8000-00aa00389b71");
        public static Guid DRM          => new Guid("00000009-0000-0010-8000-00aa00389b71");
        public static Guid ALAW         => new Guid("00000006-0000-0010-8000-00aa00389b71");
        public static Guid MULAW        => new Guid("00000007-0000-0010-8000-00aa00389b71");
        public static Guid ADPCM        => new Guid("00000002-0000-0010-8000-00aa00389b71");
        public static Guid MPEG         => new Guid("00000050-0000-0010-8000-00aa00389b71");
        public static Guid RIFF         => new Guid("4995DAEE-9EE6-11D0-A40E-00A0C9223196");
        public static Guid RIFFWAVE     => new Guid("e436eb8b-524f-11ce-9f53-0020af0ba770");
        #endregion

        #region MIDI Format Guid's
        public static Guid MIDI     => new Guid("1D262760-E957-11CF-A5D6-28DB04C10000");
        public static Guid MIDI_BUS => new Guid("2CA15FA0-6CFE-11CF-A5D6-28DB04C10000");
        public static Guid RIFFMIDI => new Guid("4995DAF0-9EE6-11D0-A40E-00A0C9223196");
        #endregion

        #region New formats enabled by CEA 861 specifciation
        public static Guid IEC61937_DOLBY_DIGITAL       => new Guid("00000092-0000-0010-8000-00aa00389b71");
        public static Guid IEC61937_WMA_PRO             => new Guid("00000164-0000-0010-8000-00aa00389b71");
        public static Guid IEC61937_DTS                 => new Guid("00000008-0000-0010-8000-00aa00389b71");
        public static Guid IEC61937_MPEG1               => new Guid("00000003-0cea-0010-8000-00aa00389b71");
        public static Guid IEC61937_MPEG2               => new Guid("00000004-0cea-0010-8000-00aa00389b71");
        public static Guid IEC61937_MPEG3               => new Guid("00000005-0cea-0010-8000-00aa00389b71");
        public static Guid IEC61937_AAC                 => new Guid("00000006-0cea-0010-8000-00aa00389b71");
        public static Guid IEC61937_ATRAC               => new Guid("00000008-0cea-0010-8000-00aa00389b71");
        public static Guid IEC61937_ONE_BIT_AUDIO       => new Guid("00000009-0cea-0010-8000-00aa00389b71");
        public static Guid IEC61937_DOLBY_DIGITAL_PLUS  => new Guid("0000000a-0cea-0010-8000-00aa00389b71");
        public static Guid IEC61937_DTS_HD              => new Guid("0000000b-0cea-0010-8000-00aa00389b71");
        public static Guid IEC61937_DOLBY_MLP           => new Guid("0000000c-0cea-0010-8000-00aa00389b71");
        public static Guid IEC61937_DST                 => new Guid("0000000d-0cea-0010-8000-00aa00389b71");
        #endregion

        public static Guid MPEGLAYER3       => new Guid("00000055-0000-0010-8000-00aa00389b71"); // MP3
        public static Guid MPEG_HEAAC       => new Guid("00001610-0000-0010-8000-00aa00389b71"); // AAC
        public static Guid WMAUDIO2         => new Guid("00000161-0000-0010-8000-00aa00389b71"); // WMA STD
        public static Guid WMAUDIO3         => new Guid("00000162-0000-0010-8000-00aa00389b71"); // WMA PRO
        public static Guid WMAUDIO_LOSSLESS => new Guid("00000163-0000-0010-8000-00aa00389b71"); // WMA LOSSLESS
    }
}
 