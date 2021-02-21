using BLL.Common.Contract;
using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;
using BLL.HardwareModules.Light.Contract;
using HAL.Models.Contract;
using HAL.Operations.Contract;

namespace BLL.HardwareModules.Light.Commands
{
    public class CheckForMorningCommand : ICheckForMorningCommand
    {
        private readonly IAnalogOperations _analogOperations;
        private readonly IValidateOperationService _validateOperationService;
        private readonly ILightSensor _lightSensor;

        public int RunCount { get; }
        public RecoveryOptionsDto RecoveryOptions { get; }

        public CheckForMorningCommand(IValidateOperationService validateOperationService,
            IAnalogOperations analogOperations, ILightSensor lightSensor)
        {
            _validateOperationService = validateOperationService;
            _analogOperations = analogOperations;
            _lightSensor = lightSensor;

            RunCount = 1;
            RecoveryOptions = new RecoveryOptionsDto(true, Recover);
        }

        public SequenceResultEnum Run()
        {
            throw new System.NotImplementedException();
        }

        public void HandleFail()
        {
            throw new System.NotImplementedException();
        }

        private static SequenceResultEnum Recover()
        {
            throw new System.NotImplementedException();
        }
    }
}