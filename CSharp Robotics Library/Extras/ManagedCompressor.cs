using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using CSharp_Robotics_Library.FlowControl;

namespace CSharp_Robotics_Library.Extras
{
    /// <summary>
    /// A Compressor which will not run if the coltage is too low.
    /// </summary>
    public class ManagedCompressor : Compressor
    {
        /// <summary>
        /// Voltage to turn off the compressor at
        /// </summary>
        public double VoltageThreshold { get; set; }

        private WaitByTime hysterisis;

        /// <summary>
        /// Creates a new Managed Compressor, which will turn off when the voltage is below the specified level
        /// </summary>
        /// <param name="voltageThreshold">Voltage to turn off the compressor at</param>
        public ManagedCompressor(double voltageThreshold) : base()
        {
            VoltageThreshold = voltageThreshold;
            hysterisis = new WaitByTime(0.1);
        }

        /// <summary>
        /// Updats the compressor, turning it on or off if necessary.
        /// </summary>
        public void Update()
        {
            if (ControllerPower.GetInputVoltage() < VoltageThreshold && hysterisis.DoneWaiting)
            {
                Stop();
                hysterisis.Reset();
            }
            else if (hysterisis.DoneWaiting)
            {
                Start();
                hysterisis.Reset();
            }
        }
    }
}
