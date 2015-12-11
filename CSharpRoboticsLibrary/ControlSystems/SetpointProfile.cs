using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// A custom profile for a control system. Can be used for any measurable quantity (speed, displacement, time, etc.)
    /// </summary>
    public class SetpointProfile : IPIDController
    {
        private List<Setpoint> m_profile;

        public double SetPoint { get; set; }

        public SetpointProfile()
        {
            m_profile = new List<Setpoint>();
            SetPoint = 0;
        }

        private Setpoint NearestLess(double currentPoint)
        {
            Setpoint toReturn = m_profile.First();
            for (int i = 0; i < m_profile.Count; i++)
            {
                if (m_profile[i].Point > currentPoint)
                    break;
                toReturn = m_profile[i];
            }
            return toReturn;
        }

        private Setpoint NearestGreater(double currentPoint)
        {
            Setpoint toReturn = m_profile.Last();
            for (int i = m_profile.Count - 1; i >= 0; i--)
            {
                if (m_profile[i].Point < currentPoint)
                    break;
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
            Setpoint next = (SetPoint < currentPoint) ? NearestLess(currentPoint) : NearestGreater(currentPoint);
            Setpoint prev = (SetPoint < currentPoint) ? NearestGreater(currentPoint) : NearestLess(currentPoint);

            double setpointSlope = (prev.Value - next.Value)/(prev.Point - next.Point);
            setpointSlope = double.IsNaN(setpointSlope) ? 0 : setpointSlope; //Handle division by zero

            double ratioComplete = (currentPoint - prev.Point)/(next.Point - prev.Point);
            ratioComplete = double.IsNaN(ratioComplete) ? 0 : setpointSlope; //Handle division by zero

            double delta = ratioComplete*setpointSlope;
            return delta + prev.Value;
        }

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
