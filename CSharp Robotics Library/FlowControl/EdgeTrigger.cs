﻿namespace CSharp_Robotics_Library.FlowControl
{
    /// <summary>
    /// Used to detect changes in a boolean state
    /// </summary>
    public class EdgeTrigger
    {
        private bool feedback;

        public EdgeTrigger()
        {
            feedback = false;
        }

        /// <summary>
        /// Manually update the internal state
        /// </summary>
        /// <param name="trigger">Current value of the edge triggered state</param>
        public void Update(bool trigger)
        {
            feedback = trigger;
        }

        /// <summary>
        /// Returns true if there is a rising edge and updates the internal state
        /// </summary>
        /// <param name="trigger">Current value of edge triggered state</param>
        /// <returns>Rising Edge</returns>
        public bool GetRisingUpdate(bool trigger)
        {
            bool toReturn = (trigger && !feedback);
            feedback = trigger;
            return toReturn;
        }

        /// <summary>
        /// Returns true if there is a falling edge and updates the internal state
        /// </summary>
        /// <param name="trigger">Current value of edge triggered state</param>
        /// <returns>Falling Edge</returns>
        public bool GetFallingUpdate(bool trigger)
        {
            bool toReturn = (!trigger && feedback);
            feedback = trigger;
            return toReturn;
        }

        /// <summary>
        /// Returns true if there is a rising edge, does not update the internal state
        /// </summary>
        /// <param name="trigger">Current value of the edge triggered state</param>
        /// <returns>Rising edge</returns>
        public bool GetRising(bool trigger)
        {
            return (trigger && !feedback);
        }

        /// <summary>
        /// Returns true if there is a falling edge, does not update the internal state
        /// </summary>
        /// <param name="trigger">Current value of the edge triggered state</param>
        /// <returns>Falling edge</returns>
        public bool GetFalling(bool trigger)
        {
            return (!trigger && feedback);
        }
    }
}
