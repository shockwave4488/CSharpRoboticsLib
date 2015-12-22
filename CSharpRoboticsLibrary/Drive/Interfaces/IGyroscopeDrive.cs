using System;
using System.Diagnostics;
using CSharpRoboticsLib.ControlSystems;
using WPILib;

namespace CSharpRoboticsLib.Drive.Interfaces
{
    /// <summary>
    /// Defines functions for a Tank Drive with a gyroscope.
    /// If your robot has both encoders and a gyroscope, use <see cref="ISensorDrive"/>
    /// </summary>
    public interface IGyroscopeDrive : ITankDrive
    {
        /// <summary>
        /// Returns the gyroscope reference
        /// </summary>
        /// <returns>Robot's Gyroscope</returns>
        AnalogGyro Gyroscope { get; }
    }

    /// <summary>
    /// Provides extentions for <see cref="IGyroscopeDrive"/>. 
    /// These methods do not need to be called statically.
    /// You will never need to reference this class.
    /// </summary>
    public static class GyroscopeDriveExtensions
    {
        /// <summary>
        /// Turns the robot at a set speed for a relative angle amount
        /// </summary>
        /// <param name="d"></param>
        /// <param name="power"></param>
        /// <param name="angle"></param>
        public static void TurnForAngle(this IGyroscopeDrive d, double power, double angle, bool brake)
        {
            d.TurnToAngle(power, angle + d.Gyroscope.GetAngle(), brake);
        }

        /// <summary>
        /// Turns the robot to an absolute angle as reported by the gyroscope.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="power"></param>
        /// <param name="angle"></param>
        public static void TurnToAngle(this IGyroscopeDrive d, double power, double angle, bool brake)
        {
            int direction = 1;
            if (d.Gyroscope.GetAngle() < angle)
                direction = -1;

            while ((d.Gyroscope.GetAngle() - angle)*direction > 0)
            {
                d.SetPowers(power*direction, -power*direction);
            }

            if(brake)
              d.SetPowers(0, 0);
        }

        /// <summary>
        /// Turns the robot to an absolute angle using a <see cref="SimplePID"/>
        /// </summary>
        /// <param name="d"></param>
        /// <param name="motion"></param>
        /// <param name="angle"></param>
        /// <param name="tolerance"></param>
        public static void TurnToAngle(this IGyroscopeDrive d, IMotionController motion, double angle, double tolerance, bool brake)
        {
            motion.SetPoint = angle;

            while (Math.Abs(d.Gyroscope.GetAngle() - angle) > tolerance)
            {
                double power = motion.Get(d.Gyroscope.GetAngle());
                d.SetPowers(power, -power);
            }

            if(brake)
               d.SetPowers(0, 0);
        }

        /// <summary>
        /// Turns the robot for a relative angle amount using a <see cref="SimplePID"/>
        /// </summary>
        /// <param name="d"></param>
        /// <param name="motion"></param>
        /// <param name="angle"></param>
        /// <param name="tolerance"></param>
        public static void TurnForAngle(this IGyroscopeDrive d, IMotionController motion, double angle, double tolerance, bool brake)
        {
            d.TurnToAngle(motion, d.Gyroscope.GetAngle() + angle, tolerance, brake);
        }
        
        /// <summary>
        /// Drives the robot in a straight line for a set time
        /// </summary>
        /// <param name="d"></param>
        /// <param name="motion">Motion Controller to use for correcting heading</param>
        /// <param name="power"></param>
        /// <param name="time"></param>
        public static void DriveStraightForTime(this IGyroscopeDrive d, IMotionController motion, double power, double time, bool brake)
        {
            motion.SetPoint = d.Gyroscope.GetAngle();
            Stopwatch s = new Stopwatch();

            while (s.Elapsed.TotalSeconds < time)
            {
                d.SetPowers(power + motion.Get(d.Gyroscope.GetAngle()), power - motion.Get(d.Gyroscope.GetAngle()));
            }

            if(brake)
                d.SetPowers(0, 0);
        }
    }
}
