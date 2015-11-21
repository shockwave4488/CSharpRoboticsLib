namespace CSharp_Robotics_Library.FlowControl
{
    /// <summary>
    /// Toggle Latch
    /// </summary>
    public class Toggle
    {
        private bool _state;
        private bool feedback;

        public Toggle() { feedback = false; _state = false; }

        /// <summary>
        /// Set or get the state of the toggle latch
        /// </summary>
        public bool state
        {
            get
            {
                return _state;
            }
            set
            {
                if (value && !feedback)
                    _state = !_state;

                feedback = value;
            }
        }

        /// <summary>
        /// Force the internal state of the latch to a value
        /// </summary>
        public void Force(bool value)
        {
            _state = value;
        }
    }
}
