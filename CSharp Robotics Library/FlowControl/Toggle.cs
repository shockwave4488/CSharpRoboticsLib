namespace CSharpRoboticsLib.FlowControl
{
    /// <summary>
    /// Toggle Latch
    /// </summary>
    public class Toggle
    {
        private bool m_state;
        private bool m_feedback;

        /// <summary>
        /// Creates a new toggle set to false
        /// </summary>
        public Toggle()
        {
            m_feedback = false;
            m_state = false;
        }

        /// <summary>
        /// Creates a new toggle set to an initial state
        /// </summary>
        /// <param name="initialState">state to initialize the toggle with</param>
        public Toggle(bool initialState)
        {
            m_feedback = false;
            m_state = true;
        }

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
