using CSharpRoboticsLib.Extras;
using WPILib;
using NUnit.Framework;
using HAL.Simulator;
using HAL.Simulator.Outputs;
using HAL.Simulator.Inputs;
using HAL.Simulator.Mechanisms;
using HAL.Simulator.Extensions;

namespace SimulatorTests.ExtrasTests
{
    [TestFixture]
    public class DriveEncodersTests : SimTestBase
    {
        [Test]
        public static void TestLinear()
        {
            using (Encoder left = new Encoder(0, 1), right = new Encoder(2, 3))
            {
                DriveEncoders e = new DriveEncoders(left, right);
                SimData.Encoder[0].Count = 4000;
                SimData.Encoder[1].Count = 4000;
                Assert.AreEqual(1000, e.LinearDistance);
            }
        }

        [Test]
        public static void TestRotational()
        {
            using (Encoder left = new Encoder(0, 1), right = new Encoder(2, 3))
            {
                DriveEncoders e = new DriveEncoders(left, right);
                SimData.Encoder[0].Count = 4000;
                SimData.Encoder[1].Count = -4000;
                Assert.AreEqual(1000, e.TurnDistance);
            }
        }

        [Test]
        public static void TestEnhancedLinearRate()
        {
            using (EnhancedEncoder left = new EnhancedEncoder(0, 1), right = new EnhancedEncoder(2, 3))
            {
                EnhancedDriveEncoders e = new EnhancedDriveEncoders(left, right);
                e.Dt = 1;
                SimData.Encoder[0].Count = 4000;
                SimData.Encoder[1].Count = 4000;
                Assert.AreEqual(1000, e.LinearSpeed);
            }
        }

        [Test]
        public static void TestEnhancedTurnRate()
        {
            using (EnhancedEncoder left = new EnhancedEncoder(0, 1), right = new EnhancedEncoder(2, 3))
            {
                EnhancedDriveEncoders e = new EnhancedDriveEncoders(left, right);
                e.Dt = 1;
                SimData.Encoder[0].Count = 4000;
                SimData.Encoder[1].Count = -4000;
                Assert.AreEqual(1000, e.TurnSpeed);
            }
        }
    }
}
