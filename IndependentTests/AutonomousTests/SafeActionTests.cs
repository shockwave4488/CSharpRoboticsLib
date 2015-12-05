using System;
using NUnit.Framework;
using CSharpRoboticsLib.Autonomous;
using CSharpRoboticsLib.FlowControl;
using System.Threading;

namespace ControlSystemsTests.AutonomousTests
{
    [TestFixture]
    public class SafeActionTests
    {
        bool m_basicMethodRun;

        [Test]
        public void SafeActionBasicTest()
        {
            SafeAction s = new SafeAction(SafeActionBasicMethod);
            m_basicMethodRun = false;
            Assert.IsTrue(s.Run());
            Assert.IsTrue(m_basicMethodRun);
        }

        private void SafeActionBasicMethod()
        {
            m_basicMethodRun = true;
        }

        [Test]
        public void SafeActionTimeoutTest()
        {
            SafeAction s = new SafeAction(SafeActionTimeoutMethod, 0.5);
            Assert.IsFalse(s.Run());
        }

        private void SafeActionTimeoutMethod()
        {
            SpinWait.SpinUntil(() => { return false; });
        }

        static Thread _t;

        [Test]
        public void SafeActionAsynchronousTest()
        {
            ChangeTrigger<bool> e = new ChangeTrigger<bool>();
            e.Update(false);
            SafeActionAsynchronousHelper();
            int changeCounts = 0;
            while (_t.IsAlive && changeCounts < 10)
            {
                if (e.GetChangeUpdate(TestClass.GetState()))
                    changeCounts++;
            }
            Assert.AreEqual(10, changeCounts);
        }

        private void SafeActionAsynchronousHelper()
        {
            SafeAction s = new SafeAction(TestClass.UpdateState, 10);
            _t = new Thread(new ThreadStart(() => { Assert.IsTrue(!s.Run()); }));
            _t.Start();
        }

        private static class TestClass
        {
            private static bool _state = false;
            public static bool GetState() => _state;
            public static void UpdateState()
            {
                while(true)
                    _state = DateTime.Now.Second % 2 == 0;
            }
        }
    }
}
