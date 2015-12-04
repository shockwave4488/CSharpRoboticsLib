using WPILib;
using CSharpRoboticsLib.FlowControl;

namespace CSharpRoboticsLib.Extras
{
    /// <summary>
    /// Digital input with built-in edge detecting and inversion
    /// </summary>
    public class EnhancedDigitalInput : DigitalInput
    {
        private EdgeTrigger m_edge;
        
        /// <summary>
        /// Inverts the read value of the Digital Input
        /// </summary>
        public bool Inverted { get; set; }

        /// <summary>
        /// Create an instance of EnhancedDigitalInput
        /// </summary>
        /// <param name="channel">Channel to assign to this input</param>
        public EnhancedDigitalInput(int channel) : base(channel)
        {
            m_edge = new EdgeTrigger();
            Inverted = false;
        }

        /// <summary>
        /// Get the value from the Enhanced Digital Input Channel
        /// </summary>
        /// <returns></returns>
        public override bool Get()
        {
            return base.Get() ^ Inverted;
        }

        /// <summary>
        /// Returns true if there is a rising edge, does not update the internal state
        /// </summary>
        /// <returns></returns>
        public bool GetRising() => m_edge.GetRising(Get());

        /// <summary>
        /// Returns true if there is a falling edge, does not update the internal state
        /// </summary>
        /// <returns></returns>
        public bool GetFalling() => m_edge.GetFalling(Get());

        /// <summary>
        /// Returns true if there is a rising edge and updates the internal state
        /// </summary>
        /// <returns></returns>
        public bool GetRisingUpdate() => m_edge.GetRisingUpdate(Get());

        /// <summary>
        /// Returns true if there is a falling edge and updates the internal state
        /// </summary>
        /// <returns></returns>
        public bool GetFallingUpdate() => m_edge.GetFallingUpdate(Get());

        /// <summary>
        /// Manually update the internal state of the edge trigger
        /// </summary>
        public void Update() => m_edge.Update(Get());
    }
}
