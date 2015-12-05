using CSharpRoboticsLib.ControlSystems;
using CSharpRoboticsLib.Extras;
using NUnit.Framework;

namespace IndependentTests.IndependentTests
{
    [TestFixture]
    [Category("TimeBased")]
    public class SimplePidTest
    {
        [Test]
        public void SimplePidProportionalTest()
        {
            SimplePID p = new SimplePID(1, 0, 0);
            p.SetPoint = 1;
            Assert.AreEqual(1, p.Get(0));
        }

        [Test]
        public void SimplePidDerivativeTest()
        {
            SimplePID p = new SimplePID(0, 0, 1);
            p.SetPoint = 1;
            Utility.AccurateWaitSeconds(1);
            Assert.AreEqual(1, p.Get(0), 0.02);
        }

        [Test]
        public void SimplePidIntegralTest()
        {
            SimplePID p = new SimplePID(0, 1, 0);
            p.SetPoint = 1;
            Utility.AccurateWaitSeconds(1);
            Assert.AreEqual(1, p.Get(0), 0.02);
        }

        [Test]
        public void SimplePidLimitTest()
        {
            SimplePID p = new SimplePID(10, 0, 0, -1, 1);
            p.SetPoint = 0;
            Assert.AreEqual(1, p.Get(-1));
            Assert.AreEqual(-1, p.Get(1));
        }
    }
}
