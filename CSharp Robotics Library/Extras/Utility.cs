using System;
using System.Diagnostics;

namespace CSharpRoboticsLib.Extras
{
    /// <summary>
    /// Provides various mathematical and logical utility functions
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="degrees">angle in degrees</param>
        /// <returns>angle in radians</returns>
        public static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        /// <summary>
        /// Converts radians to degrees
        /// </summary>
        /// <param name="radians">angle in radians</param>
        /// <returns>angle in degrees</returns>
        public static double ToDegrees(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        /// <summary>
        /// Limits the value to a low and high threshold
        /// </summary>
        /// <param name="value">value to be limited</param>
        /// <param name="low">lower limit</param>
        /// <param name="high">upper limit</param>
        /// <returns>value coerced to the nearest limit</returns>
        public static double Limit(double value, double low, double high)
        {
            if (value < low)
                return low;
            if (value > high)
                return high;
            return value;
        }

        /// <summary>
        /// Waits for a specified time more accurately than Thread.Sleep()
        /// </summary>
        /// <param name="time">Time in seconds to wait</param>
        public static void AccurateWaitSeconds(double time)
        {
            Stopwatch timer = new Stopwatch();
            timer.Restart();
            while (timer.Elapsed.TotalSeconds < time)
                ;
        }

        /// <summary>
        /// Waits for a specified time more accurately than Thread.Sleep()
        /// </summary>
        /// <param name="time">Time in milliseconds to wait</param>
        public static void AccurateWaitMilliseconds(double time)
        {
            Stopwatch timer = new Stopwatch();
            timer.Restart();
            while (timer.Elapsed.TotalMilliseconds < time)
                ;
        }
    }
}
