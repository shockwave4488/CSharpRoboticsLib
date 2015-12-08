using WPILib;

namespace CSharpRoboticsLib.Autonomous.Drive
{
    /// <summary>
    /// Defines functions for a tank drive with encoders.
    /// If your drivetrain has both a Gyroscope and Encoders, use <see cref="ISensorDrive"/>
    /// </summary>
    public interface IEncoderDrive : ITankDrive
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

    /// <summary>
    /// Provides extentions for <see cref="IEncoderDrive"/>. 
    /// These methods do not need to be called statically.
    /// You will never need to reference this class.
    /// </summary>
    public static class EncoderDriveExtensions
    {

    }
}
