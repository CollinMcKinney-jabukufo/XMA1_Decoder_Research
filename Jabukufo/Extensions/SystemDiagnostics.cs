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
                throw null;
            }
        }

        [Conditional("DEBUG")]
        public static void Debug(bool condition, Exception exception = null)
            => Assert.Always(condition, exception);

        [Conditional("RELEASE")]
        public static void Release(bool condition, Exception exception = null)
            => Assert.Always(condition, exception);
    }
}