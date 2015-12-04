using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL.Simulator;
using HAL.Simulator.Inputs;
using HAL.Simulator.Mechanisms;
using CSharpRoboticsLib.Extras;
using NUnit.Framework;

namespace IndependentTests.ExtrasTests
{
    [TestFixture]
    public class AnalogueUltrasonicTest
    {
        [Test]
        public void TestAnalogGetRange([Range(0, 0.5, 0.1)] double range)
        {
            using (AnalogueUltrasonic ultra = new AnalogueUltrasonic(1, range))
            {
                var data = SimData.AnalogIn[1];

                SimAnalogInput in1 = new SimAnalogInput(1);
                in1.GetVoltage();
                

                data.Voltage = 12345;
            }
        }
    }
}
