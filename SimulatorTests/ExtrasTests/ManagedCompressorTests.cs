using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CSharpRoboticsLib.Extras;
using WPILib;
using NUnit.Framework;
using static HAL.Simulator.SimData;

namespace SimulatorTests.ExtrasTests
{
    [TestFixture]
    public class ManagedCompressorTests : TestBase
    {
        [Test]
        public void CompressorVoltageStartStopTest()
        {
            using (ManagedCompressor c = new ManagedCompressor(10))
            {
                c.UseTimer = false;
                RoboRioData.VInVoltage = 12;
                c.Update();
                Assert.IsTrue(GetPCM(0).Compressor.CloseLoopEnabled);
                RoboRioData.VInVoltage = 9;
                c.Update();
                Assert.IsFalse(GetPCM(0).Compressor.CloseLoopEnabled);
            }
        }

        [Test]
        public void CompressorTimerTest()
        {
            using (ManagedCompressor c = new ManagedCompressor(10, 0.1))
            {
                //Reset the compressor from any residual values.
                RoboRioData.VInVoltage = 12;
                c.Update();

                Assert.IsTrue(GetPCM(0).Compressor.CloseLoopEnabled);
                RoboRioData.VInVoltage = 9;
                Assert.IsTrue(GetPCM(0).Compressor.CloseLoopEnabled);
                Thread.Sleep(200);
                Assert.IsFalse(GetPCM(0).Compressor.CloseLoopEnabled);
            }
        }

    }
}
