using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace CSharpRoboticsLib.Autonomous.Drive.Interfaces
{
    /// <summary>
    /// Defines functions for a Tank Drive with a gyroscope.
    /// If your robot has both encoders and a gyroscope, use <see cref="IEncoderDrive"/>
    /// </summary>
    interface IGyroscopeDrive : ITankDrive
    {
        /// <summary>
        /// Returns the gyroscope reference
        /// </summary>
        /// <returns>Robot's Gyroscope</returns>
        AnalogGyro Gyroscope { get; }
    }
}
