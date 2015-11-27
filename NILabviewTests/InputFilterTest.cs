using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpRoboticsLib.NILabview;

namespace NILabviewTests
{
    [TestClass]
    public class InputFilterTest
    {
        [TestMethod]
        public void FilterPositive()
        {
            InputFilter t = new InputFilter(0);
            Assert.AreEqual(0, t.Value, 0);
            for(int i = 0; i < InputFilter.BufferLength; i++)
            {
                t.Update(1);
            }
            Assert.AreEqual(1, t.Value, 0);
        }

        [TestMethod]
        public void FilterNegative()
        {
            InputFilter t = new InputFilter();
            Assert.AreEqual(0, t.Value, 0);
            for (int i = 0; i < InputFilter.BufferLength; i++)
            {
                t.Update(-1);
            }
            Assert.AreEqual(-1, t.Value, 0);
        }

        [TestMethod]
        public void ReInitializeTest()
        {
            InputFilter t = new InputFilter();
            t.ReInitialize(1);
            Assert.AreEqual(1, t.Value, 0);
        }
    }
}
