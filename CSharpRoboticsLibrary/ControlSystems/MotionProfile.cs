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
            m_dt = dt;
            m_path = new List<MotionSetpoint> {new MotionSetpoint(0, 0, acceleration)};

            MotionSetpoint last = m_path.Last();

            while (last.Position < distance)
            {
                MotionSetpoint next;
                next.Acceleration = last.Acceleration;
                next.Velocity = last.Velocity + next.Acceleration * dt;
                next.Position = last.Position + next.Velocity * dt;
                //2AX=Vf^2-Vo^2
                //X = (Vf^2 - Vo^2) / 2A;
                double stopDistance = next.Position - next.Velocity * next.Velocity / (2 * -acceleration);

                if (stopDistance > distance)
                    next.Acceleration = -acceleration;
                else if (next.Velocity > maxVelocity)
                {
                    next.Velocity = maxVelocity;
                    next.Acceleration = 0;
                }

                m_path.Add(next);
                last = next;
            }
        }

        private MotionSetpoint Get(double time)
        {
            return m_path[(int)(time / m_dt)];
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
