using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoboticsLib.NILabview
{
    /// <summary>
    /// Numeric Time-based Integration
    /// </summary>
    public class Integral
    {
        private double m_value;
        private DeltaTime dt;

        public Integral()
        {
            m_value = 0;
            dt = new DeltaTime();
        }

        /// <summary>
        /// Get the value of the integral
        /// </summary>
        /// <param name="x">Value to add to the integral</param>
        /// <returns>Integral(x)dt</returns>
        public double Get(double x)
        {
            m_value += (x * dt.Value);
            return m_value;
        }
    }
}
