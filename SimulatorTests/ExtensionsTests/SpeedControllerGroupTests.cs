using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HAL.Simulator;
using WPILib;
using CSharpRoboticsLib.WPIExtensions;

namespace SimulatorTests.ExtensionsTests
{
    [TestFixture]
    public class SpeedControllerGroupTests : SimTestBase
    {
        [Test]
        public static void TestSet()
        {
            using (Talon t1 = new Talon(0), t2 = new Talon(1), t3 = new Talon(2))
            {
                SpeedControllerGroup s = new SpeedControllerGroup(t1, t2, t3);
                s.Set(1);
                for (int i = 0; i < 3; i++)
                    Assert.AreEqual(1, SimData.PWM[i].Value, 0.01);

                s.Set(0);
                for (int i = 0; i < 3; i++)
                    Assert.AreEqual(0, SimData.PWM[i].Value, 0.01);
            }
        }

        [Test]
        public static void TestDynamicConstructAndSet()
        {
            //Just testing for runtime errors, move along, move along.
            Type[] controllerTypes =
            {
                typeof (Talon), typeof (Victor), typeof (VictorSP), typeof (Jaguar),
                typeof (TalonSRX)
            };

            foreach (Type t in controllerTypes)
            {
                using (SpeedControllerGroup s = new SpeedControllerGroup(t, 0, 1, 2))
                {
                    s.Set(1);
                }
            }
        }
    }
}
