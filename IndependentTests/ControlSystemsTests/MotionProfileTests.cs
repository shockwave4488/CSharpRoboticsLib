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
            //Mostly just checking for OutOfMemoryExceptions, which would indicate that
            //distance was never reached.

            MotionProfile m1 = new MotionProfile(10, 1, 1, 0.01);
            MotionProfile m2 = new MotionProfile(10, 10, 1, 0.01);
            MotionProfile m3 = new MotionProfile(-10, 1, 1, 0.01);
            MotionProfile m4 = new MotionProfile(-10, -1, -1, 0.01);
            MotionProfile m5 = new MotionProfile(10, -1, -1, 0.01);
        }
    }
}
