using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CSharpRoboticsLib.Autonomous.Drive;
using CSharpRoboticsLib.Autonomous;

namespace SimulatorTests.AutonomousTests
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
