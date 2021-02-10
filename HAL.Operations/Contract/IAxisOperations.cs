using HAL.Models.Contract;
using HAL.Operations.Enum;

namespace HAL.Operations.Contract
{
    /// <summary>
    /// contains operations for motors (axis)
    /// </summary>
    public interface IAxisOperations
    {
        /// <summary>
        /// moves axis to known position, relative to it's 0 position
        /// </summary>
        public OperationResultEnum MoveAxisAbsolute(IDevice axis, int ammount);

        /// <summary>
        /// moves axis relative to it's current position
        /// </summary>
        public OperationResultEnum MoveAxisRelative(IDevice axis, int ammount);

        /// <summary>
        /// moves axis in direction until sensor flag is reached
        /// </summary>
        public OperationResultEnum MoveAxisSearch(IDevice axis, IDevice sensor, bool direction);
    }
}