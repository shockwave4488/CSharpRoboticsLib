using CSharpRoboticsLib.ControlSystems;
using NUnit.Framework;

namespace IndependentTests.ControlSystemsTests
{
    [TestFixture]
    public class OutputRateLimitTests
    {
        [Test]
        public static void PositiveRateLimit()
        {
            OutputRateLimit o = new OutputRateLimit(1);
            o.Dt = 0.1;

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(o.Get(1), (i + 1) * 0.1, 0.00001);
            }
        }

        [Test]
        public static void NegativeRateLimit()
        {
            OutputRateLimit o = new OutputRateLimit(1);
            o.Dt = 0.1;

            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(o.Get(-1), (i + 1) * -0.1, 0.00001);
            }
        }
    }
}
