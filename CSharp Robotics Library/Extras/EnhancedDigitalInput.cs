using WPILib;
using CSharp_Robotics_Library.FlowControl;

namespace CSharp_Robotics_Library.Extras
{
    /// <summary>
    /// Digital input with built-in edge detecting and inversion
    /// </summary>
    public class EnhancedDigitalInput : DigitalInput
    {
        private EdgeTrigger Edge;

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
            Edge = new EdgeTrigger();
            Inverted = false;
        }

        /// <summary>
        /// Get the value from the Enhanced Digital Input Channel
        /// </summary>
        /// <returns></returns>
        public new bool Get()
        {
            return Get() ^ Inverted;
        }

        /// <summary>
        /// Returns true if there is a rising edge, does not update the internal state
        /// </summary>
        /// <returns></returns>
        public bool GetRising() => Edge.GetRising(Get());

        /// <summary>
        /// Returns true if there is a falling edge, does not update the internal state
        /// </summary>
        /// <returns></returns>
        public bool GetFalling() => Edge.GetFalling(Get());

        /// <summary>
        /// Returns true if there is a rising edge and updates the internal state
        /// </summary>
        /// <returns></returns>
        public bool GetRisingUpdate() => Edge.GetRisingUpdate(Get());

        /// <summary>
        /// Returns true if there is a falling edge and updates the internal state
        /// </summary>
        /// <returns></returns>
        public bool GetFallingUpdate() => Edge.GetFallingUpdate(Get());

        /// <summary>
        /// Manually update the internal state of the edge trigger
        /// </summary>
        public void Update() => Edge.Update(Get());
    }
}
