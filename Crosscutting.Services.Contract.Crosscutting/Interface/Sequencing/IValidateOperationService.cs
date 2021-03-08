using System;
using Crosscutting.Services.Contract.Crosscutting.Enum.Sequencing;
using Crosscutting.Services.Contract.HAL.Enum;

namespace Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing
{
    public interface IValidateOperationService
    {
        /// <exception cref="ArgumentException"></exception>
        public SequenceResultEnum GetSequenceResult(OperationResultEnum operationResult);
    }
}