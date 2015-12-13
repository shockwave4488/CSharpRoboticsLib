using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// A custom profile for a control system. Can be used for any measurable quantity (speed, displacement, time, etc.)
    /// </summary>
    public class SetPointProfile : IPIDController
    {
        private List<Setpoint> m_profile;

        /// <summary>
        /// Point to approach
        /// </summary>
        public double SetPoint { get; set; }

        /// <summary>
        /// Constructs a new, empty <see cref="SetPointProfile"/>
        /// </summary>
        public SetPointProfile()
        {
            m_profile = new List<Setpoint>();
            SetPoint = 0;
        }

        private Setpoint NearestLess(double currentPoint)
        {
            Setpoint toReturn = m_profile[0];
            for (int i = 1; i < m_profile.Count; i++)
            {
                if (m_profile[i].Point > currentPoint)
                    return toReturn;
                toReturn = m_profile[i];
            }
            return toReturn;
        }

        private Setpoint NearestGreater(double currentPoint)
        {
            Setpoint toReturn = m_profile[m_profile.Count - 1];
            for (int i = m_profile.Count - 2; i >= 0; i--)
            {
                if (m_profile[i].Point < currentPoint)
                    return toReturn;
                toReturn = m_profile[i];
            }
            return toReturn;
        }

        /// <summary>
        /// Get the value of the path at the current point
        /// </summary>
        /// <param name="currentPoint">point along the profile to measure</param>
        /// <returns>value of the profile at that point</returns>
        public double Get(double currentPoint)
        {
            //be warned: here be lots of math
            Setpoint next = NearestGreater(currentPoint);
            Setpoint prev = NearestLess(currentPoint);

            double direction = Math.Sign(SetPoint - currentPoint);

            if (next.Point == prev.Point) //If they are the EXACT same point
            {
                return next.Value * direction;
            }

            double setpointSlope = (prev.Value - next.Value)/(prev.Point - next.Point);
            setpointSlope = double.IsNaN(setpointSlope) ? 0 : setpointSlope; //Handle division by zero

            double ratioComplete = (currentPoint - prev.Point)/(next.Point - prev.Point);
            ratioComplete = double.IsNaN(ratioComplete) ? 0 : ratioComplete; //Handle division by zero

            double delta = ratioComplete*setpointSlope;
            return (delta + prev.Value) * direction;
        }

        /// <summary>
        /// Adds a setpoint to the path
        /// </summary>
        /// <param name="point">location of the setpoint</param>
        /// <param name="value">value of the setpoint</param>
        public void Add(double point, double value)
        {
            for (int i = 0; i < m_profile.Count; i++)
            {
                if (m_profile[i].Point > point)
                {
                    m_profile.Insert(i, new Setpoint(point, value));
                    return;
                }
            }

            //If the point is greatest or the list is empty
            m_profile.Add(new Setpoint(point, value));
        }
    }
}
