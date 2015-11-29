namespace CSharpRoboticsLib.Autonomous
{
    /// <summary>
    /// Interface for commands to be used with AutonScheduler
    /// </summary>
    public interface AutonCommand
    {
        /// <summary>
        /// Command intended to be run once per AutonomousPeriodic loop.
        /// </summary>
        void Execute();

        /// <summary>
        /// If the current command is finished.
        /// </summary>
        bool Finished { get; }

        /// <summary>
        /// Time until the command will exit, regardless of what Execute() returns.
        /// Set to negative if you don't wait the command to time out.
        /// </summary>
        double TimeOut { get; set; }
    }
}
