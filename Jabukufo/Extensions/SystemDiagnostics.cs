namespace System.Diagnostics
{
    public static class Assert
    {
        public static void Always(bool condition, Exception exception = default)
        {
            if (!condition)
            {
                if (exception is null)
                    exception = new Exception();
                throw exception;
            }
        }

#if DEBUG
        public static void Debug(bool condition, Exception exception = null)
#else
        public static void Release(bool condition, Exception exception = null)
#endif
            => Assert.Always(condition, exception);
    }
}