using System;
using NUnit.Framework;
using CSharpRoboticsLib.FlowControl;
using System.Threading;

namespace IndependentTests.FlowControlTests
{
    [TestFixture]
    public class WaitByTimeTest
    {
        [Test]
        public void WaitCompleteTest()
        {
            WaitByTime t = new WaitByTime(100);
            Assert.IsFalse(t.DoneWaiting);
            t.Reset();
            Assert.IsFalse(t.DoneWaiting);
            Thread.Sleep(125);
            Assert.IsTrue(t.DoneWaiting);
        }
    }
}
