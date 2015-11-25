namespace CSharp_Robotics_Library.FlowControl
{
    /// <summary>
    /// Toggle Latch
    /// </summary>
    public class Toggle
    {
        private bool m_state;
        private bool m_feedback;

        public Toggle() { m_feedback = false; m_state = false; }

        /// <summary>
        /// Set or get the state of the toggle latch
        /// </summary>
        public bool state
        {
            get
            {
                return m_state;
            }
            set
            {
                if (value && !m_feedback)
                    m_state = !m_state;

                m_feedback = value;
            }
        }

        /// <summary>
        /// Force the internal state of the latch to a value
        /// </summary>
        public void Force(bool value)
        {
            m_state = value;
        }
    }
}
