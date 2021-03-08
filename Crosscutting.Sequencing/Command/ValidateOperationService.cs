using System;
using Crosscutting.Services.Contract.Crosscutting.Enum.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing;
using Crosscutting.Services.Contract.HAL.Enum;

namespace Crosscutting.Sequencing.Command
{
    public class ValidateOperationService : IValidateOperationService
    {
        public SequenceResultEnum GetSequenceResult(OperationResultEnum operationResult)
        {
            return operationResult switch
            {
                OperationResultEnum.Failiure => SequenceResultEnum.Fail,
                OperationResultEnum.Succeess => SequenceResultEnum.Success,
                _ => throw new ArgumentException()
            };
        }
    }
}