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
            t.state = true;
            Assert.IsTrue(t.state);
            t.state = true;
            Assert.IsTrue(t.state);
            t.state = false;
            Assert.IsTrue(t.state);
            t.state = true;
            Assert.IsFalse(t.state);
            t.state = true;
            Assert.IsFalse(t.state);
            t.state = false;
            Assert.IsFalse(t.state);
            t.state = false;
            Assert.IsFalse(t.state);
        }

        [Test]
        public void ToggleForceTest()
        {
            //Also checking that force doesn't interfere with toggle
            Toggle t = new Toggle();
            t.state = false;
            t.Force(true);
            Assert.IsTrue(t.state);
            t.state = false;
            Assert.IsTrue(t.state);
            t.state = true;
            Assert.IsFalse(t.state);
            t.Force(false);
            Assert.IsFalse(t.state);
        }
    }
}
