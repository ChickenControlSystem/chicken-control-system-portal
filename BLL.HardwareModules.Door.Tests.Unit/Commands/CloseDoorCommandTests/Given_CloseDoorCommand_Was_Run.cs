using BLL.HardwareModules.Common.Contract;
using BLL.HardwareModules.Door.Commands;
using HAL.Models.Contract;
using HAL.Operations.Contract;
using NSubstitute;
using UnitTest;

namespace BLL.HardwareModules.Door.Tests.Unit.Commands.CloseDoorCommandTests
{
    public class Given_CloseDoorCommand_Was_Run : GenericGivenWhenThenTests<CloseDoorCommand>
    {
        protected IAxisOperations MockAxisOperations;
        protected IValidateOperationService MockErrorValidateOperationService;
        protected IDoor MockDoorAxis;
        protected IFloorSensor MockFloorSensor;

        protected override void Given()
        {
            MockAxisOperations = Substitute.For<IAxisOperations>();
            MockErrorValidateOperationService = Substitute.For<IValidateOperationService>();
            MockDoorAxis = Substitute.For<IDoor>();
            MockFloorSensor = Substitute.For<IFloorSensor>();

            SUT = new CloseDoorCommand(
                MockAxisOperations,
                MockErrorValidateOperationService,
                MockFloorSensor,
                MockDoorAxis
            );
        }
    }
}