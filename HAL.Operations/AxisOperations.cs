using System;
using ControlLine.Contract;
using ControlLine.Dto;
using HAL.Models.Contract;
using HAL.Operations.Contract;
using HAL.Operations.Enum;

namespace HAL.Operations
{
    public class AxisOperations : IAxisOperations
    {
        private readonly IControlLine _controlLine;
        private readonly IErrorService _errorService;

        public AxisOperations(IErrorService errorService, IControlLine controlLine)
        {
            _errorService = errorService;
            _controlLine = controlLine;
        }

        public OperationResultEnum MoveAxisAbsolute(IDevice axis, int ammount)
        {
            return GenericAxisMove(axis, ammount, 2);
        }

        public OperationResultEnum MoveAxisRelative(IDevice axis, int ammount)
        {
            return GenericAxisMove(axis, ammount, 3);
        }

        public OperationResultEnum MoveAxisSearch(IDevice axis, IDevice sensor)
        {
            throw new NotImplementedException();
        }

        private OperationResultEnum GenericAxisMove(IDevice axis, int ammount, byte operationId)
        {
            var result = _controlLine.SendOperation(new OperationDto
                {Device = axis.Id, Operation = operationId, Params = new[] {ammount}});
            return _errorService.Validate(result.Status);
        }
    }
}