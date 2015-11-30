using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Commands;

namespace CSharpRoboticsLib.Autonomous.DriveCommands
{
    public class AutonomousBaseCommand : Command
    {
        public AutonomousBaseCommand()
        {
            // Use Requires() here to declare subsystem dependencies
            // eg. Requires(chassis);
        }

        // Called just before this Command runs the first time
        protected override void Initialize()
        {
        }

        // Called repeatedly when this Command is scheduled to run
        protected override void Execute()
        {
        }

        // Make this return true when this Command no longer needs to run Execute()
        protected override bool IsFinished()
        {
            return false;
        }

        // Called once after isFinished returns true
        protected override void End()
        {
        }

        // Called when another command which requires one or more of the same
        // subsystems is scheduled to run
        protected override void Interrupted()
        {
        }
    }
}
