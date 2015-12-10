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
        protected IPIDController Pid;

        /// <summary>
        /// Sensor to interact with the PID Controller
        /// </summary>
        protected IPIDSource Sensor;
        
        /// <summary>
        /// Motor Controller to be controlled by the PID Controller.
        /// </summary>
        protected ISpeedController Motor;

        /// <summary>
        /// Positional tolerance of the setpoint, used in the AtSetPoint property.
        /// </summary>
        protected double SetpointTolerance;

        /// <summary>
        /// Set the system into a manual mode of operation
        /// </summary>
        public bool Manual { get; set; }
        
        /// <summary>
        /// Returns true if the sensor is at the setpoint within the specified tolerance
        /// </summary>
        public bool AtSetpoint => (Sensor.PidGet() < Pid.SetPoint + SetpointTolerance) && (Sensor.PidGet() > Pid.SetPoint - SetpointTolerance);

        /// <summary>
        /// Sets or returns the setpoint of the PID Controller
        /// </summary>
        public virtual double SetPoint
        {
            get { return Pid.SetPoint; }
            set { Pid.SetPoint = value; }
        }

        /// <summary>
        /// Updates the system based on the setpoint and PID Controller. If the system is in Manual, sets the motor to 0.
        /// </summary>
        public virtual void Update()
        {
            Update(0);
        }

        /// <summary>
        /// Updates the system based on the setpoint and PID Controller. If the system is in manual, Sets the motor to the argument.
        /// </summary>
        /// <param name="manual">Motor value if Manual is true</param>
        public virtual void Update(double manual)
        {
            Motor.Set(Manual ? manual : Pid.Get(Sensor.PidGet()));
        }
    }
}
