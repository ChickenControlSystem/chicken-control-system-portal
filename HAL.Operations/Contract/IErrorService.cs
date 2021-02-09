using HAL.Operations.Enum;

namespace HAL.Operations.Contract
{
    /// <summary>
    /// handles control line errors
    /// </summary>
    public interface IErrorService
    {
        /// <summary>
        /// returns success, failure depending on error, logs error
        /// </summary>
        OperationResultEnum Validate(int errorCode);
    }
}