using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPILib;

namespace CSharpRoboticsLib.Extras
{
    public class EnhancedDriveEncoders : DriveEncoders
    {
        /// <summary>
        /// Sets the Delta Time for both encoders
        /// </summary>
        public double Dt
        {
            get { return ((Left as EnhancedEncoder).Dt + (Right as EnhancedEncoder).Dt)/2; }
            set { (Left as EnhancedEncoder).Dt = value; (Right as EnhancedEncoder).Dt = value; }
        }

        /// <summary>
        /// Initialializes both encoders to the channels specified, with the RIGHT encoder reversed.
        /// </summary>
        /// <param name="lAChannel">Left A Channel</param>
        /// <param name="lBChannel">Left B Channel</param>
        /// <param name="rAChannel">Right A Channel</param>
        /// <param name="rBChannel">Right B Channel</param>
        public EnhancedDriveEncoders(int lAChannel, int lBChannel, int rAChannel, int rBChannel)
        {
            Left = new EnhancedEncoder(lAChannel, lBChannel);
            Right = new EnhancedEncoder(rAChannel, rBChannel);

            Right.SetReverseDirection(true);
        }
    }
}
