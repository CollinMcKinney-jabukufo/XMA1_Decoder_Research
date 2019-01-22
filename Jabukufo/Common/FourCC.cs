using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Jabukufo.Common
{

    [StructLayout(LayoutKind.Sequential, Pack = 0x01, Size = 0x04)]
    public struct FourCC
    {
        public readonly uint Value;

        public FourCC(uint value)
        {
            this.Value = value;
        }
        public FourCC(string fourCC)
        {
            if (fourCC.Length != 4)
                throw new DataMisalignedException();

            this.Value = (uint)fourCC[0] << 24;
            this.Value |= (uint)fourCC[1] << 16;
            this.Value |= (uint)fourCC[2] << 8;
            this.Value |= (uint)fourCC[3] << 0;
        }
        public FourCC(char[] fourCC)
        {
            if (fourCC.Length != 4)
                throw new DataMisalignedException();

            this.Value = (uint)fourCC[0] << 24;
            this.Value |= (uint)fourCC[1] << 16;
            this.Value |= (uint)fourCC[2] << 8;
            this.Value |= (uint)fourCC[3] << 0;
        }
        public FourCC(byte[] fourCC)
        {
            if (fourCC.Length != 4)
                throw new DataMisalignedException();

            this.Value = (uint)fourCC[0] << 24;
            this.Value |= (uint)fourCC[1] << 16;
            this.Value |= (uint)fourCC[2] << 8;
            this.Value |= (uint)fourCC[3] << 0;
        }

        public byte[] ToBytes()
        {
            var res = new byte[4];
            res[0] = (byte)(this.Value >> 24);
            res[1] = (byte)(this.Value >> 16);
            res[2] = (byte)(this.Value >> 8);
            res[3] = (byte)(this.Value >> 0);
            return res;
        }
        public char[] ToChars()
        {
            var res = new char[4];
            res[0] = (char)(this.Value >> 24);
            res[1] = (char)(this.Value >> 16);
            res[2] = (char)(this.Value >> 8);
            res[3] = (char)(this.Value >> 0);
            return res;
        }
        public override string ToString()
        {
            var res = new byte[4];
            res[0] = (byte)(this.Value >> 24);
            res[1] = (byte)(this.Value >> 16);
            res[2] = (byte)(this.Value >> 8);
            res[3] = (byte)(this.Value >> 0);

            return Encoding.ASCII.GetString(res);
        }

        public FourCC EndianSwap()
        {
            uint temp = 0;
            temp |= (this.Value & 0x000000FFU) << 24;
            temp |= (this.Value & 0x0000FF00U) << 8;
            temp |= (this.Value & 0x00FF0000U) >> 8;
            temp |= (this.Value & 0xFF000000U) >> 24;
            return new FourCC(temp);
        }

        public static bool operator ==(FourCC a, FourCC b) => a.Value == b.Value;
        public static bool operator !=(FourCC a, FourCC b) => a.Value != b.Value;
        public override int GetHashCode() => this.Value.GetHashCode();
        public override bool Equals(object obj) => obj is FourCC fourCC ? this == fourCC : false;
    }
}