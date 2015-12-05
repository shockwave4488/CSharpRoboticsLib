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
        bool BasicMethodRun;

        [Test]
        public void SafeActionBasicTest()
        {
            SafeAction s = new SafeAction(SafeActionBasicMethod);
            BasicMethodRun = false;
            Assert.IsTrue(s.Run());
            Assert.IsTrue(BasicMethodRun);
        }

        private void SafeActionBasicMethod()
        {
            BasicMethodRun = true;
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

        static Thread t;

        [Test]
        public void SafeActionAsynchronousTest()
        {
            ChangeTrigger<bool> e = new ChangeTrigger<bool>();
            e.Update(false);
            SafeActionAsynchronousHelper();
            int ChangeCounts = 0;
            while (t.IsAlive && ChangeCounts < 10)
            {
                if (e.GetChangeUpdate(TestClass.GetState()))
                    ChangeCounts++;
            }
            Assert.AreEqual(10, ChangeCounts);
        }

        private void SafeActionAsynchronousHelper()
        {
            SafeAction s = new SafeAction(TestClass.UpdateState, 10);
            t = new Thread(new ThreadStart(() => { Assert.IsTrue(!s.Run()); }));
            t.Start();
        }

        private static class TestClass
        {
            private static bool state = false;
            public static bool GetState() => state;
            public static void UpdateState()
            {
                while(true)
                    state = DateTime.Now.Second % 2 == 0;
            }
        }
    }
}
