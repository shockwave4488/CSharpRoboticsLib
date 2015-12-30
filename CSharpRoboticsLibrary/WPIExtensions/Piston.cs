using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;

namespace CSharpRoboticsLib.WPIExtensions
{
    /// <summary>
    /// A subclass of DoubleSolenoid meant to more intuitively model a piston
    /// </summary>
    public class Piston : DoubleSolenoid
    {
        private bool m_extended;
        private bool m_inverted;

        /// <summary>
        /// True if the piston is extended, false if not
        /// </summary>
        public bool Extended
        {
            get { return m_extended; }
            set
            {
                m_extended = value;
                value ^= m_inverted;
                Set(value ? Value.Forward : Value.Reverse);
            }
        }

        /// <summary>
        /// Inverts the setting of the piston
        /// </summary>
        public bool Inverted
        {
            get { return m_inverted; }
            set
            {
                m_inverted = value;
                Extended = Extended; //Update Extended
            }
        }

        /// <summary>
        /// <see cref="Piston"/> with specified forward and reverse channels
        /// </summary>
        /// <param name="forwardChannel"></param>
        /// <param name="reverseChannel"></param>
        public Piston(int forwardChannel, int reverseChannel) : base(forwardChannel, reverseChannel)
        {
            Extended = Inverted = false;
        }

        /// <summary>
        /// <see cref="Piston"/> with forward and reverse channels at a specific PCM
        /// </summary>
        /// <param name="moduleNumber"></param>
        /// <param name="forwardChannel"></param>
        /// <param name="reverseChannel"></param>
        public Piston(int moduleNumber, int forwardChannel, int reverseChannel) : base(moduleNumber, forwardChannel, reverseChannel)
        {
            Extended = Inverted = false;
        }
    }
}
