using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Robotics_Library.FlowControl
{
    /// <summary>
    /// Can detect a change in data for any given equatable type
    /// </summary>
    /// <typeparam name="T">An equatable type</typeparam>
    public class ChangeTrigger<T>
    {
        T state;

        /// <summary>
        /// Creates a new ChangeTrigger with a default initial state
        /// </summary>
        public ChangeTrigger()
        {
            state = default(T);
        }

        /// <summary>
        /// creates a new ChangeTrigger with a specific initial state
        /// </summary>
        public ChangeTrigger(T initialState)
        {
            state = initialState;
        }

        /// <summary>
        /// Returns true if there is a change in value and does not update the internal state
        /// </summary>
        /// <param name="toCompare">value to compare</param>
        /// <returns>change in value</returns>
        public bool GetChange(T toCompare)
        {
            return !state.Equals(toCompare);
        }

        /// <summary>
        /// Returns true if there is a change in value and updates the internal state
        /// </summary>
        /// <param name="toCompare">value to compare and update the internal state with</param>
        /// <returns>Change in value</returns>
        public bool getChangeUpdate(T toCompare)
        {
            bool toReturn = !state.Equals(toCompare);
            state = toCompare;
            return toReturn;
        }

        /// <summary>
        /// Manually update the internal state
        /// </summary>
        /// <param name="newValue">New internal state</param>
        public void Update(T newValue)
        {
            state = newValue;
        }
    }
}
