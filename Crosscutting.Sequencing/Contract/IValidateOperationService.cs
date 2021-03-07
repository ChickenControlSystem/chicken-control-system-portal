using System;
using Crosscutting.Sequencing.Sequence;
using HAL.Operations.Enum;

namespace Crosscutting.Sequencing.Contract
{
    public interface IValidateOperationService
    {
        /// <exception cref="ArgumentException"></exception>
        public SequenceResultEnum GetSequenceResult(OperationResultEnum operationResult);
    }
}