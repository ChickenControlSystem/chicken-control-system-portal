using HAL.Models.Contract;
using HAL.Operations.Enum;

namespace HAL.Operations.Contract
{
    public interface IAxisOperations
    {
        public OperationResultEnum MoveAxisAbsolute(IDevice axis, int ammount);

        public OperationResultEnum MoveAxisRelative(IDevice axis, int ammount);

        public OperationResultEnum MoveAxisSearch(IDevice axis, IDevice sensor);
    }
}