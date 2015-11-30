using System;

namespace CSharpRoboticsLib.NILabview
{
    public class DeltaTime
    {
        private bool m_manualDt;
        private double m_Dt;
        private DateTime m_lastGet;

        /// <summary>
        /// new DeltaTime object
        /// </summary>
        public DeltaTime()
        {
            m_manualDt = false;
            m_Dt = 0;
            m_lastGet = DateTime.Now;
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
                    m_Dt = (DateTime.Now - m_lastGet).TotalSeconds;
                    m_lastGet = DateTime.Now;
                }
                return m_Dt;
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
                    m_Dt = value;
                }
            }
        }
    }
}
