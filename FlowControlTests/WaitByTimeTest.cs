using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpRoboticsLib.FlowControl;
using System.Threading;

namespace FlowControlTests
{
    [TestClass]
    public class WaitByTimeTest
    {
        [TestMethod]
        public void WaitCompleteTest()
        {
            WaitByTime t = new WaitByTime(100);
            Assert.IsFalse(t.DoneWaiting);
            t.Reset();
            Assert.IsFalse(t.DoneWaiting);
            Thread.Sleep(100);
            Assert.IsTrue(t.DoneWaiting);
        }
    }
}
