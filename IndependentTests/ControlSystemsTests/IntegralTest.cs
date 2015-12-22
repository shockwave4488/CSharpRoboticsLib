using CSharpRoboticsLib.ControlSystems;
using CSharpRoboticsLib.Utility;
using NUnit.Framework;

namespace IndependentTests.ControlSystemsTests
{
    [TestFixture]
    [Category("TimeBased")]
    public class IntegralTest
    {
        [Test]
        public void PositiveIntegralTest1Step()
        {
            Integral t = new Integral();
            //Utility.AccurateWaitSeconds(1);
            t.Dt = 1.0;
            Assert.AreEqual(1, t.Get(1), 0.02);
        }

        [Test]
        public void PositiveIntegralTest10Step()
        {
            Integral t = new Integral();
            t.Dt = 0.1;
            for(int i = 0; i < 10; i++)
            {
                //Utility.AccurateWaitMilliseconds(100);
                Assert.AreEqual(0.1 * (i + 1), t.Get(1), 0.02);
            }
        }

        [Test]
        public void NegativeIntegralTest1Step()
        {
            Integral t = new Integral();
            t.Dt = 1;
            //Utility.AccurateWaitSeconds(1);
            Assert.AreEqual(-1, t.Get(-1), 0.02);
        }

        [Test]
        public void NegativeIntegralTest10Step()
        {
            Integral t = new Integral();
            t.Dt = 0.1;
            for (int i = 0; i < 10; i++)
            {
                //Utility.AccurateWaitMilliseconds(100);
                Assert.AreEqual(-0.1 * (i + 1), t.Get(-1), 0.02);
            }
        }
    }
}
