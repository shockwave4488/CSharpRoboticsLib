using HAL.Simulator.Inputs;
using HAL.Simulator.Mechanisms;
using HAL.Simulator.Outputs;

namespace SimulatorTests.AutonomousTests
{
    public abstract class DriveTestBase : SimTestBase
    {
        private static TankDriveTrainMechanism _drive;

        static DriveTestBase()
        {
            SimPWMController lMotor = new SimPWMController(0);
            SimPWMController rMotor = new SimPWMController(1);

            SimEncoder lEncoder = new SimEncoder(0);
            SimEncoder rEncoder = new SimEncoder(1);

            DriveWheelMechanism left = new DriveWheelMechanism(DCMotor.MakeCIM(), lMotor, 1, 1, lEncoder);
            DriveWheelMechanism right = new DriveWheelMechanism(DCMotor.MakeCIM(), rMotor, 1, 1, rEncoder);

            _drive = new TankDriveTrainMechanism(left, right, 11);
        }
    }
}