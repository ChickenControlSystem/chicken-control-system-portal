using System;
using BLL.Common.Contract;
using BLL.FunctionalModules.Door.Contract;
using BLL.HardwareModules.Door.Contract;
using BLL.HardwareModules.Light.Contract;

namespace BLL.FunctionalModules.Door.SequenceBuilders
{
    public class CloseDoorWhenMorningSequenceBuilder : ICloseDoorWhenMorningSequenceBuilder
    {
        private readonly IDelay _delay;
        private readonly IFluentSequenceBuilder _fluentSequenceBuilder;
        private readonly ICheckForLightCommand _checkForLightCommand;
        private readonly IOpenDoorCommand _openDoorCommand;

        public CloseDoorWhenMorningSequenceBuilder(IFluentSequenceBuilder fluentSequenceBuilder, IDelay delay,
            ICheckForLightCommand checkForLightCommand, IOpenDoorCommand openDoorCommand)
        {
            _fluentSequenceBuilder = fluentSequenceBuilder;
            _delay = delay;
            _checkForLightCommand = checkForLightCommand;
            _openDoorCommand = openDoorCommand;
        }

        public ISequence Build()
        {
            throw new NotImplementedException();
        }
    }
}