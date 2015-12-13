using CSharpRoboticsLib.Extras;
using WPILib;

namespace CSharpRoboticsLib.Drive
{
    public class DriveEncoders
    {
        /// <summary>
        /// Left side Encoder
        /// </summary>
        public virtual Encoder Left { get; protected set; }

        /// <summary>
        /// Right side Encoder
        /// </summary>
        public virtual Encoder Right { get; protected set; }

        /// <summary>
        /// Initialializes both encoders to the channels specified, with the RIGHT encoder reversed.
        /// </summary>
        /// <param name="lAChannel">Left A Channel</param>
        /// <param name="lBChannel">Left B Channel</param>
        /// <param name="rAChannel">Right A Channel</param>
        /// <param name="rBChannel">Right B Channel</param>
        public DriveEncoders(int lAChannel, int lBChannel, int rAChannel, int rBChannel) : 
            this(new Encoder(lAChannel, lBChannel), new Encoder(rAChannel, rBChannel))
        {
        }

        /// <summary>
        /// Encapsulates a Left and Right Encoder, with the right encoder reversed.
        /// </summary>
        /// <param name="left">Left Encoder</param>
        /// <param name="right">Right Encoder</param>
        public DriveEncoders(Encoder left, Encoder right)
        {
            Left = left;
            Right = right;
            Right.SetReverseDirection(true);
        }

        protected DriveEncoders()
        {
        }

        /// <summary>
        /// Distance reported by the Left Encoder
        /// </summary>
        public double LeftDistance => Left.GetDistance();

        /// <summary>
        /// Distance reported by the Right Encoder
        /// </summary>
        public double RightDistance => Right.GetDistance();

        /// <summary>
        /// Speed reported by the Left encoder
        /// </summary>
        public double LeftSpeed => Left.GetRate();

        /// <summary>
        /// Speed reported by the Right encoder
        /// </summary>
        public double RightSpeed => Right.GetRate();

        /// <summary>
        /// Distance the robot has traveled linearly (Average of both encoder distances)
        /// </summary>
        public double LinearDistance => (LeftDistance + RightDistance)/2;

        /// <summary>
        /// Distance the robot has traveled rotationally (Difference between encoder distances)
        /// </summary>
        public double TurnDistance => (LeftDistance - RightDistance)/2;

        /// <summary>
        /// Speed the robot is traveling linearly (Average of both encoder speeds)
        /// </summary>
        public double LinearSpeed => (LeftSpeed + RightSpeed)/2;

        /// <summary>
        /// Speed the robot is turning at (Difference between encoder speeds)
        /// </summary>
        public double TurnSpeed => (LeftSpeed - RightSpeed)/2;

        /// <summary>
        /// Resets both encoders
        /// </summary>
        public void Reset()
        {
            Left.Reset();
            Right.Reset();
        }

        /// <summary>
        /// Sets the Left and Right DistancePerPulse in feet based on Wheel Diameter and counts per revolution 
        /// </summary>
        /// <param name="wheelDiameter">Diameter of your drive wheels IN INCHES</param>
        /// <param name="countPerRevolution"></param>
        public void SetDistancePerPulse(double wheelDiameter, int countPerRevolution)
        {
            Left.SetDistancePerPulse(wheelDiameter / 12, countPerRevolution);
            Right.SetDistancePerPulse(wheelDiameter / 12, countPerRevolution);
        }
    }
}
