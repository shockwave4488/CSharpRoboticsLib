using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Robotics_Library.Autonomous
{
    /// <summary>
    /// Interface for commands to be used with AutonScheduler
    /// </summary>
    public interface AutonCommand
    {
        /// <summary>
        /// Command intended to be run once per AutonomousPeriodic loop.
        /// </summary>
        /// <returns>Percent complete - 1 if done</returns>
        double Execute();

        /// <summary>
        /// Time until the command will exit, regardless of what Execute() returns.
        /// Set to negative if you don't wait the command to time out.
        /// </summary>
        double TimeOut { get; set; }
    }
}
