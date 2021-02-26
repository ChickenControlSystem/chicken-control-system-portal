using BLL.Common.Contract;
using BLL.FunctionalModules.Door.SequenceBuilders;
using BLL.HardwareModules.Door.Contract;
using BLL.HardwareModules.Light.Contract;
using NSubstitute;
using UnitTest;

namespace BLL.FunctionalModules.Door.Tests.Integration.SequenceBuilders.CloseDoorWhenMorningSequenceBuilderTests
{
    public class
        Given_CloseDoorWhenMorningSequenceBuilder : GivenWhenThenSequenceBuilderTests<
            CloseDoorWhenMorningSequenceBuilder>
    {
        protected ICheckForLightCommand MockCheckForLightCommand;
        protected IDelay MockDelay;
        protected IOpenDoorCommand MockOpenDoorCommand;

        public override void When()
        {
            base.When();

            MockOpenDoorCommand = Substitute.For<IOpenDoorCommand>();
            MockDelay = Substitute.For<IDelay>();
            MockCheckForLightCommand = Substitute.For<ICheckForLightCommand>();

            SUT = new CloseDoorWhenMorningSequenceBuilder(
                FluentSequenceBuilder,
                MockDelay,
                MockCheckForLightCommand,
                MockOpenDoorCommand
            );
        }
    }
}