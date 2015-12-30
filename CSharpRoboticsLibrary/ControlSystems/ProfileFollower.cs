using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// An <see cref="IMotionController"/> designed to follow a <see cref="MotionProfile"/>
    /// </summary>
    public class ProfileFollower : IMotionController
    {
        private double m_kv;
        private double m_ka;
        private double m_setpoint;

        private Stopwatch m_timer;
        private MotionProfile m_profile;
        private IMotionController m_correction;

        /// <summary>
        /// Creates a ProfileFollower to follow the specified profile.
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="kv"></param>
        /// <param name="ka"></param>
        public ProfileFollower(MotionProfile profile, double kv, double ka) 
            : this(profile, kv, ka, null)
        {
        }

        /// <summary>
        /// Creates a new ProfileFollower to follow the specified profile
        /// with an <see cref="IMotionController"/> for position correction
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="kv"></param>
        /// <param name="ka"></param>
        /// <param name="correction"></param>
        public ProfileFollower(MotionProfile profile, double kv, double ka, IMotionController correction)
        {
            m_profile = profile;
            m_correction = correction;
            m_kv = kv;
            m_ka = ka;

            m_timer = new Stopwatch();
            SetPoint = m_profile.m_path.Last().Position;
        }

        /// <summary>
        /// Gets the motor power, using the input for the correction sensor.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double Get(double input)
        {
            if (!m_timer.IsRunning)
            {
                m_timer.Restart();
            }

            double velocity = m_kv*m_profile.GetVelocity(m_timer.ElapsedTicks/Stopwatch.Frequency);
            double acceleration = m_ka*m_profile.GetAcceleration(m_timer.ElapsedTicks/Stopwatch.Frequency);
            double correction = m_correction?.Get(m_profile.GetPosition(m_timer.ElapsedTicks/Stopwatch.Frequency) - input) ?? 0;
            bool reverse = m_profile.GetPosition(m_timer.ElapsedMilliseconds/Stopwatch.Frequency) > SetPoint;

            return (velocity + acceleration + correction) * (reverse ? -1 : 1);
        }

        /// <summary>
        /// The point to approach. 
        /// If this point is not defined as either end point along the <see cref="MotionProfile"/>, expect oscillating behavior.
        /// </summary>
        public double SetPoint
        {
            get { return m_setpoint; }
            set
            {
                m_timer.Restart();
                m_setpoint = value;
            }
        }
    }
}
