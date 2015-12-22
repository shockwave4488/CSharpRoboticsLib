using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpRoboticsLib.ControlSystems
{
    /// <summary>
    /// An interface defining different control schemes that can be used for <see cref="SimplePidSystem"/>
    /// </summary>
    public interface IMotionController
    {
        /// <summary>
        /// Gets the value of the controller and updates any internal state.
        /// </summary>
        /// <param name="input">value of the sensor</param>
        /// <returns>value to be sent to the motor</returns>
        double Get(double input);

        /// <summary>
        /// The setpoint for the controller.
        /// </summary>
        double SetPoint { get; set; }
    }
}
