using System;
using NUnit.Framework;
using CSharpRoboticsLib.FlowControl;

namespace FlowControlTests
{
    [TestFixture]
    public class ChangeTriggerTest
    {
        [Test]
        public void ChangeDetectedString()
        {
            ChangeTrigger<string> s = new ChangeTrigger<string>();
            Assert.IsTrue(s.GetChange("This is a Test"));
            Assert.IsTrue(s.GetChangeUpdate("This is a Test"));
            Assert.IsFalse(s.GetChange("This is a Test"));
        }

        [Test]
        public void ChangeDetectedInt()
        {
            ChangeTrigger<int> s = new ChangeTrigger<int>();
            Assert.IsTrue(s.GetChange(1));
            Assert.IsTrue(s.GetChangeUpdate(1));
            Assert.IsFalse(s.GetChange(1));
        }
    }
}
