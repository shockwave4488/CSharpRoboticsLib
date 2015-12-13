using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HAL.Simulator;
using WPILib;
using WPILib.LiveWindow;

namespace SimulatorTests
{
    public abstract class SimTestBase
    {
        static SimTestBase()
        {
            RobotBase.InitializeHardwareConfiguration();
            HAL.Base.HAL.HALNetworkCommunicationObserveUserProgramStarting();

            LiveWindow.SetEnabled(false);

            DriverStationHelper.StartDSLoop();

            DriverStationHelper.SetRobotMode(DriverStationHelper.RobotMode.Teleop);
            DriverStationHelper.SetEnabledState(DriverStationHelper.EnabledState.Enabled);

            Thread.Sleep(500);
        }
    }
}
