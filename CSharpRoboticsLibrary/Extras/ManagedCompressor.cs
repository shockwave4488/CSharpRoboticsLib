using WPILib;
using System.Timers;

namespace CSharpRoboticsLib.Extras
{
    /// <summary>
    /// A Compressor which will not run if the voltage is too low.
    /// </summary>
    public class ManagedCompressor : Compressor
    {
        private System.Timers.Timer m_timer;

        /// <summary>
        /// Voltage to turn off the compressor at
        /// </summary>
        public double VoltageThreshold { get; set; }

        /// <summary>
        /// Deadband for the voltage on/off trigger
        /// </summary>
        public double VoltageDeadband { get; set; }

        /// <summary>
        /// Set to true to use the built-in timer.
        /// </summary>
        public bool UseTimer
        {
            get
            {
                return m_timer.Enabled;
            }
            set
            {
                m_timer.Enabled = value;
            }
        }

        /// <summary>
        /// Creates a new Managed Compressor, which will turn off when the voltage is below the specified level.
        /// </summary>
        /// <param name="voltageThreshold">Voltage to turn off the compressor at</param>
        public ManagedCompressor(double voltageThreshold) : this(voltageThreshold, 0.1) { }

        /// <summary>
        /// Creates a new Managed Compressor, which will turn off when the volrage is below the specified level.
        /// </summary>
        /// <param name="voltageThreshold">Voltage to turn off the compressor at</param>
        /// <param name="hysterisis">time to wait between on and off values.</param>
        public ManagedCompressor(double voltageThreshold, double hysterisis)
        {
            VoltageThreshold = voltageThreshold;
            m_timer = new System.Timers.Timer(hysterisis * 1000);
            m_timer.Elapsed += (sender, e) => Update();
            m_timer.Start();
            UseTimer = true;
        }

        /// <summary>
        /// Updats the compressor, turning it on or off if necessary.
        /// Called automatically if the built-in timer is enabled.
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
