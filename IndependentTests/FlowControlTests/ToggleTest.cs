using System;
using NUnit.Framework;
using CSharpRoboticsLib.FlowControl;

namespace IndependentTests.FlowControlTests
{
    [TestFixture]
    public class ToggleTest
    {
        [Test]
        public void ToggleOnOffTest()
        {
            //Also checks edge detection
            Toggle t = new Toggle();
            t.State = true;
            Assert.IsTrue(t.State);
            t.State = true;
            Assert.IsTrue(t.State);
            t.State = false;
            Assert.IsTrue(t.State);
            t.State = true;
            Assert.IsFalse(t.State);
            t.State = true;
            Assert.IsFalse(t.State);
            t.State = false;
            Assert.IsFalse(t.State);
            t.State = false;
            Assert.IsFalse(t.State);
        }

        [Test]
        public void ToggleForceTest()
        {
            //Also checking that force doesn't interfere with toggle
            Toggle t = new Toggle();
            t.State = false;
            t.Force(true);
            Assert.IsTrue(t.State);
            t.State = false;
            Assert.IsTrue(t.State);
            t.State = true;
            Assert.IsFalse(t.State);
            t.Force(false);
            Assert.IsFalse(t.State);
        }
    }
}
