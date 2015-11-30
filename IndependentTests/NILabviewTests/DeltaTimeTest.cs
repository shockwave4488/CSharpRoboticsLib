using System;
using NUnit.Framework;
using CSharpRoboticsLib.NILabview;
using CSharpRoboticsLib.Extras;

namespace IndependentTests.NILabviewTests
{
    [TestFixture]
    [Category("TimeBased")]
    public class DeltaTimeTest
    {
        const int TestRepeats = 20;
        const double PercentAccuracy = 98;
        double Error => 1.0 - (PercentAccuracy / 100.0);

        [Test]
        public void DeltaTime10hz()
        {
            DeltaTime t = new DeltaTime();
            for(int i = 0; i < TestRepeats; i++)
            {
                Utility.AccurateWaitMilliseconds(100);
                Assert.AreEqual(0.1, t.Value, Error);
            }
        }

        [Test]
        public void DeltaTime100hz()
        {
            DeltaTime t = new DeltaTime();
            for(int i = 0; i < TestRepeats; i++)
            {
                Utility.AccurateWaitMilliseconds(10);
                Assert.AreEqual(0.01, t.Value, Error);
            }
        }

        [Test]
        public void DeltaTimeManualTest()
        {
            DeltaTime t = new DeltaTime();
            t.Value = 0.01;
            for(int i = 0; i < TestRepeats; i++)
            {
                Assert.AreEqual(0.01, t.Value, double.Epsilon);
            }
            t.Value = -1;
            for(int i = 0; i < TestRepeats; i++)
            {
                Utility.AccurateWaitSeconds(0.01);
                Assert.AreEqual(0.01, t.Value, Error);
            }
        }
    }
}
