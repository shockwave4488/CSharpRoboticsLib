using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpRoboticsLib.FlowControl;

namespace FlowControlTests
{
    [TestClass]
    public class ChangeTriggerTest
    {
        [TestMethod]
        public void ChangeDetectedString()
        {
            ChangeTrigger<string> s = new ChangeTrigger<string>();
            Assert.IsTrue(s.GetChange("This is a Test"));
            Assert.IsTrue(s.GetChangeUpdate("This is a Test"));
            Assert.IsFalse(s.GetChange("This is a Test"));
        }

        [TestMethod]
        public void ChangeDetectedInt()
        {
            ChangeTrigger<int> s = new ChangeTrigger<int>();
            Assert.IsTrue(s.GetChange(1));
            Assert.IsTrue(s.GetChangeUpdate(1));
            Assert.IsFalse(s.GetChange(1));
        }
    }
}
