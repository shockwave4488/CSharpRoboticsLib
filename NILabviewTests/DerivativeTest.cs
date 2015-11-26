using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpRoboticsLib.NILabview;
using System.Threading;

namespace NILabviewTests
{
    [TestClass]
    public class DerivativeTest
    {
        const double PercentAccuracy = 98;
        static double AcceptibleError => 1.0 - (PercentAccuracy / 100);

        [TestMethod]
        public void DerivativePositiveTest10hz()
        {
            Derivative d = new Derivative(0);
            Assert.AreEqual(0, d.Get(), double.Epsilon);
            d.Update(0.1);
            Thread.Sleep(100);
            Assert.AreEqual(1, d.Get(), AcceptibleError);
        }

        [TestMethod]
        public void DerivativeNegativeTest10hz()
        {
            Derivative d = new Derivative(0);
            Assert.AreEqual(0, d.Get(), double.Epsilon);
            d.Update(-0.1);
            Thread.Sleep(100);
            Assert.AreEqual(-1, d.Get(), AcceptibleError);
        }
        
        [TestMethod]
        public void DerivativePositiveTest100hz()
        {
            Derivative d = new Derivative(0);
            Assert.AreEqual(0, d.Get(), double.Epsilon);
            d.Update(0.01);
            Thread.Sleep(10);
            Assert.AreEqual(1, d.Get(), AcceptibleError);
        }

        [TestMethod]
        public void DerivativeNegativeTest100hz()
        {
            Derivative d = new Derivative(0);
            Assert.AreEqual(0, d.Get(), double.Epsilon);
            d.Update(-0.01);
            Thread.Sleep(10);
            Assert.AreEqual(-1, d.Get(), AcceptibleError);
        }
        
    }
}
