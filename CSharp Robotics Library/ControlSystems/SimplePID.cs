using System;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// Simpler form of PID logic
    /// </summary>
    public class SimplePID
    {
        private double m_P, m_I, m_D;
        private double m_accumulatedIntegral;
        private double m_currentPointFeedback;
        
        /// <summary>
        /// Maximum value the PID Controller can return
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// Minumum value the PID Controller can return
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// Current setpoint the PID Controller is reacting to
        /// </summary>
        public double SetPoint { get; set; }

        /// <summary>
        /// Creates a new instance of the SimplePID class
        /// </summary>
        /// <param name="p">Proportional constant</param>
        /// <param name="i">Integral Constant</param>
        /// <param name="d">Derivative Constant</param>
        /// <param name="min">Minimum allowed output of the PID loop</param>
        /// <param name="max">Maximum allowed output of the PID loop</param>
        public SimplePID(double p, double i, double d, double min, double max)
        {
            if(max < min)
                throw new Exception("Invalid Arguments: " + max + " Is less than " + min);

            m_P = p; m_I = i; m_D = d;
            m_accumulatedIntegral = 0;
            m_currentPointFeedback = 0;
            Max = max;
            Min = min;
            SetPoint = 0;
        }

        /// <summary>
        /// Creates a new SimplePID object
        /// </summary>
        /// <param name="p">Proportional Constant</param>
        /// <param name="i">Integral Constant</param>
        /// <param name="d">Derivative Constant</param>
        public SimplePID(double p, double i, double d) : this(p, i, d, double.MinValue, double.MaxValue) { }

        /// <summary>
        /// Gets the value of the PID loop, using the point given as the input for the proportional value
        /// </summary>
        /// <param name="currentPoint">current point of the system as read by a sensor</param>
        /// <returns>value calculated by the PID loop</returns>
        public double Get(double currentPoint)
        {
            return limit(((SetPoint - currentPoint) * m_P) + ((currentPoint - m_currentPointFeedback) * m_D) + m_accumulatedIntegral * m_I);
        }

        /// <summary>
        /// Updates the Integral and Derivative for the PID loop based on the current setpoint
        /// </summary>
        /// <param name="currentPoint">Current point as read by a sensor</param>
        public void Update(double currentPoint)
        {
            if (m_I != 0)
                m_accumulatedIntegral += (SetPoint - currentPoint);

            if (m_D != 0)
                m_currentPointFeedback = currentPoint;
        }

        private double limit(double value)
        {
            value = value > Max ? Max : value;
            value = value < Min ? Min : value;
            return value;
        }
    }
}
