namespace CSharp_Robotics_Library.FlowControl
{
    /// <summary>
    /// Will return true after a threshold has been met by waiting for a number of call counts
    /// </summary>
    public class WaitByCallCount
    {
        private int count;
        private int threshold;

        /// <summary>
        /// New instance of WaitByCallCount
        /// </summary>
        /// <param name="count_">the threshold after which it should return true</param>
        public WaitByCallCount(int count_)
        {
            threshold = count_;
        }

        public bool WaitComplete
        {
            get { return count > threshold; }
        }

        /// <summary>
        /// Increases the internal count by 1 if true, resets count to 0 if false
        /// </summary>
        public void Update(bool increase)
        {
            count = increase ? count + 1 : 0;
        }
    }
}
