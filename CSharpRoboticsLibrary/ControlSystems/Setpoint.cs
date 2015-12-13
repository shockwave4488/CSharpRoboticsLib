using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// Describes a set point for a profiling system.
    /// </summary>
    internal struct Setpoint
    {
        /// <summary>
        /// Location or set point
        /// </summary>
        public double Point;

        /// <summary>
        /// Value at the set point
        /// </summary>
        public double Value;

        /// <summary>
        /// new SetPoint
        /// </summary>
        /// <param name="point"></param>
        /// <param name="value"></param>
        public Setpoint(double point, double value)
        {
            Point = point;
            Value = value;
        }
    }
}
