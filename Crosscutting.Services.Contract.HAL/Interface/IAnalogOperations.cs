using Crosscutting.Services.Contract.HAL.Dto;

namespace Crosscutting.Services.Contract.HAL.Interface
{
    /// <summary>
    /// takes care of the operations to do with analog inputs, outputs
    /// </summary>
    public interface IAnalogOperations
    {
        /// <summary>
        ///     used to read an analog input
        /// </summary>
        OperationResult Read(IDevice input);
    }
}