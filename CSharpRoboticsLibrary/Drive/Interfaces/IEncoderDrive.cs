using System;
using CSharpRoboticsLib.ControlSystems;
using WPILib;
using static CSharpRoboticsLib.Utility.Util;

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
        /// <summary>
        /// Drives the robot at a power until it reaches an absolute distance
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="location"></param>
        /// <param name="power"></param>
        /// <param name="brake"></param>
        /// <param name="interval"></param>
        public static void DriveToDistance(this IEncoderDrive drive, double location, double power, bool brake, double interval = 0.02)
        {
            double direction = drive.Encoders.LinearDistance < location ? -1 : 1;

            while ((drive.Encoders.LinearDistance - location)*direction > 0)
            {
                drive.SetPowers(power, power);
                AccurateWaitSeconds(interval);
            }

            if (brake)
                drive.SetPowers(0, 0);
        }

        /// <summary>
        /// Drives the robot at a power until it reaches a relative distance
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="location"></param>
        /// <param name="power"></param>
        /// <param name="brake"></param>
        /// <param name="interval"></param>
        public static void DriveForDistance(this IEncoderDrive drive, double location, double power, bool brake, double interval = 0.02)
        {
            drive.DriveToDistance(location + drive.Encoders.LinearDistance, power, brake, interval);
        }

        /// <summary>
        /// Drives the robot to a location according to an <see cref="IMotionController"/>
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="controller"></param>
        /// <param name="location"></param>
        /// <param name="tolerance"></param>
        /// <param name="brake"></param>
        /// <param name="interval"></param>
        public static void DriveToDistance(this IEncoderDrive drive, IMotionController controller, double location, double tolerance, bool brake, double interval = 0.02)
        {
            controller.SetPoint = location;

            while (Math.Abs(location - drive.Encoders.LinearDistance) > tolerance)
            {
                double power = controller.Get(drive.Encoders.LinearDistance);
                drive.SetPowers(power, power);
                AccurateWaitSeconds(interval);
            }

            if (brake)
                drive.SetPowers(0, 0);
        }

        /// <summary>
        /// Drive the robot to a relative position according to an <see cref="IMotionController"/>
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="controller"></param>
        /// <param name="distance"></param>
        /// <param name="tolerance"></param>
        /// <param name="brake"></param>
        /// <param name="interval"></param>
        public static void DriveForDistance(this IEncoderDrive drive, IMotionController controller, double distance, double tolerance, bool brake, double interval = 0.02)
        {
            drive.DriveToDistance(controller, distance + drive.Encoders.LinearDistance, tolerance, brake, interval);
        }

        /// <summary>
        /// Drives the robot at a set speed to a location
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="speedController"></param>
        /// <param name="speed"></param>
        /// <param name="location"></param>
        /// <param name="brake"></param>
        /// <param name="interval"></param>
        public static void DriveToAtSpeed(this IEncoderDrive drive, IMotionController speedController, double speed, double location, bool brake, double interval = 0.02)
        {
            speedController.SetPoint = drive.Encoders.LinearDistance < location ? speed : -speed;

            while ((drive.Encoders.LinearDistance - location)*Math.Sign(speedController.SetPoint) > 0)
            {
                double power = speedController.Get(drive.Encoders.LinearSpeed);
                drive.SetPowers(power, power);
                AccurateWaitSeconds(interval);
            }

            if (brake)
                drive.SetPowers(0, 0);
        }

        /// <summary>
        /// Drives the robot at a set speed for a set distance
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="speedController"></param>
        /// <param name="speed"></param>
        /// <param name="distance"></param>
        /// <param name="brake"></param>
        /// <param name="interval"></param>
        public static void DriveForAtSpeed(this IEncoderDrive drive, IMotionController speedController, double speed, double distance, bool brake, double interval = 0.02)
        {
            drive.DriveToAtSpeed(speedController, speed, distance + drive.Encoders.LinearDistance, brake, interval);
        }

        /// <summary>
        /// Drives according to a function given enoder distances
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="expression">Returns: done driving | Arg1: Left Encoder Distance | Arg2: Right Encoder Distance</param>
        public static void DynamicDistanceDrive(this IEncoderDrive drive, Func<double, double, bool> expression, double interval = 0.02)
        {
            while (!expression(drive.Encoders.LeftDistance, drive.Encoders.RightDistance))
                AccurateWaitSeconds(interval);
        }

        /// <summary>
        /// Drives according to a function given the encoder speeds
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="expression">Returns: done driving | Arg1: left encoder velocity | Arg2: right encoder velocity</param>
        public static void DynamicSpeedDrive(this IEncoderDrive drive, Func<double, double, bool> expression, double interval = 0.02)
        {
            while (!expression(drive.Encoders.LeftSpeed, drive.Encoders.RightSpeed))
                AccurateWaitSeconds(interval);
        }

        /// <summary>
        /// Drives according to a function given the drive encoders
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="expression">Returns: done driving | Arg1: drive encoders</param>
        /// <param name="interval"></param>
        public static void DynamicEncoderDrive(this IEncoderDrive drive, Func<DriveEncoders, bool> expression, double interval = 0.02)
        {
            while (!expression(drive.Encoders))
                AccurateWaitSeconds(interval);
        }

        /// <summary>
        /// Drives according to a function given the drive encoders
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="expression">Returns: done driving | Arg1: Left Encoder | Arg2:Right Encoder</param>
        /// <param name="interval"></param>
        public static void DynamicEncoderDrive(this IEncoderDrive drive, Func<Encoder, Encoder, bool> expression, double interval = 0.02)
        {
            while (!expression(drive.Encoders.Left, drive.Encoders.Right))
                AccurateWaitSeconds(interval);
        }
    }
}
