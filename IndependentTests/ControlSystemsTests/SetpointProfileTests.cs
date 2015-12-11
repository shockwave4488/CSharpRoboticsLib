using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpRoboticsLib.ControlSystems;
using NUnit.Framework;

namespace IndependentTests.ControlSystemsTests
{
    [TestFixture]
    public class SetpointProfileTests
    {
        [Test]
        public static void SetpointProfileTest()
        {
            SetpointProfile p = new SetpointProfile();
            p.Add(0, 0);
            p.Add(1, 1);
            p.SetPoint = 1;

            Assert.AreEqual(0.1, p.Get(0.1));
            Assert.AreEqual(0.9, p.Get(0.9));
            Assert.AreEqual(0.5, p.Get(0.5));
            Assert.AreEqual(1, p.Get(1.5));

            //Reverse
            p.SetPoint = 0;

            Assert.AreEqual(0, p.Get(0));
            Assert.AreEqual(-1, p.Get(1));
            Assert.AreEqual(-0.5, p.Get(0.5));
        }
    }
}
