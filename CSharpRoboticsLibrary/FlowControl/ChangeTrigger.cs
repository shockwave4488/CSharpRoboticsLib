namespace CSharpRoboticsLib.FlowControl
{
    /// <summary>
    /// Can detect a change in data for any given equatable type
    /// </summary>
    /// <typeparam name="T">An equatable type</typeparam>
    public class ChangeTrigger<T>
    {
        private T m_state;

        /// <summary>
        /// Creates a new ChangeTrigger with a default initial state
        /// </summary>
        public ChangeTrigger()
        {
            m_state = default(T);
        }

        /// <summary>
        /// creates a new ChangeTrigger with a specific initial state
        /// </summary>
        public ChangeTrigger(T initialState)
        {
            m_state = initialState;
        }

        /// <summary>
        /// Returns true if there is a change in value and does not update the internal state
        /// </summary>
        /// <param name="toCompare">value to compare</param>
        /// <returns>change in value</returns>
        public bool GetChange(T toCompare)
        {
            return !m_state?.Equals(toCompare) ?? null != toCompare;
        }

        /// <summary>
        /// Returns true if there is a change in value and updates the internal state
        /// </summary>
        /// <param name="toCompare">value to compare and update the internal state with</param>
        /// <returns>Change in value</returns>
        public bool GetChangeUpdate(T toCompare)
        {
            bool toReturn = !m_state?.Equals(toCompare) ?? null != toCompare;
            m_state = toCompare;
            return toReturn;
        }

        /// <summary>
        /// Manually update the internal state
        /// </summary>
        /// <param name="newValue">New internal state</param>
        public void Update(T newValue)
        {
            m_state = newValue;
        }
    }
}
