namespace CSharpRoboticsLib.NILabview
{
    /// <summary>
    /// Input filter functionally identical to that in the NI LabVIEW PID library
    /// </summary>
    public class InputFilter
    {
        private static readonly double[] forewardCoefficients = new double[] { 0.144426, 0.235649, 0.239850, 0.235649, 0.144426 };
        private double[] internalState;

        /// <summary>
        /// Constructs a new InputFilter
        /// </summary>
        /// <param name="initialState">Initial state of the filter</param>
        public InputFilter(double initialState)
        {
            ReInitialize(initialState);
        }

        /// <summary>
        /// sets all internal states to the double specified
        /// </summary>
        public void ReInitialize(double setTo)
        {
            internalState = new double[forewardCoefficients.Length];
            for (int i = 0; i < internalState.Length; i++)
                internalState[i] = setTo;
        }

        /// <summary>
        /// Updates the internal state by adding the input
        /// </summary>
        /// <param name="input">new input</param>
        public void Update(double input)
        {
            for (int i = internalState.Length - 1; i >= 1; i--)
                internalState[i] = internalState[i - 1];

            internalState[0] = input;
        }

        /// <summary>
        /// Gets the filtered output of the internal state
        /// </summary>
        public double GetValue()
        {
            double toReturn = 0;
            for (int i = 0; i < internalState.Length; i++)
                toReturn += internalState[i] * forewardCoefficients[i];

            return toReturn;
        }

    }
}
