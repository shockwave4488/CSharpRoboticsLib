using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.SmartDashboard;
using CSharpRoboticsLib.Utility;
using NetworkTables;
using NUnit.Framework;
using WPILib;
using WPILib.Internal;

namespace IndependentTests.UtilityTests
{
    [TestFixture]
    public class LoggerTests
    {
        [OneTimeSetUp]
        public static void Setup()
        {
            HLUsageReporting.Implementation = new HardwareHLUsageReporting();
            NetworkTable.SetNetworkIdentity("Robot");
            NetworkTable.SetServerMode();
            NetworkTable.GetTable("");
            NetworkTable.GetTable("LiveWindow").GetSubTable("~STATUS~").PutBoolean("LW Enabled", false);
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            NetworkTable.Shutdown();
        }

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
            Logger.SmartDashboardName = var;
            Logger.AddMessage("Hello SmartDashboard!");

            Assert.IsNotNull(SmartDashboard.GetString(var, null));
            Assert.AreNotEqual("", SmartDashboard.GetString(var, null));
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

        [Test] //Manual test
        public static void DetailsTest()
        {
            Logger.Clear();
            Logger.ShowDetails = true;
            Logger.AddMessage("Checking Details");

            string s = Logger.ToString();

            Logger.ShowDetails = false;
        }

        [Test]
        public static void LevelsTest()
        {
            Logger.Clear();
            Logger.Level = 1;
            Logger.AddMessage("This should not be shown");

            Assert.AreEqual("", Logger.ToString());
            Logger.Level = 0;
        }
    }
}
