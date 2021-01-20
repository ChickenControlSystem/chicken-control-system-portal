using ControlLine.Exception.Hardware;

namespace ControlLine.Contract
{
    /// <summary>
    /// validates the status from control line response
    /// </summary>
    public interface IControlLineStatusValidator
    {
        /// <summary>
        /// returns error based on status code
        /// </summary>
        public DeviceFailiure ValidateError(byte status);

        /// <summary>
        /// returns true if the status is an error
        /// returns false if the status is success
        /// </summary>
        public bool IsError(byte status);
    }
}