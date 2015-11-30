using System;
using NUnit.Framework;
using CSharpRoboticsLib.FlowControl;

namespace IndependentTests.FlowControlTests
{
    [TestFixture]
    public class EdgeTriggerTest
    {
        [Test]
        public void AutoUpdateRisingToFalling()
        {
            EdgeTrigger t = new EdgeTrigger();
            t.Update(false);
            Assert.IsTrue(t.GetRisingUpdate(true));
            Assert.IsTrue(t.GetFallingUpdate(false));
        }

        [Test]
        public void AutoUpdateFallingToRising()
        {
            EdgeTrigger t = new EdgeTrigger();
            t.Update(true);
            Assert.IsTrue(t.GetFallingUpdate(false));
            Assert.IsTrue(t.GetRisingUpdate(true));
        }

        [Test]
        public void ManualUpdateRisingToFalling()
        {
            EdgeTrigger t = new EdgeTrigger();
            t.Update(false);
            Assert.IsTrue(t.GetRising(true));
            t.Update(true);
            Assert.IsTrue(t.GetFalling(false));
        }

        [Test]
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
