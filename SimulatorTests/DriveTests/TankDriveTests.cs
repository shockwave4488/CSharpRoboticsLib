using NUnit.Framework;
using CSharpRoboticsLib.Drive.Interfaces;

namespace SimulatorTests.DriveTests
{
    [TestFixture]
    public class TankDriveTests
    {
        
    }

    internal class TestDrive : ITankDrive
    {
        public double LeftPower { get; private set; }
        public double RightPower { get; private set; }

        public void SetPowers(double leftPower, double rightPower)
        {
            LeftPower = leftPower;
            RightPower = rightPower;
        }
    }
}
