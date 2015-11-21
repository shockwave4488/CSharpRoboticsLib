using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Robotics_Library.NI_PID
{
    /// <summary>
    /// Single-Order derivative function
    /// </summary>
    public class Derivative
    {
        private double xPrev1, xPrev2;
        private DateTime dt;

        /// <summary>
        /// Creates a new Derivative object.
        /// </summary>
        /// <param name="InitialCondition">Initial value to set x</param>
        public Derivative(double InitialCondition)
        {
            xPrev1 = xPrev2 = InitialCondition;
            dt = DateTime.Now;
        }

        /// <summary>
        /// Updates the derivative. Call this after Get() has been used.
        /// </summary>
        /// <param name="x">new value of x</param>
        public void Update(double x)
        {
            xPrev2 = xPrev1;
            xPrev1 = x;
            dt = DateTime.Now;
        }

        /// <summary>
        /// Gets dx/dt
        /// </summary>
        /// <returns>dx/dt</returns>
        public double Get()
        {
            return (xPrev1 - xPrev2) / (DateTime.Now - dt).TotalSeconds;
        }
    }
}
