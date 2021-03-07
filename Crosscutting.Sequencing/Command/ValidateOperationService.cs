using System;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.Sequence;
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