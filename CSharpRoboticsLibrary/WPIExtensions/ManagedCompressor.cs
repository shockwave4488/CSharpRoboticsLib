using WPILib;

namespace CSharpRoboticsLib.WPIExtensions
{
    /// <summary>
    /// A Compressor which will not run if the voltage is too low.
    /// </summary>
    public class ManagedCompressor : Compressor
    {
        private Notifier m_periodic;
        private bool m_usingTimer;
        private readonly double m_period;

        /// <summary>
        /// If the <see cref="ManagedCompressor"/> is being updated automatically and periodically
        /// </summary>
        public bool UseTimer
        {
            get { return m_usingTimer; }
            set
            {
                m_usingTimer = value;
                if (value)
                    m_periodic.StartPeriodic(m_period);
                else
                    m_periodic.Stop();
            }
        }

        /// <summary>
        /// Voltage to turn off the compressor at
        /// </summary>
        public double VoltageThreshold { get; set; }

        /// <summary>
        /// Deadband for the voltage on/off trigger
        /// </summary>
        public double VoltageDeadband { get; set; }

        /// <summary>
        /// Creates a new Managed Compressor, which will turn off when the voltage is below the specified level.
        /// </summary>
        /// <param name="voltageThreshold">Voltage to turn off the compressor at</param>
        public ManagedCompressor(double voltageThreshold) : this(voltageThreshold, 0.1) { }

        /// <summary>
        /// Creates a new Managed Compressor, which will turn off when the volrage is below the specified level.
        /// </summary>
        /// <param name="voltageThreshold">Voltage to turn off the compressor at</param>
        /// <param name="Period">time to wait between on and off values.</param>
        public ManagedCompressor(double voltageThreshold, double period)
        {
            m_period = period;
            VoltageThreshold = voltageThreshold;
            m_periodic = new Notifier(Update);
            UseTimer = true;
        }

        /// <summary>
        /// Manually Udates the compressor, turning it on or off if necessary.
        /// Called automatically according to the period value if <see cref="UseTimer"/> is enabled.
        /// </summary>
        public void Update()
        {
            if (ControllerPower.GetInputVoltage() < VoltageThreshold - VoltageDeadband)
            {
                Stop();
            }
            else if(ControllerPower.GetInputVoltage() > VoltageThreshold + VoltageDeadband)
            {
                Start();
            }
        }
    }
}
