using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp_Robotics_Library.FlowControl;

namespace FlowControlTests
{
    [TestClass]
    public class ToggleTest
    {
        [TestMethod]
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

        [TestMethod]
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
