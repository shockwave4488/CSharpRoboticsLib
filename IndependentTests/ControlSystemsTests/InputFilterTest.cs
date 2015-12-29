using CSharpRoboticsLib.ControlSystems;
using CSharpRoboticsLib.Utility;
using NUnit.Framework;

namespace IndependentTests.ControlSystemsTests
{
    [TestFixture]
    public class InputFilterTest
    {
        [Test]
        public void FilterPositive()
        {
            InputFilter t = new InputFilter(0);
            Assert.AreEqual(0, t.Get(), 0);
            for(int i = 0; i < InputFilter.BufferLength; i++)
            {
                t.Get(1);
            }
            Assert.AreEqual(1, t.Get(), 0);
        }

        [Test]
        public void FilterNegative()
        {
            InputFilter t = new InputFilter();
            Assert.AreEqual(0, t.Get(), 0);
            for (int i = 0; i < InputFilter.BufferLength; i++)
            {
                t.Get(-1);
            }
            Assert.AreEqual(-1, t.Get(), 0);
        }

        [Test]
        public void ReInitializeTest()
        {
            InputFilter t = new InputFilter();
            t.ReInitialize(1);
            Assert.AreEqual(1, t.Get(), 0);
        }
    }
}
