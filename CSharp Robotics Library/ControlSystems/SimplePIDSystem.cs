using WPILib.Interfaces;

namespace CSharpRoboticsLib.ControlSystems
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
        protected ISpeedController motor;

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
                return (sensor.PidGet() < PID.SetPoint + setpointTolerance) && (sensor.PidGet() > PID.SetPoint - setpointTolerance);
            }
        }

        /// <summary>
        /// Sets or returns the setpoint of the PID Controller
        /// </summary>
        public virtual double SetPoint
        {
            get { return PID.SetPoint; }
            set { PID.SetPoint = value; }
        }

        /// <summary>
        /// Updates the system based on the setpoint and PID Controller. If the system is in Manual, sets the motor to 0.
        /// </summary>
        public virtual void Update()
        {
            if (!Manual)
                motor.Set(PID.Get(sensor.PidGet()));
            else
                motor.Set(0);

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
                motor.Set(manual);
        }
    }
}
