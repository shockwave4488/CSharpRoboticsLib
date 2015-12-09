﻿using System.Diagnostics;

namespace CSharpRoboticsLib.Autonomous.Drive
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
        //I have successfully circumvented the limitation that all interface functions
        //must be abstract, allowing for some aspects of multiple inheritance.

        /// <summary>
        /// Drives at a speed for a set time
        /// </summary>
        /// <param name="d"></param>
        /// <param name="power"></param>
        /// <param name="time"></param>
        public static void StraightForTime(this ITankDrive d, double power, double time)
        {
            d.DriveForTime(power, power, time);
        }

        /// <summary>
        /// Turns the robot in place for a set time
        /// </summary>
        /// <param name="d"></param>
        /// <param name="power"></param>
        /// <param name="time"></param>
        public static void TurnForTime(this ITankDrive d, double power, double time)
        {
            d.DriveForTime(power, -power, time);
        }

        /// <summary>
        /// Drives both motors at a set speed for a set time
        /// </summary>
        /// <param name="d"></param>
        /// <param name="lPower"></param>
        /// <param name="rPower"></param>
        /// <param name="time"></param>
        public static void DriveForTime(this ITankDrive d, double lPower, double rPower, double time)
        {
            Stopwatch s = new Stopwatch();
            s.Restart();

            while (s.Elapsed.TotalSeconds < time)
            {
                d.SetPowers(lPower, rPower);
            }

            d.SetPowers(0, 0);
        }
    }
}
