using System;
using BLL.HardwareModules.Common.Sequence;
using HAL.Operations.Enum;

namespace BLL.HardwareModules.Common.Contract
{
    public interface IValidateOperationService
    {
        /// <exception cref="ArgumentException"></exception>
        public SequenceResultEnum GetSequenceResult(OperationResultEnum operationResult);
    }
}