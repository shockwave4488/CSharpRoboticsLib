using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp_Robotics_Library.FlowControl;

namespace FlowControlTests
{
    [TestClass]
    public class EdgeTriggerTest
    {
        [TestMethod]
        public void AutoUpdateRisingToFalling()
        {
            EdgeTrigger t = new EdgeTrigger();
            t.Update(false);
            Assert.IsTrue(t.GetRisingUpdate(true));
            Assert.IsTrue(t.GetFallingUpdate(false));
        }

        [TestMethod]
        public void AutoUpdateFallingToRising()
        {
            EdgeTrigger t = new EdgeTrigger();
            t.Update(true);
            Assert.IsTrue(t.GetFallingUpdate(false));
            Assert.IsTrue(t.GetRisingUpdate(true));
        }

        [TestMethod]
        public void ManualUpdateRisingToFalling()
        {
            EdgeTrigger t = new EdgeTrigger();
            t.Update(false);
            Assert.IsTrue(t.GetRising(true));
            t.Update(true);
            Assert.IsTrue(t.GetFalling(false));
        }

        [TestMethod]
        public void ManualUpdateFallingToRising()
        {
            EdgeTrigger t = new EdgeTrigger();
            t.Update(true);
            Assert.IsTrue(t.GetFalling(false));
            t.Update(false);
            Assert.IsTrue(t.GetRising(true));
        }
    }
}
