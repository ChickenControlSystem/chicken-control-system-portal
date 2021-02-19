using BLL.HardwareModules.Common.Command;
using BLL.HardwareModules.Door.Commands;
using HAL.Models.Contract;
using HAL.Models.Device;
using HAL.Operations.Contract;
using NSubstitute;
using UnitTest;

namespace BLL.HardwareModules.Door.Tests.Unit.Commands.CloseDoorCommandTests
{
    public class Given_CloseDoorCommand_Was_Run : GenericGivenWhenThenTests<CloseDoorCommand>
    {
        protected IAxisOperations MockAxisOperations;
        protected readonly IDevice DoorAxis = new DoorAxis();
        protected readonly IDevice FloorSensor = new FloorDigitalSensor();

        protected override void Given()
        {
            MockAxisOperations = Substitute.For<IAxisOperations>();

            SUT = new CloseDoorCommand(MockAxisOperations, new ValidateOperationService());
        }
    }
}