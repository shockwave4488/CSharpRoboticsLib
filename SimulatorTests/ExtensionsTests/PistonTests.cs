using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CSharpRoboticsLib.WPIExtensions;
using HAL.Simulator;

namespace SimulatorTests.ExtensionsTests
{
    [TestFixture]
    public class PistonTests : SimTestBase
    {
        [Test]
        public static void PistonBasicTest()
        {
            using (Piston p = new Piston(0, 1))
            {
                Assert.IsFalse(SimData.GetPCM(0).Solenoids[0].Value);
                Assert.IsTrue(SimData.GetPCM(0).Solenoids[1].Value);
                p.Extended = true;
                Assert.IsTrue(SimData.GetPCM(0).Solenoids[0].Value);
                Assert.IsFalse(SimData.GetPCM(0).Solenoids[1].Value);
                p.Extended = false;
                Assert.IsFalse(SimData.GetPCM(0).Solenoids[0].Value);
                Assert.IsTrue(SimData.GetPCM(0).Solenoids[1].Value);
            }
        }

        [Test]
        public static void PistonInvertedTest()
        {
            using (Piston p = new Piston(0, 1) {Inverted = true})
            {
                Assert.IsTrue(SimData.GetPCM(0).Solenoids[0].Value);
                Assert.IsFalse(SimData.GetPCM(0).Solenoids[1].Value);
                p.Extended = true;
                Assert.IsFalse(SimData.GetPCM(0).Solenoids[0].Value);
                Assert.IsTrue(SimData.GetPCM(0).Solenoids[1].Value);
                p.Extended = false;
                Assert.IsTrue(SimData.GetPCM(0).Solenoids[0].Value);
                Assert.IsFalse(SimData.GetPCM(0).Solenoids[1].Value);
            }
        }
    }
}
