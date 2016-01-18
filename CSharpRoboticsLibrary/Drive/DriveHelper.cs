using System;
using CSharpRoboticsLib.Drive.Interfaces;
using WPILib;

namespace CSharpRoboticsLib.Drive
{
    //Shamelessly copy-pasted from Team 254©'s Robot Code©

    /// <summary>
    /// Does all calculations for driving the robot split-arcade style. Contains an <see cref="IEncoderDrive"/> object.
    /// Does not handle shifting.
    /// </summary>
    public class DriveHelper
    {
        private ITankDrive m_drive;
        private double m_oldTurn, m_quickStopAccumulator;
        private double m_speedDeadzone, m_turnDeadzone;
        private double m_highNonLinearity, m_lowNonLinearity;
        private double m_highSensitivity, m_lowSensitivity;
        
        /// <summary>
        /// Initializes a new <see cref="DriveHelper"/>
        /// </summary>
        /// <param name="drive"><see cref="ITankDrive"/> to control</param>
        /// <param name="speedDeadzone">deadband value for the speed axis</param>
        /// <param name="turnDeadzone">deadband value for the turn axis</param>
        /// <param name="highNonLinearity">Non-linearity for the turn input in high gear</param>
        /// <param name="lowNonLinearity">Non-linearity for the turn input in low gear</param>
        /// <param name="highSensitivity">turn sensitivity in high gear</param>
        /// <param name="lowSensitivity">turn sensitivity in low gear</param>
        public DriveHelper(ITankDrive drive, double speedDeadzone, double turnDeadzone, double highNonLinearity, double lowNonLinearity, double highSensitivity, double lowSensitivity)
        {
            m_drive = drive;
            m_oldTurn = m_quickStopAccumulator = 0;
            m_speedDeadzone = speedDeadzone;
            m_turnDeadzone = turnDeadzone;
            m_highNonLinearity = highNonLinearity;
            m_lowNonLinearity = lowNonLinearity;
            m_highSensitivity = highSensitivity;
            m_lowSensitivity = lowSensitivity;
        }

        /// <summary>
        /// the contained <see cref="ITankDrive"/>
        /// </summary>
        public ITankDrive DriveTrain => m_drive;

        /// <summary>
        /// Does all calculations and subroutines for driving the robot split-arcade style. 
        /// </summary>
        /// <param name="speed">Linear Control</param>
        /// <param name="turn">Rotational Control</param>
        /// <param name="isQuickTurn">Allows less smooth, but faster turning. Will likely be turned on in low gear.</param>
        /// <param name="isHighGear">Is the robot in High Gear? (requires different sensitivity values)</param>
        public void Drive(double speed, double turn, bool isQuickTurn, bool isHighGear)
        {
            if (DriverStation.Instance.Autonomous)
            {
                return;
            }

            double wheelNonLinearity;

            turn = HandleDeadband(turn, m_speedDeadzone);
            speed = HandleDeadband(speed, m_turnDeadzone);

            //double negInertia = turn - m_oldTurn;
            //m_oldTurn = turn;
            
            //If throttle == 0 and quickturn is off, things get weird. Trust me, I tried.
            isQuickTurn |= speed == 0;

            if (isHighGear)
            {
                wheelNonLinearity = m_highNonLinearity;
                // Apply a sin function that's scaled to make it feel better.
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
            }
            else
            {
                wheelNonLinearity = m_lowNonLinearity;
                // Apply a sin function that's scaled to make it feel better.
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
                turn = Math.Sin(Math.PI / 2.0 * wheelNonLinearity * turn)
                        / Math.Sin(Math.PI / 2.0 * wheelNonLinearity);
            }

            double leftPwm, rightPwm, overPower;
            double sensitivity;

            double angularPower;
            double linearPower;

            // Negative inertia!
            // Currently not enabled
            //double negInertiaScalar;
            if (isHighGear)
            {
                //negInertiaScalar = 5.0;
                sensitivity = m_highSensitivity;
            }
            else
            {
                /*
                if (turn * negInertia > 0)
                {
                    //negInertiaScalar = 2.5;
                }
                else
                {
                    if (Math.Abs(turn) > 0.65)
                    {
                        //negInertiaScalar = 5.0;
                    }
                    else
                    {
                        //negInertiaScalar = 3.0;
                    }
                }
                */
                sensitivity = m_lowSensitivity;
            }
            //double negInertiaPower = negInertia * negInertiaScalar;
            //m_negInertiaAccumulator += negInertiaPower;
            /*
            turn = turn + m_negInertiaAccumulator;
            if (m_negInertiaAccumulator > 1)
            {
                m_negInertiaAccumulator -= 1;
            }
            else if (m_negInertiaAccumulator < -1)
            {
                m_negInertiaAccumulator += 1;
            }
            else
            {
                m_negInertiaAccumulator = 0;
            }
            */

            linearPower = speed;

            // Quickturn!
            if (isQuickTurn)
            {
                if (Math.Abs(linearPower) < 0.2)
                {
                    double alpha = 0.1;
                    m_quickStopAccumulator = (1 - alpha) * m_quickStopAccumulator + alpha * ((Math.Abs(turn) < 1.0) ? turn : Math.Sign(turn)) * 5;
                }
                overPower = 1.0;
                angularPower = turn;
            }
            else
            {
                overPower = 0.0;
                angularPower = Math.Abs(speed) * turn * sensitivity - m_quickStopAccumulator;
                if (m_quickStopAccumulator > 1)
                {
                    m_quickStopAccumulator -= 1;
                }
                else if (m_quickStopAccumulator < -1)
                {
                    m_quickStopAccumulator += 1;
                }
                else
                {
                    m_quickStopAccumulator = 0.0;
                }
            }

            rightPwm = leftPwm = linearPower;
            leftPwm += angularPower;
            rightPwm -= angularPower;

            if (leftPwm > 1.0)
            {
                rightPwm -= overPower * (leftPwm - 1.0);
                leftPwm = 1.0;
            }
            else if (rightPwm > 1.0)
            {
                leftPwm -= overPower * (rightPwm - 1.0);
                rightPwm = 1.0;
            }
            else if (leftPwm < -1.0)
            {
                rightPwm += overPower * (-1.0 - leftPwm);
                leftPwm = -1.0;
            }
            else if (rightPwm < -1.0)
            {
                leftPwm += overPower * (-1.0 - rightPwm);
                rightPwm = -1.0;
            }

            m_drive.SetPowers(leftPwm, rightPwm);
        }
        
        /// <summary>
        /// Handles the deadband in the controllers. For Xbox controllers, the deadband on the joysticks is about 0.2
        /// </summary>
        /// <param name="val">Input Value</param>
        /// <param name="deadband">deadband</param>
        /// <returns>If value is within deadband, 0, Otherwise value</returns>
        private static double HandleDeadband(double val, double deadband)
        {
            return (Math.Abs(val) > Math.Abs(deadband)) ? val : 0.0;
        }
    }
}
