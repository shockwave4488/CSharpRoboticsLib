using System;
using System.Diagnostics;
using CSharpRoboticsLib.Utility;
using CSharpRoboticsLib.WPIExtensions;
using NUnit.Framework;

namespace IndependentTests.ExtrasTests
{
    [TestFixture]
    [Category("TimeBased")]
    public class UtilityTest
    {
        const int TestRepeats = 50;
        const double PercentTolerance = 98;
        double Error => 1.0 - (PercentTolerance / 100);

        [Test]
        public void AccurateWaitTest1Hz()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats / 10; i++) //Ain't nobody got time to wait 20 seconds for a test!
            {
                sw.Restart();
                Util.AccurateWaitSeconds(1.0);
                sw.Stop();
                var seconds = sw.Elapsed.TotalSeconds;
                Assert.That(seconds, Is.EqualTo(1.0).Within(Error));
            }
        }

        [Test]
        public void AccurateWait10Hz()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats; i++)
            {
                sw.Restart();
                Util.AccurateWaitSeconds(0.1);
                sw.Stop();
                var seconds = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                Assert.That(seconds, Is.EqualTo(0.1).Within(Error));
            }
        }

        [Test]
        public void AccurateWait100Hz()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats; i++)
            {
                sw.Restart();
                Util.AccurateWaitSeconds(0.01);
                sw.Stop();
                var seconds = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                Assert.That(seconds, Is.EqualTo(0.01).Within(Error));
            }
        }


        [Test]
        public void AccurateWaitTest1HzMs()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats / 10; i++) //Ain't nobody got time to wait 20 seconds for a test!
            {
                sw.Restart();
                Util.AccurateWaitMilliseconds(1000);
                sw.Stop();
                var seconds = sw.Elapsed.TotalSeconds;
                Assert.That(seconds, Is.EqualTo(1.0).Within(Error));
            }
        }

        [Test]
        public void AccurateWait10HzMs()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats; i++)
            {
                sw.Restart();
                Util.AccurateWaitMilliseconds(100);
                sw.Stop();
                var seconds = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                Assert.That(seconds, Is.EqualTo(0.1).Within(Error));
            }
        }

        [Test]
        public void AccurateWait100HzMs()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats; i++)
            {
                sw.Restart();
                Util.AccurateWaitMilliseconds(10);
                sw.Stop();
                var seconds = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                Assert.That(seconds, Is.EqualTo(0.01).Within(Error));
            }
        }
    }
}
