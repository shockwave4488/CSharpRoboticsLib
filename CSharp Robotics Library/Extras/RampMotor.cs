using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using WPILib.Interfaces;

namespace CSharp_Robotics_Library.Extras
{
    /// <summary>
    /// A subclass of a motor controller intended to allow controlled change in power, for safety or voltage regulation means.
    /// </summary>
    /// <typeparam name="T">Type of motor controller</typeparam>
    public class RampMotor<T> : ISpeedController where T : ISpeedController
    {
        ISpeedController controller;
        private double power;

        /// <summary>
        /// The max amount of change in power the motor can accelerate with
        /// </summary>
        public double MaxAccel { get; set; }

        /// <summary>
        /// The max amount of change in power the motor can decellerate with
        /// </summary>
        public double MaxDecel { get; set; }

        /// <summary>
        /// Sets both MaxAccel and MaxDecel
        /// </summary>
        public double MaxChange
        {
            set { MaxAccel = value; MaxDecel = value; }
        }

        /// <summary>
        /// inverts the direction of the motor rotation.
        /// </summary>
        public bool Inverted
        {
            get { return controller.Inverted; }
            set { controller.Inverted = value; }
        }

        /// <summary>
        /// Opens a new RampingMotor 
        /// </summary>
        /// <param name="channel">The PWM channel that the motor is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public RampMotor(int port)
        {
            controller = (ISpeedController)Activator.CreateInstance(typeof(T), port);
        }

        /// <summary>
        /// Sets the value to the motor, within the change limitations
        /// </summary>
        /// <param name="value">value to set to or approach</param>
        public void Set(double value)
        {
            //The motor is DECELLERATING if |value| < |power| OR value and power are not both positive or both negative
            //Likewise, the motor is ACCELERATING if |value| > |power| AND value and power are both negative or both positive
            //if the motor is ACCELERATING, use MaxAccel. If the robot is DECELLERATING, use MaxDecel.
            double delta = Math.Sign(value) == Math.Sign(power) && Math.Abs(value) > Math.Abs(power) ? MaxAccel : MaxDecel;

            if (value > power + delta) //If the motor wants to change power faster than it is allowed, change it by the max power change allowed
                power += delta;
            else if (value < power - delta)
                power -= delta;
            else //If the motor wants to go to a power within the change limitations, set the power to the value.
                power = value;

            controller.Set(value);
        }

        /// <summary>
        /// Set the power of a motor regardless of the ramping limitations
        /// </summary>
        /// <param name="value">Power to set to</param>
        public void ForcePower(double value)
        {
            power = value;
        }

        /// <summary>
        /// returns the last value of this controller
        /// </summary>
        /// <returns></returns>
        public double Get()
        {
            return controller.Get();
        }

        /// <summary>
        /// sets the output value of the controller
        /// </summary>
        /// <param name="value"></param>
        /// <param name="syncGroup"></param>
        public void Set(double value, byte syncGroup)
        {
            controller.Set(value, syncGroup);
        }

        /// <summary>
        /// Disable the speed controller
        /// </summary>
        public void Disable()
        {
            controller.Disable();
        }

        /// <summary>
        /// Set the output to the value calculated by the PIDController
        /// </summary>
        /// <param name="value"></param>
        public void PidWrite(double value)
        {
            Set(value);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose()
        {
            controller.Dispose();
        }
    }
}
