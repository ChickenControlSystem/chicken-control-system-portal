using System;
using BLL.Common.Sequence;
using HAL.Operations.Enum;

namespace BLL.Common.Contract
{
    public interface IValidateOperationService
    {
        /// <exception cref="ArgumentException"></exception>
        public SequenceResultEnum GetSequenceResult(OperationResultEnum operationResult);
    }
}