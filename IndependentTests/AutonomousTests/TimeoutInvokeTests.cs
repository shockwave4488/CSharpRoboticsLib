using System;
using System.Threading;
using CSharpRoboticsLib.Autonomous;
using NUnit.Framework;

namespace IndependentTests.AutonomousTests
{
    [TestFixture]
    public class SafeActionTests
    {
        private bool m_basicMethodRun;

        [Test]
        public void TimeoutInvokeTest()
        {
            Action a = TimeoutInvokeHelper;
            Assert.IsTrue(a.TryExecute(20));
            Assert.IsTrue(m_basicMethodRun);
        }

        private void TimeoutInvokeHelper()
        {
            m_basicMethodRun = true;
        }

        [Test]
        public void TimeoutInvokeTimeoutTest()
        {
            Action a = TimeoutInvokeTimeoutHelper;
            Assert.IsFalse(a.TryExecute(10));
        }

        private void TimeoutInvokeTimeoutHelper()
        {
            SpinWait.SpinUntil(() => false);
        }

        [Test]
        public void TimeoutInvokeReturnTest()
        {
            Func<int> f = TimeoutInvokeReturnHelper;
            int i;
            Assert.IsTrue(f.TryExecute(10, out i));
            Assert.AreEqual(i, 1);

            Assert.AreEqual(f.TryExecute(10), 1);
        }

        public int TimeoutInvokeReturnHelper()
        {
            return 1;
        }
    }
}
