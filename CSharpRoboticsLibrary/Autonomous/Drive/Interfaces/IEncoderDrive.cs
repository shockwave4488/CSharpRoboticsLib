using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPILib;

namespace CSharpRoboticsLib.Autonomous.Drive.Interfaces
{
    /// <summary>
    /// Defines functions for a tank drive with encoders.
    /// If your robot has both encoders and a gyroscope, use this.
    /// </summary>
    interface IEncoderDrive : ITankDrive
    {
        /// <summary>
        /// Gets the encoder associated with the left motor(s).
        /// </summary>
        /// <returns>Left-Side Encoder</returns>
        Encoder LeftEncoder { get; }

        /// <summary>
        /// Gets the encoder associated with the right motor(s).
        /// </summary>
        /// <returns>Right-Side Encoder</returns>
        Encoder RightEncoder { get; }
    }
}
