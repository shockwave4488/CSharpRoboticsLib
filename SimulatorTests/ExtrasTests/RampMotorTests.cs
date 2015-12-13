using NUnit.Framework;
using HAL.Simulator;
using CSharpRoboticsLib.Extras;
using WPILib;

namespace SimulatorTests.ExtrasTests
{
    [TestFixture]
    public class RampMotorTests : SimTestBase
    {
        [Test]
        public void RampAccelPositiveTest()
        {
            using (RampMotor motor = new RampMotor(typeof(Talon), 0))
            {
                motor.MaxAccel = 0.2;
                for (int i = 0; motor.Get() < 1; i++)
                {
                    motor.Set(1);
                    Assert.AreEqual(0.2 * (i + 1), motor.Get(), 0.001);
                }
            }
        }

        [Test]
        public void RampAccelNegativeTest()
        {
            using (RampMotor motor = new RampMotor(typeof(Talon), 0))
            {
                motor.MaxAccel = 0.2;
                for (int i = 0; motor.Get() > -1; i++)
                {
                    motor.Set(-1);
                    Assert.AreEqual(-0.2*(i + 1), motor.Get(), 0.001);
                }
            }
        }

        [Test]
        public void RampForceTest()
        {
            using (RampMotor motor = new RampMotor(typeof(Talon), 0))
            {
                motor.MaxChange = 0.2;
                motor.ForcePower(1);
                Assert.AreEqual(1, motor.Get(), 0.001);
            }
        }

        [Test]
        public void RampDecelPositiveTest()
        {
            using (RampMotor motor = new RampMotor(typeof(Talon), 0))
            {
                motor.MaxDecel = 0.2;
                motor.ForcePower(1);
                for (int i = 0; motor.Get() > 0; i++)
                {
                    Assert.AreEqual(1 - (i * 0.2), motor.Get(), 0.001);
                    motor.Set(0);
                }
            }
        }

        [Test]
        public void RampDecelNegativeTest()
        {
            using (RampMotor motor = new RampMotor(typeof(Talon), 0))
            {
                motor.MaxDecel = 0.2;
                motor.ForcePower(-1);
                for (int i = 0; motor.Get() < 0; i++)
                {
                    Assert.AreEqual((i * 0.2) - 1, motor.Get(), 0.001);
                    motor.Set(0);
                }
            }
        }
    }
}