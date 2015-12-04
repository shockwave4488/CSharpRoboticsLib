using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoboticsLib.Autonomous.Drive.Interfaces
{
    /// <summary>
    /// Defines functions for a basic tank drive.
    /// No sensors, no CAN, just left and right motors.
    /// </summary>
    public interface ITankDrive
    {
        /// <summary>
        /// Sets the powers of the Left and Right Motors.
        /// </summary>
        /// <param name="LeftPower">power the Left Motor is set to</param>
        /// <param name="RightPower">power the Left Motor is set to</param>
        void SetPowers(double LeftPower, double RightPower);
    }
}
