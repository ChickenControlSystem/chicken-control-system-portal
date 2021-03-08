﻿using Crosscutting.Services.Contract.BLL.Interface;
using Crosscutting.Services.Contract.Crosscutting.Dto.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Enum.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Interface.Utilities;
using Crosscutting.Services.Contract.HAL.Interface;

namespace BLL.HardwareModules.Light.Commands
{
    public class CheckForLightCommand : ICheckForLightCommand
    {
        private readonly IAnalogOperations _analogOperations;
        private readonly IValidateOperationService _validateOperationService;
        private readonly ILightSensor _lightSensor;
        private readonly IThreadOperations _threadOperations;
        private int _runCount;


        public RecoveryOptionsDto RecoveryOptions { get; }

        public CheckForLightCommand(IValidateOperationService validateOperationService,
            IAnalogOperations analogOperations, ILightSensor lightSensor, IThreadOperations threadOperations)
        {
            _validateOperationService = validateOperationService;
            _analogOperations = analogOperations;
            _lightSensor = lightSensor;
            _threadOperations = threadOperations;

            RecoveryOptions = new RecoveryOptionsDto(true, Recover);
        }

        public SequenceResultEnum Run()
        {
            var hardwareResult = _analogOperations.Read(_lightSensor);

            var hardwareStatus = _validateOperationService.GetSequenceResult(hardwareResult.ResultStatus);

            if (hardwareStatus == SequenceResultEnum.Success)
            {
                if (hardwareResult.Return >= 100)
                    return SequenceResultEnum.Success;
            }

            _threadOperations.SyncronousDelay(600000);
            return SequenceResultEnum.Fail;
        }

        public void HandleFail()
        {
            //TODO: log error
        }

        private static SequenceResultEnum Recover()
        {
            //TODO: report error to UI
            //TODO: log error
            return SequenceResultEnum.Success;
        }

        public void SetRunCountBeforeMorning(int runCount)
        {
            _runCount = runCount;
        }

        public int GetRunCount()
        {
            return _runCount;
        }
    }
}