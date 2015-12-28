using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WPILib;
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

        public void PidWrite(double value)
        {
            foreach (ISpeedController s in m_controllers)
                s.PidWrite(value);
        }

        public void Dispose()
        {
            foreach (ISpeedController s in m_controllers)
                s.Dispose();
        }

        public void Set(double value)
        {
            foreach (ISpeedController s in m_controllers)
                s.Set(value);
        }

        public double Get()
        {
            double toReturn = 0;

            foreach (ISpeedController s in m_controllers)
                toReturn += s.Get();

            return toReturn/m_controllers.Length;
        }

        public void Set(double value, byte syncGroup)
        {
            foreach (ISpeedController s in m_controllers)
                s.Set(value, syncGroup);
        }

        public void Disable()
        {
            foreach (ISpeedController s in m_controllers)
                s.Disable();
        }

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
