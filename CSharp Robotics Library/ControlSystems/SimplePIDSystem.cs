using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib;
using WPILib.Interfaces;

namespace CSharp_Robotics_Library.ControlSystems
{
    /// <summary>
    /// Extendable controller for a PID Controlled mechanism.
    /// Defines No Constructors.
    /// </summary>
    public abstract class SimplePIDSystem
    {
        /// <summary>
        /// The PID Controller
        /// </summary>
        protected SimplePID PID;

        /// <summary>
        /// Sensor to interact with the PID Controller
        /// </summary>
        protected IPIDSource sensor;
        
        /// <summary>
        /// Motor Controller to be controlled by the PID Controller.
        /// </summary>
        protected ISpeedController Motor;

        /// <summary>
        /// Positional tolerance of the setpoint, used in the AtSetPoint property.
        /// </summary>
        protected double setpointTolerance;

        /// <summary>
        /// Set the system into a manual mode of operation
        /// </summary>
        public bool Manual { get; set; }
        
        /// <summary>
        /// Returns true if the sensor is at the setpoint within the specified tolerance
        /// </summary>
        public bool AtSetpoint
        {
            get
            {
                return (sensor.PidGet() < PID.setpoint + setpointTolerance) && (sensor.PidGet() > PID.setpoint - setpointTolerance);
            }
        }

        /// <summary>
        /// Sets or returns the setpoint of the PID Controller
        /// </summary>
        public virtual double SetPoint
        {
            get { return PID.setpoint; }
            set { PID.setpoint = value; }
        }

        /// <summary>
        /// Updates the system based on the setpoint and PID Controller. If the system is in Manual, sets the motor to 0.
        /// </summary>
        public virtual void Update()
        {
            if (!Manual)
                Motor.Set(PID.get(sensor.PidGet()));
            else
                Motor.Set(0);

            PID.Update(sensor.PidGet());
        }

        /// <summary>
        /// Updates the system based on the setpoint and PID Controller. If the system is in manual, Sets the motor to the argument.
        /// </summary>
        /// <param name="manual">Motor value if Manual is true</param>
        public virtual void Update(double manual)
        {
            Update();
            if (Manual)
                Motor.Set(manual);
        }
    }
}
