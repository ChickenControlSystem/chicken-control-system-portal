using System;
using BLL.Common.Contract;
using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;
using BLL.FunctionalModules.Door.SequenceBuilders;
using BLL.HardwareModules.Door.Contract;
using BLL.HardwareModules.Light.Contract;
using Common.DateTime;
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
        protected ITimeService MockTimeService;
        private TimeService _timeService;

        private void SetupTasks()
        {
            MockOpenDoorCommand
                .RecoveryOptions
                .Returns(new RecoveryOptionsDto());
            MockOpenDoorCommand
                .GetRunCount()
                .Returns(3);

            MockCheckForLightCommand
                .RecoveryOptions
                .Returns(new RecoveryOptionsDto());
            MockCheckForLightCommand
                .GetRunCount()
                .Returns(10);

            MockDelay
                .RecoveryOptions
                .Returns(new RecoveryOptionsDto());
            MockDelay
                .GetRunCount()
                .Returns(1);
            MockDelay
                .Run()
                .Returns(SequenceResultEnum.Success);
        }

        //TODO: add to Common.Testing => partial mocks
        public override void Given()
        {
            base.Given();

            _timeService = new TimeService();
            MockTimeService = Substitute.For<ITimeService>();
            MockOpenDoorCommand = Substitute.For<IOpenDoorCommand>();
            MockDelay = Substitute.For<IDelay>();
            MockCheckForLightCommand = Substitute.For<ICheckForLightCommand>();

            SetupTasks();

            MockTimeService
                .MilisecondsInTimeSpan(Arg.Any<TimeSpan>())
                .Returns(args =>
                {
                    var time = (TimeSpan) args[0];
                    return _timeService.MilisecondsInTimeSpan(time);
                });

            SUT = new CloseDoorWhenMorningSequenceBuilder(
                FluentSequenceBuilder,
                MockDelay,
                MockCheckForLightCommand,
                MockOpenDoorCommand,
                MockTimeService
            );
        }
    }
}