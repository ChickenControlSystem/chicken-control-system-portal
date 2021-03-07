using System;
using Crosscutting.Sequencing.Sequence;
using Crosscutting.Services.Contract.HAL.Enum;

namespace Crosscutting.Sequencing.Contract
{
    public interface IValidateOperationService
    {
        /// <exception cref="ArgumentException"></exception>
        public SequenceResultEnum GetSequenceResult(OperationResultEnum operationResult);
    }
}