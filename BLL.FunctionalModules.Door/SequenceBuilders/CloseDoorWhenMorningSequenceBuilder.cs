using System;
using BLL.Common.Contract;
using BLL.FunctionalModules.Door.Contract;
using BLL.HardwareModules.Door.Contract;
using BLL.HardwareModules.Light.Contract;
using Common.DateTime;

namespace BLL.FunctionalModules.Door.SequenceBuilders
{
    //TODO: refactor time stuff out
    public class CloseDoorWhenMorningSequenceBuilder : ICloseDoorWhenMorningSequenceBuilder
    {
        private readonly IDelay _delay;
        private readonly ITimeService _timeService;
        private readonly IFluentSequenceBuilder _fluentSequenceBuilder;
        private readonly ICheckForLightCommand _checkForLightCommand;
        private readonly IOpenDoorCommand _openDoorCommand;

        public CloseDoorWhenMorningSequenceBuilder(IFluentSequenceBuilder fluentSequenceBuilder, IDelay delay,
            ICheckForLightCommand checkForLightCommand, IOpenDoorCommand openDoorCommand, ITimeService timeService)
        {
            _fluentSequenceBuilder = fluentSequenceBuilder;
            _delay = delay;
            _checkForLightCommand = checkForLightCommand;
            _openDoorCommand = openDoorCommand;
            _timeService = timeService;
        }

        public ISequence Build()
        {
            var currentTime = _timeService.MilisecondsNow();

            var monitorLightConditionBeforeMorningBeforeMidnight =
                currentTime > _timeService.MilisecondsInTimeSpan(new TimeSpan(16, 0, 0));

            var monitorLightConditionBeforeMorningAfterMidnight =
                currentTime < _timeService.MilisecondsInTimeSpan(new TimeSpan(6, 0, 0));

            var monitorLightConditionBeforeMorning =
                monitorLightConditionBeforeMorningBeforeMidnight || monitorLightConditionBeforeMorningAfterMidnight;

            var monitorLightConditionAfterMorning =
                currentTime > _timeService.MilisecondsInTimeSpan(new TimeSpan(6, 0, 0)) &&
                currentTime < _timeService.MilisecondsInTimeSpan(new TimeSpan(8, 0, 0));

            double delayTime = 0;

            if (monitorLightConditionBeforeMorningBeforeMidnight)
            {
                _checkForLightCommand.SetRunCountBeforeMorning(12);
                delayTime = (_timeService.MilisecondsInTimeSpan(new TimeSpan(24, 0, 0)) - currentTime) + 21600000;
            }
            else if (monitorLightConditionBeforeMorningAfterMidnight)
            {
                _checkForLightCommand.SetRunCountBeforeMorning(12);
                delayTime = _timeService.MilisecondsInTimeSpan(new TimeSpan(6, 0, 0)) - currentTime;
            }
            else if (monitorLightConditionAfterMorning)
            {
                var timeDifference = _timeService.MilisecondsInTimeSpan(new TimeSpan(8, 0, 0)) - currentTime;

                var ceiling = Math.Ceiling(timeDifference / _timeService.MilisecondsInTimeSpan(new TimeSpan(0, 10, 0)));
                var runCount = (int) ceiling;

                if (runCount < 1)
                    runCount = 1;

                _checkForLightCommand.SetRunCountBeforeMorning(runCount);
            }

            _delay.WaitUntil(delayTime);

            return _fluentSequenceBuilder
                .QueueConditional(_delay, monitorLightConditionBeforeMorning)
                .QueueConditional(_checkForLightCommand,
                    monitorLightConditionBeforeMorning || monitorLightConditionAfterMorning)
                .Queue(_openDoorCommand)
                .Serial()
                .Fail(() => { })
                .Build();
        }
    }
}