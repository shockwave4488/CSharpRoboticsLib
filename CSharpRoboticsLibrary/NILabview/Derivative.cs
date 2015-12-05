using System.Diagnostics;

namespace CSharpRoboticsLib.NILabview
{
    /// <summary>
    /// Single-Order derivative function.
    /// </summary>
    public class Derivative
    {
        private double m_xPrev;
        private DeltaTime m_dt;

        /// <summary>
        /// The change in time for this particular PID loop in seconds.
        /// Set to negative to determine dt automatically.
        /// </summary>
        public double Dt
        {
            get { return m_dt.Value; }
            set { m_dt.Value = value; }
        }

        /// <summary>
        /// Creates a new Derivative object.
        /// </summary>
        /// <param name="initialCondition">Initial value to set x</param>
        public Derivative(double initialCondition)
        {
            m_xPrev = initialCondition;
            m_dt = new DeltaTime();
        }

        /// <summary>
        /// Creates a new derivative object with the initial value set to zero.
        /// </summary>
        public Derivative() : this(0) { }

        /// <summary>
        /// Gets dx/dt
        /// </summary>
        /// <returns>dx/dt</returns>
        public double Get(double x)
        {
            double toReturn = (x - m_xPrev) / m_dt.Value;

            //Handle division by zero errors, ignoring the input if there is no change in time.
            if (double.IsNaN(toReturn))
            {
                toReturn = 0;
            }
            else
            {
                m_xPrev = x;
            }

            return toReturn;
        }

        /// <summary>
        /// resets the derivative to the specified value
        /// </summary>
        /// <param name="value">value to reset to</param>
        public void ReInitialize(double value)
        {
            m_xPrev = value;
        }

        /// <summary>
        /// resets the derivative to zero
        /// </summary>
        public void ReInitialize()
        {
            ReInitialize(0);
        }
    }
}
