using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPILib;

namespace CSharpRoboticsLib.Autonomous.DriveCommands.Interfaces
{
    /// <summary>
    /// Defines functions for a tank drive with encoders.
    /// </summary>
    interface IEncoderDrive : ITankDrive
    {
        /// <summary>
        /// Gets the encoder associated with the left motor(s).
        /// </summary>
        /// <returns>Left-Side Encoder</returns>
        Encoder GetLeftEncoder();

        /// <summary>
        /// Gets the encoder associated with the right motor(s).
        /// </summary>
        /// <returns>Right-Side Encoder</returns>
        Encoder GetRightEncoder();
    }
}
