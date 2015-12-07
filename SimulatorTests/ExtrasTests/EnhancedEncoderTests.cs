using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpRoboticsLib.Extras;
using HAL.Simulator;
using NUnit.Framework;
using WPILib;

using static HAL.Simulator.SimData;

namespace SimulatorTests.ExtrasTests
{
    [TestFixture]
    public class EnhancedEncoderTests
    {
        [Test]
        public void EnhancedEncoderSetDistancePerPulseTest()
        {
            using (Encoder e = new Encoder(0, 1))
            {
                e.SetDistancePerPulse(4, 256);
                Assert.AreEqual(0.04908, e.DistancePerPulse, 0.0001);
            }
        }

        [Test]
        public void EnhancedEncoderRateTest()
        {
            using (Encoder e = new EnhancedEncoder(0, 1))
            {
                e.Reset();
                CSharpRoboticsLib.Extras.Utility.AccurateWaitMilliseconds(100);
                SimData.Encoder[0].Count = 10000;
                Assert.AreEqual(1, e.GetRate());
            }
        }
    }
}
