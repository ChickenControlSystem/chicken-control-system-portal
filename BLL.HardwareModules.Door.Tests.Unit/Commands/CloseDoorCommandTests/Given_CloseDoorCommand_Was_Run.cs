using BLL.Common.Contract;
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
        protected IValidateOperationService MockErrorValidateOperationService;
        private IDoor _doorAxis;
        private IFloorSensor _floorSensor;

        public override void Given()
        {
            MockAxisOperations = Substitute.For<IAxisOperations>();
            MockErrorValidateOperationService = Substitute.For<IValidateOperationService>();
            _doorAxis = new DoorAxis();
            _floorSensor = new FloorDigitalSensor();

            SUT = new CloseDoorCommand(
                MockAxisOperations,
                MockErrorValidateOperationService,
                _doorAxis,
                _floorSensor
            );
        }
    }
}