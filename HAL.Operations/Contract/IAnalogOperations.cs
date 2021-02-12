using HAL.Models.Contract;
using HAL.Operations.Dto;

namespace HAL.Operations.Contract
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