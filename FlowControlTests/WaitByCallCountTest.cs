using System;
using NUnit.Framework;
using CSharpRoboticsLib.FlowControl;

namespace FlowControlTests
{
    [TestFixture]
    public class WaitByCallCountTest
    {
        [Test]
        public void WaitCompleteTest()
        {
            WaitByCallCount t = new WaitByCallCount(2);
            Assert.IsFalse(t.WaitComplete);
            t.Update();
            t.Update();
            Assert.IsTrue(t.WaitComplete);
        }
    }
}
