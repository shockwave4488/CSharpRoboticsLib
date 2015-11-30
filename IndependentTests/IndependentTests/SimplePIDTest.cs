using CSharpRoboticsLib.ControlSystems;
using CSharpRoboticsLib.Extras;
using NUnit.Framework;

namespace IndependentTests.IndependentTests
{
    [TestFixture]
    [Category("TimeBased")]
    public class SimplePIDTest
    {
        [Test]
        public void SimplePIDProportionalTest()
        {
            SimplePID p = new SimplePID(1, 0, 0);
            p.SetPoint = 1;
            Assert.AreEqual(1, p.Get(0));
        }

        [Test]
        public void SimplePIDDerivativeTest()
        {
            SimplePID p = new SimplePID(0, 0, 1);
            p.SetPoint = 1;
            Utility.AccurateWaitSeconds(1);
            Assert.AreEqual(1, p.Get(0), 0.02);
        }

        [Test]
        public void SimplePIDIntegralTest()
        {
            SimplePID p = new SimplePID(0, 1, 0);
            p.SetPoint = 1;
            Utility.AccurateWaitSeconds(1);
            Assert.AreEqual(1, p.Get(0), 0.02);
        }

        [Test]
        public void SimplePIDLimitTest()
        {
            SimplePID p = new SimplePID(10, 0, 0, -1, 1);
            p.SetPoint = 0;
            Assert.AreEqual(1, p.Get(-1));
            Assert.AreEqual(-1, p.Get(1));
        }
    }
}
