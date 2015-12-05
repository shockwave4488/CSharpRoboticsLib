using System;
using System.Diagnostics;

namespace CSharpRoboticsLib.NILabview
{
    public class DeltaTime
    {
        private bool m_manualDt;
        private double m_dt;
        private Stopwatch m_timer;

        /// <summary>
        /// new DeltaTime object
        /// </summary>
        public DeltaTime()
        {
            m_manualDt = false;
            m_dt = 0;
            m_timer = new Stopwatch();
            m_timer.Start();
        }

        /// <summary>
        /// Get or manually set the delta time value.
        /// Set to any positive value to manually keep a value.
        /// Set to -1 to determine dt automatically.
        /// </summary>
        public double Value
        {
            get
            {
                if (!m_manualDt)
                {
                    m_dt = (double)m_timer.ElapsedTicks / Stopwatch.Frequency;
                    m_timer.Restart();
                }
                return m_dt;
            }
            set
            {
                if (value < 0)
                {
                    m_manualDt = false;
                }
                else
                {
                    m_manualDt = true;
                    m_dt = value;
                }
            }
        }
    }
}
