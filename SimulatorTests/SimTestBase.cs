using System.Threading;
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
