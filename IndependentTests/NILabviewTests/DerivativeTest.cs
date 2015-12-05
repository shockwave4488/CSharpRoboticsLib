using System.Diagnostics;
using NUnit.Framework;
using CSharpRoboticsLib.NILabview;

namespace IndependentTests.NILabviewTests
{
    [TestFixture]
    [Category("TimeBased")]
    public class DerivativeTest
    {
        private const double PercentAccuracy = 98;
        private const int TestRepeats = 20;
        private static double AcceptibleError => 1.0 - (PercentAccuracy / 100);

        [Test]
        public void DerivativePositiveTest10Hz()
        {
            Derivative d = new Derivative(0);
            d.Dt = 0.1;
            Assert.AreEqual(0, d.Get(0), double.Epsilon);
            for (int i = 0; i < TestRepeats; i++)
            {
                //Utility.AccurateWaitSeconds(0.1);
                Assert.AreEqual(1, d.Get(0.1 * (i + 1)), AcceptibleError);
            }
        }

        [Test]
        public void DerivativeNegativeTest10Hz()
        {
            Derivative d = new Derivative(0);
            d.Dt = 0.1;
            Assert.AreEqual(0, d.Get(0), double.Epsilon);
            for (int i = 0; i < TestRepeats; i++)
            {
                //Utility.AccurateWaitSeconds(0.1);
                Assert.AreEqual(-1, d.Get(-0.1 * (i + 1)), AcceptibleError);
            }
        }
        
        [Test]
        public void DerivativePositiveTest100Hz()
        {
            Derivative d = new Derivative(0);
            d.Dt = 0.01;
            Assert.AreEqual(0, d.Get(0), double.Epsilon);
            for (int i = 0; i < TestRepeats; i++)
            {
                //Utility.AccurateWaitSeconds(0.01);
                Assert.AreEqual(1, d.Get(0.01 * (i + 1)), AcceptibleError);
            }
        }

        [Test]
        public void DerivativeNegativeTest100Hz()
        {
            Derivative d = new Derivative(0);
            d.Dt = 0.01;
            Assert.AreEqual(0, d.Get(0), double.Epsilon);
            for (int i = 0; i < TestRepeats; i++)
            {
                //Utility.AccurateWaitSeconds(0.01);
                Assert.AreEqual(-1, d.Get(-0.01 * (i + 1)), AcceptibleError);
            }
        }
        
    }
}
