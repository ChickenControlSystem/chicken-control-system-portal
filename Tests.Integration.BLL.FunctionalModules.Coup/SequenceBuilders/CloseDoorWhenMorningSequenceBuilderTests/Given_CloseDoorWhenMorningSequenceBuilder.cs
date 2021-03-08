using System;
using BLL.FunctionalModules.Coup.SequenceBuilders;
using BLL.HardwareModules.Door.Contract;
using BLL.HardwareModules.Light.Contract;
using Crosscutting.DateTime;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.Sequence;
using Crosscutting.Sequencing.TaskRecovery;
using Crosscutting.UnitTest;
using NSubstitute;

namespace Tests.Integration.BLL.FunctionalModules.Coup.SequenceBuilders.CloseDoorWhenMorningSequenceBuilderTests
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