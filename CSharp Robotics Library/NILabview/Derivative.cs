using System.Diagnostics;

namespace CSharpRoboticsLib.NILabview
{
    /// <summary>
    /// Single-Order derivative function.
    /// </summary>
    public class Derivative
    {
        private double xPrev;
        private DeltaTime dt;

        /// <summary>
        /// Creates a new Derivative object.
        /// </summary>
        /// <param name="InitialCondition">Initial value to set x</param>
        public Derivative(double InitialCondition)
        {
            xPrev = InitialCondition;
            dt = new DeltaTime();
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
            double toReturn = (x - xPrev) / dt.Value;

            //Handle division by zero errors, Essentially ignoring the input if there is no change in time.
            if (double.IsNaN(toReturn))
            {
                toReturn = 0;
            }
            else
            {
                xPrev = x;
            }

            return toReturn;
        }

        /// <summary>
        /// resets the derivative to the specified value
        /// </summary>
        /// <param name="value">value to reset to</param>
        public void ReInitialize(double value)
        {
            xPrev = value;
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
