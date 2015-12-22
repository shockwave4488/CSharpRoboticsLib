using System;
using WPILib.Interfaces;

namespace CSharpRoboticsLib.WPIExtensions
{
    /// <summary>
    /// A subclass of a motor controller intended to allow controlled change in power, for safety or voltage regulation.
    /// </summary>
    public class RampMotor : ISpeedController 
    {
        private ISpeedController m_controller;
        private double m_power;
        private double m_maxAccel, m_maxDecel;

        /// <summary>
        /// The max amount of change in power the motor can accelerate with
        /// </summary>
        public double MaxAccel
        {
            get { return m_maxAccel; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MaxAccel must be greater than zero. Value given: " + value);
                m_maxAccel = value;
            }
        }

        /// <summary>
        /// The max amount of change in power the motor can decellerate with
        /// </summary>
        public double MaxDecel
        {
            get { return m_maxDecel; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("MaxDecel must be greater than zero. Value given: " + value);
                m_maxDecel = value;
            }
        }

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
        /// Opens a new RampingMotor of a specific motor type
        /// </summary>
        /// <param name="controllerType">Type of motor controller (Jaguar, Victor, Spark, CAN Talon, etc.)</param>
        /// <param name="port">The PWM channel that the motor is attached to. 0-9 are on-board, 10-19 are on the MXP port</param>
        public RampMotor(Type controllerType, int port) : this((ISpeedController)Activator.CreateInstance(controllerType, port))
        {

        }

        /// <summary>
        /// Wraps an existing <see cref="ISpeedController"/> with <see cref="RampMotor{T}"/>
        /// </summary>
        /// <param name="motorController">existing Motor Controller</param>
        public RampMotor(ISpeedController motorController)
        {
            m_controller = motorController;
            MaxChange = 1;
            m_power = 0;
        }

        /// <summary>
        /// Sets the value to the motor, within the change limitations
        /// </summary>
        /// <param name="value">value to set to or approach</param>
        public void Set(double value)
        {
            //The motor is DECELLERATING if |value| < |power| OR value and power are not both positive or both negative
            //Likewise, the motor is ACCELERATING if |value| > |power| AND value and power are both negative or both positive
            //If power is zero, the motor is accelerating.
            //if the motor is ACCELERATING, use MaxAccel. If the robot is DECELLERATING, use MaxDecel.
            bool accel = (Math.Sign(value) == Math.Sign(m_power) && Math.Abs(value) > Math.Abs(m_power)) ||
                         Math.Abs(m_power) < MaxAccel;
            double delta = accel ? MaxAccel : MaxDecel;

            if (value > m_power + delta) //If the motor wants to change power faster than it is allowed, change it by the max power change allowed
                m_power += delta;
            else if (value < m_power - delta)
                m_power -= delta;
            else //If the motor wants to go to a power within the change limitations, set the power to the value.
                m_power = value;

            m_controller.Set(m_power);
        }

        /// <summary>
        /// Set the power of a motor regardless of the ramping limitations
        /// </summary>
        /// <param name="value">Power to set to</param>
        public void ForcePower(double value)
        {
            m_power = value;
            m_controller.Set(value);
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
