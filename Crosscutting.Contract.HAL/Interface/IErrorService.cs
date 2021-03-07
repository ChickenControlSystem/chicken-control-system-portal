using Crosscutting.Contract.HAL.Enum;

namespace Crosscutting.Contract.HAL.Interface
{
    /// <summary>
    ///     handles control line errors
    /// </summary>
    public interface IErrorService
    {
        /// <summary>
        ///     returns success, failure depending on error, logs error
        /// </summary>
        OperationResultEnum Validate(byte errorCode);
    }
}