using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoboticsLib.Autonomous.Drive.Interfaces
{
    /// <summary>
    /// Defines functions for a basic tank drive.
    /// No sensors, just left and right motors.
    /// </summary>
    public interface ITankDrive
    {
        /// <summary>
        /// Sets the powers of the Left and Right Motors.
        /// </summary>
        /// <param name="LeftPower">power the Left Motor is set to</param>
        /// <param name="RightPower">power the Left Motor is set to</param>
        void SetPowers(double LeftPower, double RightPower);
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
        //I have not tried these methods at run time, but it seems to compile just fine.

        /// <summary>
        /// Drives at a speed for a set time
        /// </summary>
        /// <param name="d"></param>
        /// <param name="time"></param>
        public static void DriveForTime(this ITankDrive d, double time)
        {
            Derived thing = new Derived();
            thing.DriveForTime(1);
        }
    }

    public class Base { }
    public class Derived : Base, ITankDrive
    {
        public void SetPowers(double LeftPower, double RightPower)
        {
            
        }
    }
}
