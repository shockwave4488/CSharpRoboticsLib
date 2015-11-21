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
        /// <returns>True if the command is complete</returns>
        bool Execute();

        /// <summary>
        /// Double from 0 to 1, reporting how close to completion the command is
        /// </summary>
        /// <returns>Double 0 - 1</returns>
        double PercentComplete();

        /// <summary>
        /// Time until the command will exit, regardless of what Execute() returns.
        /// </summary>
        double TimeOut { get; set; }
    }
}
