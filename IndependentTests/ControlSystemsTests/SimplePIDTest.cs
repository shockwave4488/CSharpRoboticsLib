using CSharpRoboticsLib.ControlSystems;
using CSharpRoboticsLib.Utility;
using CSharpRoboticsLib.WPIExtensions;
using NUnit.Framework;

namespace IndependentTests.ControlSystemsTests
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
            Util.AccurateWaitSeconds(1);
            Assert.AreEqual(1, p.Get(0), 0.02);
        }

        [Test]
        public void SimplePidIntegralTest()
        {
            SimplePID p = new SimplePID(0, 1, 0);
            p.SetPoint = 1;
            Util.AccurateWaitSeconds(1);
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

        [Test]
        public static void SimpleContinuousTest()
        {
            SimplePID p = new SimplePID(1, 0, 0) {Continuous = true, MaxInput = 1, MinInput = -1, SetPoint = 0.75};
            Assert.AreEqual(-0.5, p.Get(-0.75));
            p.SetPoint = -0.75;
            Assert.AreEqual(0.5, p.Get(0.75));
            p.SetPoint = 0.5;
            Assert.AreEqual(0.5, p.Get(0));
            p.SetPoint = 0.9;
            Assert.AreEqual(-0.5, p.Get(-0.6));
            p.SetPoint = -0.9;
            Assert.AreEqual(0.5, p.Get(0.6));
        }
}
}
