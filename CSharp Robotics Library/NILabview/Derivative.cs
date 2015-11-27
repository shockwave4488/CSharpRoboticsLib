using System.Diagnostics;

namespace CSharpRoboticsLib.NILabview
{
    /// <summary>
    /// Single-Order derivative function.
    /// </summary>
    public class Derivative
    {
        private double xPrev1, xPrev2;
        private Stopwatch dt;

        /// <summary>
        /// Creates a new Derivative object.
        /// </summary>
        /// <param name="InitialCondition">Initial value to set x</param>
        public Derivative(double InitialCondition)
        {
            xPrev1 = xPrev2 = InitialCondition;
            dt = new Stopwatch();
        }

        /// <summary>
        /// Creates a new derivative object with the initial value set to zero.
        /// </summary>
        public Derivative() : this(0) { }

        /// <summary>
        /// Updates the derivative. Call this after Get() has been used.
        /// </summary>
        /// <param name="x">new value of x</param>
        public void Update(double x)
        {
            xPrev2 = xPrev1;
            xPrev1 = x;
            dt.Restart();
        }

        /// <summary>
        /// Gets dx/dt
        /// </summary>
        /// <returns>dx/dt</returns>
        public double Get()
        {
            double toReturn = (xPrev1 - xPrev2) / dt.Elapsed.TotalSeconds;
            toReturn = double.IsNaN(toReturn) ? 0 : toReturn; //handling division by zero errors
            return toReturn;
        }
    }
}
