using System.Diagnostics;
using NUnit.Framework;
using CSharpRoboticsLib.NILabview;

namespace NILabviewTests
{
    [TestFixture]
    public class DerivativeTest
    {
        private const double PercentAccuracy = 98;
        private const int TestRepeats = 10;
        private static double AcceptibleError => 1.0 - (PercentAccuracy / 100);

        private static Stopwatch timer = new Stopwatch();

        private static void AccurateWait(double seconds)
        {
            timer.Restart();
            while (timer.Elapsed.TotalSeconds < seconds)
                ;
        }

        [Test]
        public void DerivativePositiveTest10hz()
        {
            Derivative d = new Derivative(0);
            Assert.AreEqual(0, d.Get(), double.Epsilon);
            for (int i = 0; i < TestRepeats; i++)
            {
                d.Update(0.1 * (i + 1));
                AccurateWait(0.1);
                Assert.AreEqual(1, d.Get(), AcceptibleError);
            }
        }

        [Test]
        public void DerivativeNegativeTest10hz()
        {
            Derivative d = new Derivative(0);
            Assert.AreEqual(0, d.Get(), double.Epsilon);
            for (int i = 0; i < TestRepeats; i++)
            {
                d.Update(-0.1 * (i + 1));
                AccurateWait(0.1);
                Assert.AreEqual(-1, d.Get(), AcceptibleError);
            }
        }
        
        [Test]
        public void DerivativePositiveTest100hz()
        {
            Derivative d = new Derivative(0);
            Assert.AreEqual(0, d.Get(), double.Epsilon);
            for (int i = 0; i < TestRepeats; i++)
            {
                d.Update(0.01 * (i + 1));
                AccurateWait(0.01);
                Assert.AreEqual(1, d.Get(), AcceptibleError);
            }
        }

        [Test]
        public void DerivativeNegativeTest100hz()
        {
            Derivative d = new Derivative(0);
            Assert.AreEqual(0, d.Get(), double.Epsilon);
            for (int i = 0; i < TestRepeats; i++)
            {
                d.Update(-0.01 * (i + 1));
                AccurateWait(0.01);
                Assert.AreEqual(-1, d.Get(), AcceptibleError);
            }
        }
        
    }
}
