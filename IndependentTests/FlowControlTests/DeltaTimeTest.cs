using CSharpRoboticsLib.FlowControl;
using CSharpRoboticsLib.Utility;
using CSharpRoboticsLib.WPIExtensions;
using NUnit.Framework;

namespace IndependentTests.FlowControlTests
{
    [TestFixture]
    [Category("TimeBased")]
    public class DeltaTimeTest
    {
        const int TestRepeats = 20;
        const double PercentAccuracy = 98;
        double Error => 1.0 - (PercentAccuracy / 100.0);

        [Test]
        public void DeltaTime10Hz()
        {
            DeltaTime t = new DeltaTime();
            for(int i = 0; i < TestRepeats; i++)
            {
                Util.AccurateWaitMilliseconds(100);
                Assert.AreEqual(0.1, t.Value, Error);
            }
        }

        [Test]
        public void DeltaTime100Hz()
        {
            DeltaTime t = new DeltaTime();
            for(int i = 0; i < TestRepeats; i++)
            {
                Util.AccurateWaitMilliseconds(10);
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
                Util.AccurateWaitSeconds(0.01);
                Assert.AreEqual(0.01, t.Value, Error);
            }
        }
    }
}
