namespace CSharpRoboticsLib.Utility
{
    /// <summary>
    /// Input filter functionally identical to that in the NI LabVIEW Controller library
    /// </summary>
    public class InputFilter
    {
        private static readonly double[] ForewardCoefficients = { 0.144426, 0.235649, 0.239850, 0.235649, 0.144426 };
        private double[] m_internalState;

        /// <summary>
        /// Length of the internal status buffer
        /// </summary>
        public const int BufferLength = 5;

        /// <summary>
        /// Constructs a new InputFilter
        /// </summary>
        /// <param name="initialState">Initial state of the filter</param>
        public InputFilter(double initialState)
        {
            ReInitialize(initialState);
        }

        /// <summary>
        /// Constructs a new InputFilter initialized to Zero
        /// </summary>
        public InputFilter() : this(0) { }

        /// <summary>
        /// sets all internal states to the double specified
        /// </summary>
        public void ReInitialize(double setTo)
        {
            m_internalState = new double[ForewardCoefficients.Length];
            for (int i = 0; i < m_internalState.Length; i++)
                m_internalState[i] = setTo;
        }

        /// <summary>
        /// Updates the internal state by adding the input
        /// </summary>
        /// <param name="input">new input</param>
        private void Update(double input)
        {
            for (int i = m_internalState.Length - 1; i >= 1; i--)
                m_internalState[i] = m_internalState[i - 1];

            m_internalState[0] = input;
        }

        /// <summary>
        /// Gets the filtered output of the internal state
        /// </summary>
        private double Value
        {
            get
            {
                double toReturn = 0;
                for (int i = 0; i < m_internalState.Length; i++)
                    toReturn += m_internalState[i] * ForewardCoefficients[i];

                return toReturn;
            }
        }

        /// <summary>
        /// Gets the filtered output of the internal state
        /// </summary>
        /// <param name="value">Value to update the <see cref="InputFilter"/> with</param>
        /// <returns>Filtered output</returns>
        public double Get(double value)
        {
            Update(value);
            return Value;
        }

        /// <summary>
        /// Gets the filtered value of the input without pushing a new value to the internal buffer
        /// </summary>
        /// <returns></returns>
        public double Get()
        {
            return Value;
        }
    }
}
