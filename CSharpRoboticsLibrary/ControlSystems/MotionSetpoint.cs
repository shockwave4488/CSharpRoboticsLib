using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoboticsLib.ControlSystems
{
    struct MotionSetpoint
    {
        public double Position;
        public double Velocity;
        public double Acceleration;

        public MotionSetpoint(double position, double velocity, double acceleration)
        {
            Position = position;
            Velocity = velocity;
            Acceleration = acceleration;
        }
    }
}
