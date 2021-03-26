using BLL.HardwareModules.Light.Commands;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Utils;
using Bootstrapping.Services.Contract.HAL.Interface;
using Crosscutting.UnitTest;
using NSubstitute;

namespace Tests.Unit.BLL.HardwareModules.Light.Commands.CheckForLightCommandTests
{
    public class Given_CheckForLightCommand : GenericGivenWhenThenTests<CheckForLightCommand>
    {
        protected IValidateOperationService MockValidateOperationService;
        protected IAnalogOperations MockAnalogOperations;
        protected ILightSensor MockLightSensor;
        protected ITimeService MockTimeService;
        protected IThreadOperations MockThreadingOperations;

        protected override void Given()
        {
            MockValidateOperationService = Substitute.For<IValidateOperationService>();
            MockAnalogOperations = Substitute.For<IAnalogOperations>();
            MockLightSensor = Substitute.For<ILightSensor>();
            MockThreadingOperations = Substitute.For<IThreadOperations>();

            SUT = new CheckForLightCommand(
                MockValidateOperationService,
                MockAnalogOperations,
                MockLightSensor,
                MockThreadingOperations
            );
        }
    }
}