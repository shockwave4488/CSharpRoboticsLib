namespace CSharpRoboticsLib.Drive.Interfaces
{
    /// <summary>
    /// Defines functions for a tank drive with encoders.
    /// If your drivetrain has both a Gyroscope and Encoders, use <see cref="ISensorDrive"/>
    /// </summary>
    public interface IEncoderDrive : ITankDrive
    {
        /// <summary>
        /// Encoders on the Drive Train
        /// </summary>
        DriveEncoders Encoders { get; }
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
