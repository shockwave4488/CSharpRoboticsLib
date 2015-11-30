using System;
using WPILib.Interfaces;

namespace CSharpRoboticsLib.Extras
{
    /// <summary>
    /// A subclass of a motor controller intended to allow controlled change in power, for safety or voltage regulation.
    /// </summary>
    /// <typeparam name="T">Type of motor controller</typeparam>
    public class RampMotor<T> : ISpeedController where T : ISpeedController
    {
        private ISpeedController m_controller;
        private double m_power;

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
            get { return m_controller.Inverted; }
            set { m_controller.Inverted = value; }
        }

        /// <summary>
        /// Opens a new RampingMotor 
        /// </summary>
        /// <param name="channel">The PWM channel that the motor is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public RampMotor(int port)
        {
            m_controller = (ISpeedController)Activator.CreateInstance(typeof(T), port);
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
            double delta = Math.Sign(value) == Math.Sign(m_power) && Math.Abs(value) > Math.Abs(m_power) ? MaxAccel : MaxDecel;

            if (value > m_power + delta) //If the motor wants to change power faster than it is allowed, change it by the max power change allowed
                m_power += delta;
            else if (value < m_power - delta)
                m_power -= delta;
            else //If the motor wants to go to a power within the change limitations, set the power to the value.
                m_power = value;

            m_controller.Set(value);
        }

        /// <summary>
        /// Set the power of a motor regardless of the ramping limitations
        /// </summary>
        /// <param name="value">Power to set to</param>
        public void ForcePower(double value)
        {
            m_power = value;
        }

        /// <summary>
        /// Returns the last value set to this controller
        /// </summary>
        /// <returns></returns>
        public double Get()
        {
            return m_controller.Get();
        }

        /// <summary>
        /// Sets the output value for this controller
        /// </summary>
        /// <param name="value"></param>
        /// <param name="syncGroup"></param>
        public void Set(double value, byte syncGroup)
        {
            m_controller.Set(value, syncGroup);
        }

        /// <summary>
        /// Disable the speed controller
        /// </summary>
        public void Disable()
        {
            m_controller.Disable();
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
            m_controller.Dispose();
        }
    }
}
