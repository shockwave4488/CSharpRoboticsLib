using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.SmartDashboard;
using CSharpRoboticsLib.Utility;
using NUnit.Framework;

namespace IndependentTests.UtilityTests
{
    [TestFixture]
    public class LoggerTests
    {
        [Test]
        public static void AddMessageTest()
        {
            Logger.Clear();
            Logger.AddMessage("Hello World!");
            Assert.AreNotEqual("", Logger.ToString());

            //For manual checking
            string s = Logger.ToString();
        }

        [Test]
        public static void SmartDashboardTest()
        {
            //I know I need to initialize the SmartDashboard somehow... not sure how though.
            Logger.Clear();
            string var = "TEST";
            //Logger.SmartDashboardVariable = var;
            Logger.AddMessage("Hello SmartDashboard!");

            // I know its deprecated, but I'm not sure what to use in its place.
            // SmartDashboard.GetData() returns an ISendable, which I don't know how to use.
            //Assert.IsNotNull(SmartDashboard.GetString(var));
            //Assert.AreNotEqual("", SmartDashboard.GetString(var));
        }

        [Test] //This is just going to be a manual test - me looking at values making sure it works.
        public static void TimerResetTest()
        {
            Logger.Clear();
            Logger.AddMessage("Before Reset");
            Util.AccurateWaitMilliseconds(1000);
            Logger.ResetTimer();
            Logger.AddMessage("After Reset");

            string s = Logger.ToString();
        }
    }
}
