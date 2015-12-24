using System;
using System.Threading;

namespace CSharpRoboticsLib
{
    /// <summary>
    /// Class providing extension methods for functions and actions allowing calls with a specified millisecond timeout.
    /// </summary>
    public static class TimeoutInvokeExtensions
    {
        //----------
        //   Func
        //----------

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

        public static bool TryExecute<T1, TOut>(this Func<T1, TOut> f, T1 arg1, int timeoutMs, out TOut result)
        {
            TOut t = default(TOut);
            Thread thread = new Thread(() => t = f(arg1));
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            result = t;
            return completed;
        }

        public static TOut TryExecute<T1, TOut>(this Func<T1, TOut> f, T1 arg1, int timeoutMs)
        {
            TOut result;
            f.TryExecute(arg1, timeoutMs, out result);
            return result;
        }

        public static bool TryExecute<T1, T2, TOut>(this Func<T1, T2, TOut> f, T1 arg1, T2 arg2, int timeoutMs, out TOut result)
        {
            TOut t = default(TOut);
            Thread thread = new Thread(() => t = f(arg1, arg2));
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            result = t;
            return completed;
        }

        public static TOut TryExecute<T1, T2, TOut>(this Func<T1, T2, TOut> f, T1 arg1, T2 arg2, int timeoutMs)
        {
            TOut result;
            f.TryExecute(arg1, arg2, timeoutMs, out result);
            return result;
        }

        public static bool TryExecute<T1, T2, T3, TOut>(this Func<T1, T2, T3, TOut> f, T1 arg1, T2 arg2, T3 arg3, int timeoutMs, out TOut result)
        {
            TOut t = default(TOut);
            Thread thread = new Thread(() => t = f(arg1, arg2, arg3));
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            result = t;
            return completed;
        }

        public static TOut TryExecute<T1, T2, T3, TOut>(this Func<T1, T2, T3, TOut> f, T1 arg1, T2 arg2, T3 arg3, int timeoutMs)
        {
            TOut result;
            f.TryExecute(arg1, arg2, arg3, timeoutMs, out result);
            return result;
        }

        public static bool TryExecute<T1, T2, T3, T4, TOut>(this Func<T1, T2, T3, T4, TOut> f, T1 arg1, T2 arg2, T3 arg3, T4 arg4, int timeoutMs, out TOut result)
        {
            TOut t = default(TOut);
            Thread thread = new Thread(() => t = f(arg1, arg2, arg3, arg4));
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            result = t;
            return completed;
        }

        public static TOut TryExecute<T1, T2, T3, T4, TOut>(this Func<T1, T2, T3, T4, TOut> f, T1 arg1, T2 arg2, T3 arg3, T4 arg4, int timeoutMs)
        {
            TOut result;
            f.TryExecute(arg1, arg2, arg3, arg4, timeoutMs, out result);
            return result;
        }

        //----------
        //  Action
        //----------

        public static bool TryExecute(this Action a, int timeoutMs)
        {
            Thread thread = new Thread(() => a());
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            return completed;
        }

        public static bool TryExecute<T>(this Action<T> a, T arg, int timeoutMs)
        {
            Thread thread = new Thread(() => a(arg));
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            return completed;
        }

        public static bool TryExecute<T1, T2>(this Action<T1, T2> a, T1 arg1, T2 arg2, int timeoutMs)
        {
            Thread thread = new Thread(() => a(arg1, arg2));
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            return completed;
        }

        public static bool TryExecute<T1, T2, T3>(this Action<T1, T2, T3> a, T1 arg1, T2 arg2, T3 arg3, int timeoutMs)
        {
            Thread thread = new Thread(() => a(arg1, arg2, arg3));
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            return completed;
        }

        public static bool TryExecute<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> a, T1 arg1, T2 arg2, T3 arg3, T4 arg4, int timeoutMs)
        {
            Thread thread = new Thread(() => a(arg1, arg2, arg3, arg4));
            thread.Start();

            bool completed = thread.Join(timeoutMs);
            if (!completed)
                thread.Abort();

            return completed;
        }
    }
}
