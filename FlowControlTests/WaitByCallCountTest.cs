using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpRoboticsLib.FlowControl;

namespace FlowControlTests
{
    [TestClass]
    public class WaitByCallCountTest
    {
        [TestMethod]
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
