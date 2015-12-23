using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// Creates a trapezoidal motion profile
    /// </summary>
    public class MotionProfile
    {
        internal List<MotionSetpoint> m_path;
        private double m_dt;
        
        /// <summary>
        /// Creates a new MotionProfile that works under the constraints specified.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="maxVelocity"></param>
        /// <param name="acceleration"></param>
        /// <param name="dt"></param>
        public MotionProfile(double distance, double maxVelocity, double acceleration, double dt)
        {
            acceleration = Math.Abs(acceleration) * Math.Sign(distance);
            maxVelocity = Math.Abs(maxVelocity)*Math.Sign(distance);
            m_dt = dt;
            m_path = new List<MotionSetpoint> {new MotionSetpoint(0, 0, acceleration)};

            MotionSetpoint last = m_path.Last();

            while (Math.Abs(last.Position) < Math.Abs(distance) && m_path.Count < 10000)
            {
                double stopDistance = last.Position - last.Velocity * last.Velocity / (2 * -acceleration);

                MotionSetpoint next;

                if (Math.Abs(stopDistance) > Math.Abs(distance))
                    next.Acceleration = -acceleration;
                else if (Math.Abs(last.Velocity) > Math.Abs(maxVelocity))
                {
                    next.Velocity = maxVelocity;
                    next.Acceleration = 0;
                }
                else next.Acceleration = acceleration;
                        
                next.Velocity = last.Velocity + next.Acceleration * dt;
                next.Position = last.Position + next.Velocity * dt;
                //2AX=Vf^2-Vo^2
                //X = (Vf^2 - Vo^2) / 2A;

                m_path.Add(next);
                last = next;
            }

            m_path.Add(new MotionSetpoint(distance, 0, 0));
        }

        private MotionSetpoint Get(double time)
        {
            try
            {
                return m_path[(int) (time/m_dt)];
            }
            catch (ArgumentOutOfRangeException)
            {
                return time < 0 ? m_path.First() : m_path.Last();
            }
        }

        /// <summary>
        /// Gets the expected acceleration at a specific time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public double GetAcceleration(double time) => Get(time).Acceleration;

        /// <summary>
        /// Gets the expected velocity at a specific time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public double GetVelocity(double time) => Get(time).Velocity;

        /// <summary>
        /// Gets the expected position at a specific time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public double GetPosition(double time) => Get(time).Position;
    }
}
