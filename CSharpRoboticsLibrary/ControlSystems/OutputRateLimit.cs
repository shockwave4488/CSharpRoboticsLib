using System;
using CSharpRoboticsLib.FlowControl;
using CSharpRoboticsLib.Utility;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// Limits the rate of change of an output
    /// </summary>
    public class OutputRateLimit
    {
        private DeltaTime m_dt;
        private double m_feedback;

        public double Dt
        {
            get { return m_dt.Value; }
            set { m_dt.Value = value; }
        }

        /// <summary>
        /// Maximum units per second of change
        /// </summary>
        public double UnitsPerSecond { get; set; }

        /// <summary>
        /// new OutputRateLimit
        /// </summary>
        /// <param name="unitsPerSecond"></param>
        public OutputRateLimit(double unitsPerSecond)
        {
            m_dt = new DeltaTime();
            UnitsPerSecond = unitsPerSecond;
            m_feedback = 0;
        }

        /// <summary>
        /// Gets the value and updates the internal state
        /// </summary>
        /// <param name="input">value to be rate limited</param>
        /// <returns></returns>
        public double Get(double input)
        {
            double units = Math.Abs(UnitsPerSecond*m_dt.Value);
            double delta = input - m_feedback;
            double max = units*Math.Sign(delta);

            double toReturn = units >= Math.Abs(delta) ? input : max + m_feedback;

            m_feedback = toReturn;

            return toReturn;
        }
    }
}

