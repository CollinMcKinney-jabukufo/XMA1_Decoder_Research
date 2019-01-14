namespace Jabukufo.Audio.Structures.XMA
{
    public struct XMACHANNELMASK
    {
        // TODO: Not sure which of these should actually come first, though it doesn't really matter a 
        // whole lot for the time being.
        public FLAGS Lower;
        public FLAGS Upper;

        /// <summary>
        /// Indicates which audio channels map to which speakers. Similar to  (<see cref="WAV.ChannelMask"/>), 
        /// but modified to fit in a single byte.
        /// </summary>
        public enum FLAGS : byte
        {
            /// <summary>
            /// Corresponds to '<see cref="WAV.ChannelMask.SPEAKER_FRONT_LEFT"/>'
            /// </summary>
            XMA_SPEAKER_LEFT = (1 << 00),

            /// <summary>
            /// Corresponds to '<see cref="WAV.ChannelMask.SPEAKER_FRONT_RIGHT"/>'
            /// </summary>
            XMA_SPEAKER_RIGHT = (1 << 01),

            /// <summary>
            /// Corresponds to '<see cref="WAV.ChannelMask.SPEAKER_FRONT_CENTER"/>'
            /// </summary>
            XMA_SPEAKER_CENTER = (1 << 02),

            /// <summary>
            /// Corresponds to '<see cref="WAV.ChannelMask.SPEAKER_LOW_FREQUENCY"/>'
            /// </summary>
            XMA_SPEAKER_LFE = (1 << 03),

            /// <summary>
            /// Corresponds to '<see cref="WAV.ChannelMask.SPEAKER_SIDE_LEFT"/>'
            /// </summary>
            XMA_SPEAKER_LEFT_SURROUND = (1 << 04),

            /// <summary>
            /// Corresponds to '<see cref="WAV.ChannelMask.SPEAKER_SIDE_RIGHT"/>'
            /// </summary>
            XMA_SPEAKER_RIGHT_SURROUND = (1 << 05),

            /// <summary>
            /// Corresponds to '<see cref="WAV.ChannelMask.SPEAKER_BACK_LEFT"/>'
            /// </summary>
            XMA_SPEAKER_LEFT_BACK = (1 << 06),

            /// <summary>
            /// Corresponds to '<see cref="WAV.ChannelMask.SPEAKER_BACK_RIGHT"/>'
            /// </summary>
            XMA_SPEAKER_RIGHT_BACK = (1 << 07),
        }
    }
}
