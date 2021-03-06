﻿using System;
using System.Diagnostics;

namespace CSharpRoboticsLib.FlowControl
{
    /// <summary>
    /// Will return true after an amount of time has passed
    /// </summary>
    public class WaitByTime
    {
        private Stopwatch m_timer;

        /// <summary>
        /// Time until the timer is finished in seconds. Does not reset the timer.
        /// </summary>
        public double WaitTimeSeconds
        {
            get { return WaitTimeMilliseconds / 1000.0; }
            set { WaitTimeMilliseconds = (int)(value * 1000); }
        }

        /// <summary>
        /// Time until the timer is finished in milliseconds. Does not reset the timer.
        /// </summary>
        public int WaitTimeMilliseconds { get; set; }
        

        /// <summary>
        /// constructs a new instance of WaitByTime
        /// </summary>
        /// <param name="threshold">Wait time in milliseconds</param>
        public WaitByTime(int milliseconds)
        {
            WaitTimeMilliseconds = milliseconds;
            Reset();
        }

        /// <summary>
        /// constructs a new instance of WaitByTime
        /// </summary>
        /// <param name="seconds">Wait time in seconds</param>
        public WaitByTime(double seconds)
        {
            WaitTimeSeconds = seconds;
            Reset();
        }

        /// <summary>
        /// Reset the timer to the time span specified by WaitTimeMilliseconds
        /// </summary>
        public void Reset()
        {
            if (null == m_timer)
                m_timer = new Stopwatch();

            m_timer.Restart();
        }
        
        /// <summary>
        /// Returns true if enough time has passed since last reset
        /// </summary>
        public bool DoneWaiting => m_timer.ElapsedMilliseconds > WaitTimeMilliseconds;
    }
}
