using System;
using NUnit.Framework;
using CSharpRoboticsLib.Extras;

namespace IndependentTests.ExtrasTests
{
    [TestFixture]
    public class UtilityTest
    {
        const int TestRepeats = 50;
        const double PercentTolerance = 98;
        double Error => 1.0 - (PercentTolerance / 100);

        [Test]
        public void AccurateWaitTest1hz()
        {
            for (int i = 0; i < TestRepeats / 10; i++) //Ain't nobody got time to wait 20 seconds for a test!
            {
                DateTime dt = DateTime.Now;
                Utility.AccurateWaitSeconds(1);
                Assert.AreEqual(1, (DateTime.Now - dt).TotalSeconds, Error);
            }
        }

        [Test]
        public void AccurateWait10hz()
        {
            for (int i = 0; i < TestRepeats; i++)
            {
                DateTime dt = DateTime.Now;
                Utility.AccurateWaitSeconds(0.1);
                Assert.AreEqual(0.1, (DateTime.Now - dt).TotalSeconds, Error);
            }
        }

        [Test]
        public void AccurateWait100hz()
        {
            for(int i = 0; i < TestRepeats; i++)
            {
                DateTime dt = DateTime.Now;
                Utility.AccurateWaitSeconds(0.01);
                Assert.AreEqual(0.01, (DateTime.Now - dt).TotalSeconds, Error);
            }
        }
    }
}
