using HAL.ControlLine.Contract;
using HAL.ControlLine.Dto;
using HAL.Models.Contract;
using HAL.Operations.Contract;
using HAL.Operations.Enum;

namespace HAL.Operations
{
    public class AxisOperations : HardwareOperations, IAxisOperations
    {
        public AxisOperations(IErrorService errorService, IControlLine controlLine) : base(errorService, controlLine)
        {
        }

        public OperationResultEnum MoveAxisAbsolute(IDevice axis, int ammount)
        {
            return GenericAxisMove(axis, ammount, 2);
        }

        public OperationResultEnum MoveAxisRelative(IDevice axis, int ammount)
        {
            return GenericAxisMove(axis, ammount, 3);
        }

        public OperationResultEnum MoveAxisSearch(IDevice axis, IDevice sensor, bool direction)
        {
            return SendOperation(new OperationDto
            {
                Device = axis.Id,
                Operation = 4,
                Params = new[]
                {
                    sensor.Id,
                    direction ? 1 : 0
                }
            });
        }

        private OperationResultEnum GenericAxisMove(IDevice axis, int ammount, byte operationId)
        {
            return SendOperation(new OperationDto
            {
                Device = axis.Id,
                Operation = operationId,
                Params = new[]
                {
                    ammount
                }
            });
        }
    }
}