using WPILib.Interfaces;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// Extendable controller for a Motion Controlled mechanism.
    /// Defines No Constructors.
    /// </summary>
    public abstract class SimpleMotionControlledSystem
    {
        /// <summary>
        /// The Motion Controller
        /// </summary>
        protected IMotionController Motion;

        /// <summary>
        /// Sensor to interact with the Motion Controller
        /// </summary>
        protected IPIDSource Sensor;
        
        /// <summary>
        /// Motor Controller to be controlled by the Motion Controller.
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
        public bool AtSetpoint => (Sensor.PidGet() < Motion.SetPoint + SetpointTolerance) && (Sensor.PidGet() > Motion.SetPoint - SetpointTolerance);

        /// <summary>
        /// Sets or returns the setpoint of the Motion Controller
        /// </summary>
        public virtual double SetPoint
        {
            get { return Motion.SetPoint; }
            set { Motion.SetPoint = value; }
        }

        /// <summary>
        /// Updates the system based on the setpoint and Motion Controller. If the system is in Manual, sets the motor to 0.
        /// </summary>
        public virtual void Update()
        {
            Update(0);
        }

        /// <summary>
        /// Updates the system based on the setpoint and Motion Controller. If the system is in manual, Sets the motor to the argument.
        /// </summary>
        /// <param name="manual">Motor value if Manual is true</param>
        public virtual void Update(double manual)
        {
            Motor.Set(Manual ? manual : Motion.Get(Sensor.PidGet()));
        }
    }
}
