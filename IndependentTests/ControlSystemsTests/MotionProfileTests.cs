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
    public class MotionProfileTests
    {
        [Test]
        public static void ConstructorErrorTest()
        {
            MotionProfile m1 = new MotionProfile(10, 1, 1, 0.01);
            string s1 = m1.ToString();
            MotionProfile m2 = new MotionProfile(10, 10, 1, 0.01);
            string s2 = m2.ToString();
            MotionProfile m3 = new MotionProfile(-10, 1, 1, 0.01);
            string s3 = m3.ToString();
            MotionProfile m4 = new MotionProfile(-10, -1, -1, 0.01);
            string s4 = m4.ToString();
            MotionProfile m5 = new MotionProfile(10, -1, -1, 0.01);
            string s5 = m5.ToString();

            string s = (new MotionProfile(20, 10, 10, 0.02)).ToString();
            Assert.Fail();
        }
    }
}
