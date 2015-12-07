using System;
using System.Threading;

namespace CSharpRoboticsLib.Autonomous
{
    public static class TimeoutInvokeExtensions
    {
        public static bool TryExecute<T>(this Func<T> f, int timeoutMs, out T result)
        {
            T t = default(T);
            Thread thread = new Thread(() => t = f());
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            result = t;
            return completed;
        }

        public static T TryExecute<T>(this Func<T> f, int timeoutMs)
        {
            T result;
            f.TryExecute(timeoutMs, out result);
            return result;
        }

        public static bool TryExecute(this Action a, int timeoutMs)
        {
            Thread thread = new Thread(() => a());
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            return completed;
        }
    }
}
