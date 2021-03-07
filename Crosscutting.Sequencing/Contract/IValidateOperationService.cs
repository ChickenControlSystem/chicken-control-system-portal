using System;
using Crosscutting.Contract.HAL.Enum;
using Crosscutting.Sequencing.Sequence;

namespace Crosscutting.Sequencing.Contract
{
    public interface IValidateOperationService
    {
        /// <exception cref="ArgumentException"></exception>
        public SequenceResultEnum GetSequenceResult(OperationResultEnum operationResult);
    }
}