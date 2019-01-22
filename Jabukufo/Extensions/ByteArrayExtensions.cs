using System.Diagnostics;

namespace System
{
    public static class ByteArrayExtensions
    {
        public unsafe static T Convert<T>(this byte[] @this) where T : unmanaged
        {
            // TODO: maybe allow byte[]'s <= sizeof(T) to be converted?
            Assert.Always(@this.Length == sizeof(T));
            fixed (void* pThis = @this)
                return *(T*)pThis;
        }
    }
}