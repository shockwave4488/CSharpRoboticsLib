using System;
using WPILib.Interfaces;

namespace CSharpRoboticsLib.WPIExtensions
{
    /// <summary>
    /// An encapsulated collection of <see cref="ISpeedController"/>s
    /// </summary>
    public class SpeedControllerGroup : ISpeedController
    {
        private ISpeedController[] m_controllers;

        /// <summary>
        /// Raw array data
        /// </summary>
        public ISpeedController[] Data => m_controllers;

        /// <summary>
        /// Creates a new <see cref="SpeedControllerGroup"/> from the given <see cref="ISpeedController"/>s
        /// </summary>
        /// <param name="controllers"></param>
        public SpeedControllerGroup(params ISpeedController[] controllers)
        {
            m_controllers = controllers;
        }

        /// <summary>
        /// Creates new <see cref="ISpeedController"/>s of the given <see cref="Type"/> at the given ports
        /// </summary>
        /// <param name="controllerType">type of controller to create</param>
        /// <param name="ports">ports to initialize the controller at</param>
        public SpeedControllerGroup(Type controllerType, params int[] ports)
        {
            m_controllers = new ISpeedController[ports.Length];
            for (int i = 0; i < ports.Length; i++)
            {
                m_controllers[i] = (ISpeedController)Activator.CreateInstance(controllerType, ports[i]);
            }
        }

        /// <summary>
        /// Set output to the value calculated by <see cref="PIDController"/>
        /// </summary>
        /// <param name="value"></param>
        public void PidWrite(double value)
        {
            foreach (ISpeedController s in m_controllers)
                s.PidWrite(value);
        }
         
        /// <summary>
        /// Performs appication-defined tasks associated with freeing, releasing, or resetting unmanaged resources
        /// </summary>
        public void Dispose()
        {
            foreach (ISpeedController s in m_controllers)
                s.Dispose();
        }

        /// <summary>
        /// Sets the output value for these speed controllers
        /// </summary>
        /// <param name="value"></param>
        public void Set(double value)
        {
            foreach (ISpeedController s in m_controllers)
                s.Set(value);
        }

        /// <summary>
        /// returns the last value set to these speed controllers
        /// </summary>
        /// <returns></returns>
        public double Get()
        {
            double toReturn = 0;

            foreach (ISpeedController s in m_controllers)
                toReturn += s.Get();

            return toReturn/m_controllers.Length;
        }

        /// <summary>
        /// Sets the output value for these speed controllers
        /// </summary>
        /// <param name="value"></param>
        /// <param name="syncGroup"></param>
        public void Set(double value, byte syncGroup)
        {
            foreach (ISpeedController s in m_controllers)
                s.Set(value, syncGroup);
        }

        /// <summary>
        /// Disables the speed controllers
        /// </summary>
        public void Disable()
        {
            foreach (ISpeedController s in m_controllers)
                s.Disable();
        }

        /// <summary>
        /// Inverts the direction of the motors' rotation
        /// </summary>
        public bool Inverted
        {
            get { return m_controllers[0].Inverted; }
            set
            {
                foreach (ISpeedController s in m_controllers)
                    s.Inverted = value;
            }
        }
    }
}
