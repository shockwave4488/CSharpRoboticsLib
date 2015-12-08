using CSharpRoboticsLib.Extras;
using HAL.Simulator;
using NUnit.Framework;
using WPILib;

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
            using (Encoder e = new EnhancedEncoder(0, 1) {Dt = 0.1})
            {
                e.Reset();
                e.Get(); //Push the value to the Derivative
                SimData.Encoder[0].Count = 4000;
                Assert.AreEqual(10000, e.GetRate());
            }
        }
    }
}
