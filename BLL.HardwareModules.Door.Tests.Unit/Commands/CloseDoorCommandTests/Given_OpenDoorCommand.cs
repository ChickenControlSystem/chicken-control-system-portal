using BLL.HardwareModules.Door.Commands;
using Crosscutting.Sequencing.Contract;
using Crosscutting.UnitTest;
using HAL.Models.Contract;
using HAL.Models.Device;
using HAL.Operations.Contract;
using NSubstitute;

namespace BLL.HardwareModules.Door.Tests.Unit.Commands.CloseDoorCommandTests
{
    public class Given_OpenDoorCommand : GenericGivenWhenThenTests<OpenDoorCommand>
    {
        protected IAxisOperations MockAxisOperations;
        protected IValidateOperationService MockErrorValidateOperationService;
        private IDoor _doorAxis;
        private ICeilingSensor _ceilingSensor;

        public override void Given()
        {
            MockAxisOperations = Substitute.For<IAxisOperations>();
            MockErrorValidateOperationService = Substitute.For<IValidateOperationService>();
            _doorAxis = new DoorAxis();
            _ceilingSensor = new CeilingDigitalSensor();

            SUT = new OpenDoorCommand(
                MockAxisOperations,
                MockErrorValidateOperationService,
                _doorAxis,
                _ceilingSensor
            );
        }
    }
}