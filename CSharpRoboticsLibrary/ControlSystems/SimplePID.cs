using System;
using CSharpRoboticsLib.Utility;
using CSharpRoboticsLib.WPIExtensions;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// Simpler form of PID logic
    /// </summary>
    public class SimplePID : IMotionController
    {
        private double m_kP, m_kI, m_kD;
        private Derivative m_d;
        private Integral m_i;
        
        /// <summary>
        /// The change in time for this particular PID loop in seconds.
        /// Set to negative to determine dt automatically.
        /// </summary>
        public double Dt
        {
            set { m_d.Dt = value; m_i.Dt = value; }
        }

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
        /// Defines if the system is continuous
        /// </summary>
        public bool Continuous { get; set; }

        /// <summary>
        /// Maximum possible input
        /// </summary>
        public double MaxInput { get; set; }

        /// <summary>
        /// Minimum possible input
        /// </summary>
        public double MinInput { get; set; }

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
                throw new ArgumentException($"Invalid Arguments: {max} Is less than {min}");

            m_kP = p; m_kI = i; m_kD = d;
            Max = max;
            Min = min;
            SetPoint = 0;
            m_d = new Derivative();
            m_i = new Integral();
            Continuous = false;
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
            double error = (SetPoint - currentPoint);
            if (Continuous)
                error = Utility.Util.WrapError(currentPoint, SetPoint, MinInput, MaxInput);

            double p = m_kP == 0 ? 0 : m_kP * error;
            double I = m_kI == 0 ? 0 : m_kI * m_i.Get(error);
            double d = m_kD == 0 ? 0 : m_kD * m_d.Get(error);

            return Utility.Util.Limit(p + I + d, Min, Max);
        }

        /// <summary>
        /// Reset the integral value.
        /// </summary>
        public void ResetIntegral()
        {
            m_i.ReInitialize();
        }
    }
}
