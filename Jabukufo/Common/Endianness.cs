using System;

namespace Jabukufo.Common
{
    /// <summary>
    /// Describes the "endianness" of bits/bytes.
    /// </summary>
    [Flags] public enum Endianness : byte
    {
        /// <summary>
        /// Big-Endian byte-ordering.
        /// Consider using <see cref="LE_LSB"/>, <see cref="LE_MSB"/>, <see cref="BE_LSB"/>, or <see cref="BE_MSB"/>.
        /// </summary>
        BE = 1 << 0,

        /// <summary>
        /// Little-Endian byte-ordering.
        /// Consider using <see cref="LE_LSB"/>, <see cref="LE_MSB"/>, <see cref="BE_LSB"/>, or <see cref="BE_MSB"/>.
        /// </summary>
        LE = 1 << 1,

        /// <summary>
        /// Most-Significant-Bit bit-ordering.
        /// Consider using <see cref="LE_LSB"/>, <see cref="LE_MSB"/>, <see cref="BE_LSB"/>, or <see cref="BE_MSB"/>.
        /// </summary>
        MSB = 1 << 2,

        /// <summary>
        /// Least-Significant-Bit bit-ordering.
        /// Consider using <see cref="LE_LSB"/>, <see cref="LE_MSB"/>, <see cref="BE_LSB"/>, or <see cref="BE_MSB"/>.
        /// </summary>
        LSB = 1 << 3,
        
        /// <summary>
        /// Little-Endian, Least-Significant-Bit
        /// </summary>
        LE_LSB = LE | LSB,

        /// <summary>
        /// Little-Endian, Most-Significant-Bit
        /// </summary>
        LE_MSB = LE | MSB,

        /// <summary>
        /// Big-Endian, Least-Significant-Bit
        /// </summary>
        BE_LSB = BE | LSB,

        /// <summary>
        /// Big-Endian, Most-Significant-Bit
        /// </summary>
        BE_MSB = BE | MSB,
    }
}
