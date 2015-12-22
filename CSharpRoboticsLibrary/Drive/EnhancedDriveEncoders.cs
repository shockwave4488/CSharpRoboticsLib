using System;
using CSharpRoboticsLib.WPIExtensions;
using WPILib;

namespace CSharpRoboticsLib.Drive
{
    public class EnhancedDriveEncoders : DriveEncoders
    {
        /// <summary>
        /// Left Side <see cref="EnhancedEncoder"/>
        /// </summary>
        public override Encoder Left
        {
            get
            {
                return base.Left;
            }
            protected set
            {
                if (value is EnhancedEncoder)
                    base.Left = value;
                else
                    throw new ArgumentException($"{value} Is not an EnhancedEncoder");
            }
        }

        /// <summary>
        /// Right side <see cref="EnhancedEncoder"/>
        /// </summary>
        public override Encoder Right
        {
            get
            {
                return base.Right;
            }
            protected set
            {
                if (value is EnhancedEncoder)
                    base.Right = value;
                else
                    throw new ArgumentException($"{value} Is not an EnhancedEncoder");
            }
        }

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

        /// <summary>
        /// Encapsulates a Left and Right <see cref="EnhancedEncoder"/>, with the right encoder reversed.
        /// </summary>
        /// <param name="left">Left Encoder</param>
        /// <param name="right">Right Encoder</param>
        public EnhancedDriveEncoders(EnhancedEncoder left, EnhancedEncoder right)
        {
            Left = left;
            Right = right;

            Right.SetReverseDirection(true);
        }
    }
}
