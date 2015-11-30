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
        /// New Integral.
        /// </summary>
        public Integral()
        {
            m_value = 0;
            m_dt = new DeltaTime();
        }

        /// <summary>
        /// Get the value of the integral
        /// </summary>
        /// <param name="x">Value to add to the integral</param>
        /// <returns>Integral(x)dt</returns>
        public double Get(double x)
        {
            m_value += (x * m_dt.Value);
            return m_value;
        }

        /// <summary>
        /// Gets the value of the integral
        /// </summary>
        /// <returns>Integral(x)dt</returns>
        public double Get()
        {
            return m_value;
        }

        /// <summary>
        /// Reinitialize the integral to the specified value.
        /// </summary>
        /// <param name="value">value to set the integral to</param>
        public void ReInitialize(double value)
        {
            m_value = value;
        }

        /// <summary>
        /// Reinitialize the integral to zero.
        /// </summary>
        public void ReInitialize()
        {
            m_value = 0;
        }
    }
}
