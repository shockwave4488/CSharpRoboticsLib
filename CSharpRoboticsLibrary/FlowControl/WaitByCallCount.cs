using System;

namespace CSharpRoboticsLib.FlowControl
{
    /// <summary>
    /// Will return true after a threshold has been met by waiting for a number of call counts
    /// </summary>
    public class WaitByCallCount
    {
        private int m_count;

        /// <summary>
        /// Call Count limit - after this, <see cref="WaitComplete"/> will return true
        /// </summary>
        public int Threshold { get; set; }

        /// <summary>
        /// New instance of WaitByCallCount
        /// </summary>
        /// <param name="count">the threshold after which it should return true</param>
        public WaitByCallCount(int count)
        {
            if (count < 1)
                throw new ArgumentException($"Call Count to wait ({count}) less than one");
            Threshold = count;
            m_count = 0;
        }

        /// <summary>
        /// returns if the update function has been called enough times
        /// </summary>
        public bool WaitComplete
        {
            get { return m_count >= Threshold; }
        }

        /// <summary>
        /// Increases the internal count by 1 if true, resets count to 0 if false
        /// </summary>
        public void Update(bool increase)
        {
            m_count = increase ? m_count + 1 : 0;
        }

        /// <summary>
        /// Increases the internal count by 1
        /// </summary>
        public void Update()
        {
            m_count++;
        }
        
        /// <summary>
        /// Increases the internal count by the amount specified
        /// </summary>
        /// <param name="count">amount to increase by</param>
        public void Update(int count)
        {
            m_count += count;
        }

        /// <summary>
        /// Resets the internal count to zero
        /// </summary>
        public void ResetCount()
        {
            m_count = 0;
        }
    }
}
