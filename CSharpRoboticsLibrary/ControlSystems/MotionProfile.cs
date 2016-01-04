using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="dt">Time interval each point will be calculated for</param>
        public MotionProfile(double distance, double maxVelocity, double acceleration, double dt)
        {
            //Not using DeltaTime here because this is just for mathematical purposes
            m_dt = dt;
            m_path = new List<MotionSetpoint> { new MotionSetpoint(0, 0, acceleration) };

            //Making sure signs are correct
            acceleration = Math.Abs(acceleration) * Math.Sign(distance);
            maxVelocity = Math.Abs(maxVelocity)*Math.Sign(distance);

            MotionSetpoint last = m_path.Last();

            //Actual path generation
            while (Math.Abs(last.Position) < Math.Abs(distance))
            {
                MotionSetpoint next = new MotionSetpoint(last, dt);

                double stopDistance = last.Position - last.Velocity * last.Velocity / (2 * -acceleration);
                double finalV = Math.Sqrt(2*-acceleration*(distance - last.Position) + last.Velocity*last.Velocity);

                if (Math.Abs(stopDistance) > Math.Abs(distance) || finalV * Math.Sign(distance) > 0 || last.Acceleration < 0)
                {
                    last.Acceleration = -acceleration;
                    next = new MotionSetpoint(last, dt);
                }
                else if (Math.Abs(next.Velocity) >= Math.Abs(maxVelocity))
                {
                    last.Velocity = maxVelocity;
                    last.Acceleration = (last.Acceleration < 0) ? last.Acceleration : 0;
                    next = new MotionSetpoint(last, dt);
                }
                else next.Acceleration = acceleration;

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

        /// <summary>
        /// String format of a MotionProfile
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string toReturn = "";
            foreach (MotionSetpoint m in m_path)
            {
                toReturn += $"[{m.Position}, {m.Velocity}, {m.Acceleration}]\n";
            }
            return toReturn;
        }
    }
}
