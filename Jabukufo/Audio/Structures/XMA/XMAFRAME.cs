﻿using Jabukufo.Bits;
using Jabukufo.Common;
using System;
using System.Diagnostics;

namespace Jabukufo.Audio.Structures.XMA
{
    /// <summary>
    /// https://github.com/xenia-project/libav/blob/9919df9ebe02baa4f4a8082e82d6807680977f34/libavcodec/xma2dec.c#L1495
    /// There is no way to represent the XMA frame as a C struct, since it is a
    /// variable-sized string of bits that need not be stored at a byte-aligned
    /// position in memory.This is the layout:
    /// </summary>
    public class XMAFRAME
    {
        /// <summary>
        /// Size in bits of the frame's initial LengthInBits field.
        /// </summary>
        public const short XMA_BITS_IN_FRAME_LENGTH_FIELD = 15;

        /// <summary>
        /// Special LengthInBits value that marks an invalid final frame.
        /// </summary>
        public const short XMA_FINAL_FRAME_MARKER = 0x7FFF;

        /// <summary>
        /// A 15-bit number representing the length of this frame.
        /// </summary>
        public ushort FrameLength;

        /// <summary>
        /// Encoded XMA data; its size in bits is (<see cref="FrameLength"/> - <see cref="XMA_BITS_IN_FRAME_LENGTH_FIELD"/>).
        /// </summary>
        public BitContext FrameData;

        /// <summary>
        /// Decoded <see cref="XMASUBFRAME"/>s will be contained here.
        /// This is abstract and not part of the actual <see cref="XMAFRAME"/> structure.
        /// </summary>
        public XMASUBFRAME[] SubFrames = new XMASUBFRAME[Constants.XMA_SUBFRAMES_PER_FRAME];

        public XMAFRAME(BitContext packetContext, XMAFILE xmaFile)
        {
            Debug.WriteLine(typeof(XMAFRAME).FullName);
            Debug.Indent();

            this.FrameLength = packetContext.ReadValue<ushort>(XMAFRAME.XMA_BITS_IN_FRAME_LENGTH_FIELD, Endianness.BE);
            Debug.WriteLine($"{nameof(this.FrameLength)}: {this.FrameLength}");

            if (this.FrameLength == XMA_FINAL_FRAME_MARKER)
                return;

            this.FrameData = packetContext.GetBits(this.FrameLength, false);
            Debug.Unindent();
        }
    }
}
