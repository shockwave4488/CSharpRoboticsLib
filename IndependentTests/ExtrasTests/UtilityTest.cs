using System;
using System.Diagnostics;
using NUnit.Framework;
using CSharpRoboticsLib.Extras;

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
        public void AccurateWaitTest1hz()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats / 10; i++) //Ain't nobody got time to wait 20 seconds for a test!
            {
                sw.Restart();
                Utility.AccurateWaitSeconds(1.0);
                sw.Stop();
                var seconds = sw.Elapsed.TotalSeconds;
                Assert.That(seconds, Is.EqualTo(1.0).Within(Error));
            }
        }

        [Test]
        public void AccurateWait10hz()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats; i++)
            {
                sw.Restart();
                Utility.AccurateWaitSeconds(0.1);
                sw.Stop();
                var seconds = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                Assert.That(seconds, Is.EqualTo(0.1).Within(Error));
            }
        }

        [Test]
        public void AccurateWait100hz()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats; i++)
            {
                sw.Restart();
                Utility.AccurateWaitSeconds(0.01);
                sw.Stop();
                var seconds = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                Assert.That(seconds, Is.EqualTo(0.01).Within(Error));
            }
        }


        [Test]
        public void AccurateWaitTest1hzMs()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats / 10; i++) //Ain't nobody got time to wait 20 seconds for a test!
            {
                sw.Restart();
                Utility.AccurateWaitMilliseconds(1000);
                sw.Stop();
                var seconds = sw.Elapsed.TotalSeconds;
                Assert.That(seconds, Is.EqualTo(1.0).Within(Error));
            }
        }

        [Test]
        public void AccurateWait10hzMs()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats; i++)
            {
                sw.Restart();
                Utility.AccurateWaitMilliseconds(100);
                sw.Stop();
                var seconds = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                Assert.That(seconds, Is.EqualTo(0.1).Within(Error));
            }
        }

        [Test]
        public void AccurateWait100hzMs()
        {
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < TestRepeats; i++)
            {
                sw.Restart();
                Utility.AccurateWaitMilliseconds(10);
                sw.Stop();
                var seconds = (double)sw.ElapsedTicks / Stopwatch.Frequency;
                Assert.That(seconds, Is.EqualTo(0.01).Within(Error));
            }
        }
    }
}
