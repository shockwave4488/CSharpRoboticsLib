using System;
using CSharpRoboticsLib.ControlSystems;
using CSharpRoboticsLib.Utility;
using WPILib;

namespace CSharpRoboticsLib.WPIExtensions
{
    /// <summary>
    /// An encoder with corrected rate calculation and distance per pulse setting
    /// </summary>
    public class EnhancedEncoder : Encoder
    {
        private Derivative m_velocityFilter;

        public double Dt
        {
            get { return m_velocityFilter.Dt; }
            set { m_velocityFilter.Dt = value; }
        }

        /// <summary>
        /// CSharpRoboticslib.Extras.EnhancedEncoder
        /// </summary>
        /// <param name="aChannel"></param>
        /// <param name="bChannel"></param>
        public EnhancedEncoder(int aChannel, int bChannel) : base(aChannel, bChannel)
        {
            m_velocityFilter = new Derivative();
        }

        /// <summary>
        /// Gets the velocity reported by the encoder.
        /// </summary>
        /// <returns>Derivative of the distance</returns>
        public override double GetRate()
        {
            return m_velocityFilter.Get(GetDistance());
        }

        /// <summary>
        /// Resets the encoder and derivative
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            m_velocityFilter.ReInitialize();
        }
    }

    /// <summary>
    /// Provides an extension method for setting <see cref="Encoder.DistancePerPulse"/>
    /// </summary>
    public static class EncoderExtension
    {
        /// <summary>
        /// Sets <see cref="DistancePerPulse"/> to the proper value given by diameter of a wheel and counts per revolution
        /// </summary>
        /// <param name="wheelDiameter">Diameter of the wheel. Units used here will determine units of <see cref="GetRate"/></param>
        /// <param name="countPerRevolution">Counts per revolution of the encoder. Usually 360 or 256.</param>
        public static void SetDistancePerPulse(this Encoder e, double wheelDiameter, int countPerRevolution)
        {
            e.DistancePerPulse = (Math.PI * wheelDiameter) / countPerRevolution;
        }
    }
}
