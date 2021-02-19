using System;
using BLL.HardwareModules.Common.Contract;
using BLL.HardwareModules.Common.Sequence;
using HAL.Operations.Enum;

namespace BLL.HardwareModules.Common.Command
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