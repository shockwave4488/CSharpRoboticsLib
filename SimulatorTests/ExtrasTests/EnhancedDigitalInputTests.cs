using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpRoboticsLib.Extras;
using NUnit.Framework;
using WPILib;

using static HAL.Simulator.SimData;

namespace SimulatorTests.ExtrasTests
{
    [TestFixture]
    public class EnhancedDigitalInputTests : SimTestBase
    {
        [Test]
        public void EnhancedDigitalInvertedTest()
        {
            using (EnhancedDigitalInput e = new EnhancedDigitalInput(0))
            {
                e.Inverted = true;
                DIO[0].Value = true;
                Assert.IsFalse(e.Get());
                DIO[0].Value = false;
                Assert.IsTrue(e.Get());
            }
        }

        [Test]
        public void EnhencedDigitalOverrideTest()
        {
            using (DigitalInput e = new EnhancedDigitalInput(0) {Inverted = true})
            {
                DIO[0].Value = true;
                Assert.IsFalse(e.Get());
                DIO[0].Value = false;
                Assert.IsTrue(e.Get());
            }
        }
    }
}
