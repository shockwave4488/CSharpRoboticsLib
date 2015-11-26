using System;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// Simpler form of PID logic
    /// </summary>
    public class SimplePID
    {
        private double P, I, D;
        private double accumulatedIntegral;
        private double currentPointFeedback;
        
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
        public double setpoint { get; set; }

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

            P = p; I = i; D = d;
            accumulatedIntegral = 0;
            currentPointFeedback = 0;
            Max = max;
            Min = min;
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
        public double get(double currentPoint)
        {
            return limit(((setpoint - currentPoint) * P) + ((currentPoint - currentPointFeedback) * -D) + accumulatedIntegral);
        }

        /// <summary>
        /// Updates the Integral and Derivative for the PID loop based on the current setpoint
        /// </summary>
        /// <param name="currentPoint">Current point as read by a sensor</param>
        public void Update(double currentPoint)
        {
            if (I != 0) accumulatedIntegral += (currentPoint - currentPointFeedback) * I;
            if (D != 0) currentPointFeedback = currentPoint;
        }

        private double limit(double value)
        {
            value = value > Max ? Max : value;
            value = value < Min ? Min : value;
            return value;
        }
    }
}
