﻿using System;
using System.Diagnostics;
using static CSharpRoboticsLib.Utility.Util;

namespace CSharpRoboticsLib.Drive.Interfaces
{
    /// <summary>
    /// Defines functions for a basic tank drive.
    /// No sensors, just left and right motors.
    /// </summary>
    public interface ITankDrive
    {
        /// <summary>
        /// Sets the powers of the Left and Right Motors. Should negate or invert one side.
        /// </summary>
        /// <param name="leftPower">power the Left Motor is set to</param>
        /// <param name="rightPower">power the Right Motor is set to</param>
        void SetPowers(double leftPower, double rightPower);
    }

    /// <summary>
    /// Provides extentions for <see cref="ITankDrive"/>. 
    /// These methods do not need to be called statically.
    /// You will never need to reference this class.
    /// </summary>
    public static class TankDriveExtensions
    {
        /// <summary>
        /// Drives at a speed for a set time
        /// </summary>
        /// <param name="d"></param>
        /// <param name="power"></param>
        /// <param name="time"></param>
        /// <param name="brake">Set motors to zero when this is finished?</param>
        public static void StraightForTime(this ITankDrive d, double power, double time, bool brake, double interval = 0.02)
        {
            d.DriveForTime(power, power, time, brake, interval);
        }

        /// <summary>
        /// Turns the robot in place for a set time
        /// </summary>
        /// <param name="d"></param>
        /// <param name="power"></param>
        /// <param name="time"></param>
        /// <param name="brake">Set motors to zero when this is finished?</param>
        public static void TurnForTime(this ITankDrive d, double power, double time, bool brake, double interval = 0.02)
        {
            d.DriveForTime(power, -power, time, brake, interval);
        }

        /// <summary>
        /// Drives both motors at a set speed for a set time
        /// </summary>
        /// <param name="d"></param>
        /// <param name="lPower"></param>
        /// <param name="rPower"></param>
        /// <param name="time"></param>
        /// <param name="brake">Set motors to zero when this is finished?</param>
        public static void DriveForTime(this ITankDrive d, double lPower, double rPower, double time, bool brake, double interval = 0.02)
        {
            Stopwatch s = new Stopwatch();
            s.Restart();

            while (s.Elapsed.TotalSeconds < time)
            {
                d.SetPowers(lPower, rPower);
                AccurateWaitSeconds(interval);
            }

            if(brake)
                d.SetPowers(0, 0);
        }

        /// <summary>
        /// Drives accordingly to an action
        /// </summary>
        /// <param name="d"></param>
        /// <param name="expression">Parameter: Time elapsed</param>
        /// <param name="time"></param>
        public static void DynamicDriveForTime(this ITankDrive d, Action<double> expression, double time, double interval = 0.02)
        {
            Stopwatch s = new Stopwatch();
            s.Restart();

            while (s.Elapsed.TotalSeconds < time)
            {
                expression(s.Elapsed.TotalSeconds);
                AccurateWaitSeconds(interval);
            }
        }

        /// <summary>
        /// Drives according to a function given the time
        /// </summary>
        /// <param name="d"></param>
        /// <param name="expression">Returns: done driving | Arg1: Time elapsed</param>
        /// <param name="interval"></param>
        public static void DynamicDriveForTime(this ITankDrive d, Func<double, bool> expression, double interval = 0.02)
        {
            Stopwatch s = new Stopwatch();
            s.Restart();

            while (!expression(s.Elapsed.TotalSeconds))
            {
                AccurateWaitSeconds(interval);
            }
        }
    }
}
