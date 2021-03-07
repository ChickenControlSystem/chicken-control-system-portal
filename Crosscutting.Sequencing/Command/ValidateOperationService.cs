using System;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.Sequence;
using HAL.Operations.Enum;

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