namespace SoftwareDesignPatterns
{
    public class SingletonThreadSafe
    {
        private static SingletonThreadSafe instance = null;
        private static readonly object threadLock = new object();

        private SingletonThreadSafe()
        {
        }

        public static SingletonThreadSafe GetInstance()
        {
            if (instance is null)
            {
                lock(threadLock)
                {
                    if (instance is null) // double check in critical area
                    {
                        instance = new SingletonThreadSafe();
                    }
                }
            }

            return instance;
        }

        public override string ToString()
        {
            return $"{nameof(SingletonThreadSafe)} is a class that its implementation respects thread-safety for singleton instantiation";
        }
    }
}
