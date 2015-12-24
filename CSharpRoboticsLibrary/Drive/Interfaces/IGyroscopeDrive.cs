using System;
using System.Diagnostics;
using CSharpRoboticsLib.ControlSystems;
using WPILib;
using static CSharpRoboticsLib.Utility.Utility;

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
        GyroBase Gyroscope { get; }
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
        /// <param name="brake"></param>
        public static void TurnForAngle(this IGyroscopeDrive d, double power, double angle, bool brake, double interval = 0.02)
        {
            d.TurnToAngle(power, angle + d.Gyroscope.GetAngle(), brake, interval);
        }

        /// <summary>
        /// Turns the robot to an absolute angle as reported by the gyroscope.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="power"></param>
        /// <param name="angle"></param>
        /// <param name="brake"></param>
        public static void TurnToAngle(this IGyroscopeDrive d, double power, double angle, bool brake, double interval = 0.02)
        {
            int direction = 1;
            if (d.Gyroscope.GetAngle() < angle)
                direction = -1;

            while ((d.Gyroscope.GetAngle() - angle)*direction > 0)
            {
                d.SetPowers(power*direction, -power*direction);
                AccurateWaitSeconds(interval);
            }

            if(brake)
              d.SetPowers(0, 0);
        }

        /// <summary>
        /// Turns the robot to an absolute angle using a <see cref="SimplePID"/>
        /// </summary>
        /// <param name="d"></param>
        /// <param name="controller"></param>
        /// <param name="angle"></param>
        /// <param name="tolerance"></param>
        /// <param name="brake"></param>
        public static void TurnToAngle(this IGyroscopeDrive d, IMotionController controller, double angle, double tolerance, bool brake, double interval = 0.02)
        {
            controller.SetPoint = angle;

            while (Math.Abs(d.Gyroscope.GetAngle() - angle) > tolerance)
            {
                double power = controller.Get(d.Gyroscope.GetAngle());
                d.SetPowers(power, -power);
                AccurateWaitSeconds(interval);
            }

            if(brake)
               d.SetPowers(0, 0);
        }

        /// <summary>
        /// Turns the robot for a relative angle amount using a <see cref="IMotionController"/>
        /// </summary>
        /// <param name="d"></param>
        /// <param name="controller"></param>
        /// <param name="angle"></param>
        /// <param name="tolerance"></param>
        public static void TurnForAngle(this IGyroscopeDrive d, IMotionController controller, double angle, double tolerance, bool brake, double interval = 0.02)
        {
            d.TurnToAngle(controller, d.Gyroscope.GetAngle() + angle, tolerance, brake, interval);
        }

        /// <summary>
        /// Drives the robot in a straight line for a set time using a <see cref="IMotionController"/> to correct heading
        /// </summary>
        /// <param name="d"></param>
        /// <param name="correction">Motion Controller to use for correcting heading</param>
        /// <param name="power"></param>
        /// <param name="time"></param>
        /// <param name="brake"></param>
        public static void DriveStraightForTime(this IGyroscopeDrive d, IMotionController correction, double power, double time, bool brake, double interval = 0.02)
        {
            correction.SetPoint = d.Gyroscope.GetAngle();
            Stopwatch s = new Stopwatch();

            while (s.Elapsed.TotalSeconds < time)
            {
                d.SetPowers(power + correction.Get(d.Gyroscope.GetAngle()), power - correction.Get(d.Gyroscope.GetAngle()));
                AccurateWaitSeconds(interval);
            }

            if(brake)
                d.SetPowers(0, 0);
        }

        public static void DynamicGyroscopeDrive(this IGyroscopeDrive d, Func<double, bool> expression, double interval = 0.02)
        {
            while (!expression(d.Gyroscope.GetAngle()))
                AccurateWaitSeconds(interval);
        }

        public static void DynamicGyroscopeDrive(this IGyroscopeDrive d, Func<GyroBase, bool> expression,
            double interval = 0.02)
        {
            while (!expression(d.Gyroscope))
                AccurateWaitSeconds(interval);
        }
    }
}
