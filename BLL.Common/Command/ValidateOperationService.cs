using System;
using BLL.Common.Contract;
using BLL.Common.Sequence;
using HAL.Operations.Enum;

namespace BLL.Common.Command
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