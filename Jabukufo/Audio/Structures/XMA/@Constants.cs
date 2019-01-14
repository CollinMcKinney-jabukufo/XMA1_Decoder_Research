namespace Jabukufo.Audio.Structures.XMA
{
    /// <summary>
    /// XMA Constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Size in bytes of each <see cref="PCM_SAMPLE_16b"/>.
        /// </summary>
        public const int XMA_OUTPUT_SAMPLE_BYTES = 2;

        /// <summary>
        /// Size in bits of each <see cref="PCM_SAMPLE_16b"/>.
        /// </summary>
        public const int XMA_OUTPUT_SAMPLE_BITS = XMA_OUTPUT_SAMPLE_BYTES * 8;

        /// <summary>
        /// Size in bytes of each <see cref="XMAPACKET"/>.
        /// </summary>
        public const int XMA_BYTES_PER_PACKET = 2048;

        /// <summary>
        /// Size in bits of each <see cref="XMAPACKET"/>.
        /// </summary>
        public const int XMA_BITS_PER_PACKET = XMA_BYTES_PER_PACKET * 8;

        /// <summary>
        /// Size in bytes of each <see cref="XMAPACKET"/> header.
        /// </summary>
        public const int XMA_PACKET_HEADER_BYTES = 4;

        /// <summary>
        /// Size in bits of each <see cref="XMAPACKET"/> header.
        /// </summary>
        public const int XMA_PACKET_HEADER_BITS = XMA_PACKET_HEADER_BYTES * 8;

        /// <summary>
        /// <see cref="PCM_SAMPLE_16b"/> count in each <see cref="XMAFRAME"/>.
        /// </summary>
        public const int XMA_SAMPLES_PER_FRAME = 512;

        /// <summary>
        /// <see cref="PCM_SAMPLE_16b"/> count in each <see cref="XMASUBFRAME"/>.
        /// </summary>
        public const int XMA_SAMPLES_PER_SUBFRAME = 128;

        /// <summary>
        /// <see cref="XMASUBFRAME"/> count in each <see cref="XMAFRAME"/>.
        /// </summary>
        public const int XMA_SUBFRAMES_PER_FRAME = XMA_SAMPLES_PER_FRAME / XMA_SAMPLES_PER_SUBFRAME;

        /// <summary>
        /// Maximum <see cref="XMAPACKET"/> count that can be submitted to the XMA decoder at a time.
        /// </summary>
        public const int XMA_READBUFFER_MAX_PACKETS = 4095;

        /// <summary>
        /// Maximum <see cref="byte"/> count that can be submitted to the XMA decoder at a time.
        /// </summary>
        public const int XMA_READBUFFER_MAX_BYTES = XMA_READBUFFER_MAX_PACKETS * XMA_BYTES_PER_PACKET;

        /// <summary>
        /// Maximum <see cref="byte"/> count allowed for the XMA decoder's output buffers.
        /// </summary>
        public const int XMA_WRITEBUFFER_MAX_BYTES = 31 * 256;

        /// <summary>
        /// Required <see cref="byte"/> alignment of the XMA decoder's output buffers.
        /// </summary>
        public const int XMA_WRITEBUFFER_BYTE_ALIGNMENT = 256;

        // Decode chunk sizes for the XMA_PLAYBACK_INIT.subframesToDecode field
        public const int XMA_MIN_SUBFRAMES_TO_DECODE = 1;
        public const int XMA_MAX_SUBFRAMES_TO_DECODE = 8;
        public const int XMA_OPTIMAL_SUBFRAMES_TO_DECODE = 4;

        // LoopCount<255 means finite repetitions; LoopCount=255 means infinite looping
        public const int XMA_MAX_LOOPCOUNT = 254;
        public const int XMA_INFINITE_LOOP = 255;
    }
}
