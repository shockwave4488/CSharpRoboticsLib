using WPILib;
using WPILib.Interfaces;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// Extendable controller for a <see cref="IMotionController"/> Controlled mechanism.
    /// Defines No Constructors.
    /// </summary>
    public abstract class MotionControlledSystem
    {
        /// <summary>
        /// Notifier to update the <see cref="MotionControlledSystem"/> periodically
        /// </summary>
        protected Notifier Periodic;

        /// <summary>
        /// The <see cref="IMotionController"/> for the system to follow
        /// </summary>
        protected IMotionController Controller;

        /// <summary>
        /// The <see cref="IPIDSource"/> for the <see cref="IMotionController"/> to react to
        /// </summary>
        protected IPIDSource Sensor;
        
        /// <summary>
        /// The <see cref="ISpeedController"/> to be controlled by the <see cref="IMotionController"/>
        /// </summary>
        protected ISpeedController Motor;

        /// <summary>
        /// Positional tolerance of the <see cref="SetPoint"/>, used in the AtSetPoint property.
        /// </summary>
        protected double SetpointTolerance;

        /// <summary>
        /// Set the <see cref="MotionControlledSystem"/> into manual mode of operation
        /// </summary>
        public bool Manual { get; set; }

        /// <summary>
        /// Te power to be used if <see cref="Manual"/> is enabled
        /// </summary>
        public double ManualPower { get; set; }
        
        /// <summary>
        /// Returns true if the <see cref="Sensor"/> is at the setpoint within the specified tolerance
        /// </summary>
        public bool AtSetpoint => (Sensor.PidGet() < Controller.SetPoint + SetpointTolerance) && (Sensor.PidGet() > Controller.SetPoint - SetpointTolerance);

        /// <summary>
        /// Sets or returns the setpoint of the <see cref="Controller"/>
        /// </summary>
        public virtual double SetPoint
        {
            get { return Controller.SetPoint; }
            set { Controller.SetPoint = value; }
        }

        /// <summary>
        /// Updates the system based on <see cref="SetPoint"/>. If the system is in Manual, sets the motor to 0.
        /// </summary>
        public virtual void Update()
        {
            Motor.Set(Manual ? ManualPower : Controller.Get(Sensor.PidGet()));
        }

        /// <summary>
        /// Starts running <see cref="Update"/> periodically every <paramref name="period"/> seconds
        /// </summary>
        /// <param name="period"></param>
        public void Start(double period)
        {
            Periodic.StartPeriodic(period);
        }

        /// <summary>
        /// Stops running the <see cref="Update"/> periodically
        /// </summary>
        public void Stop()
        {
            Periodic.Stop();
        }
    }
}
