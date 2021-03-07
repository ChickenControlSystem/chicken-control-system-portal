using System;
using Crosscutting.Contract.HAL.Enum;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.Sequence;

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