using System;

namespace CSharpRoboticsLib.FlowControl
{
    public class WaitByTime
    {
        private DateTime m_doneWaiting;

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
            m_doneWaiting = DateTime.Now.AddMilliseconds(WaitTimeMilliseconds);
        }
        
        /// <summary>
        /// Returns true if enough time has passed since last reset
        /// </summary>
        public bool DoneWaiting => DateTime.Now > m_doneWaiting;
    }
}
