namespace CSharpRoboticsLib.Drive.Interfaces
{
    /// <summary>
    /// Defines functions for a drive train with both a gyroscope and encoders.
    /// </summary>
    public interface ISensorDrive : IEncoderDrive, IGyroscopeDrive
    {
    }

    /// <summary>
    /// Provides extentions for <see cref="ISensorDrive"/>. 
    /// These methods do not need to be called statically.
    /// You will never need to reference this class.
    /// </summary>
    public static class SensorDriveExtensions
    {

    }
}
