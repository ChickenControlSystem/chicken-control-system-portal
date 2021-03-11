using BLL.HardwareModules.Door.Commands;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing;
using Bootstrapping.Services.Contract.HAL.Interface;
using Crosscutting.UnitTest;
using HAL.Models.Device;
using NSubstitute;

namespace Tests.Unit.BLL.HardwareModules.Door.Commands.CloseDoorCommandTests
{
    public class Given_OpenDoorCommand : GenericGivenWhenThenTests<OpenDoorCommand>
    {
        protected IAxisOperations MockAxisOperations;
        protected IValidateOperationService MockErrorValidateOperationService;
        private IDoor _doorAxis;
        private ICeilingSensor _ceilingSensor;

        protected override void Given()
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