using System;
using ControlLine.Contract;
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

        public OperationResultEnum MoveAxisAbsolute(IDevice axis)
        {
            throw new NotImplementedException();
        }

        public OperationResultEnum MoveAxisRelative(IDevice axis)
        {
            throw new NotImplementedException();
        }

        public OperationResultEnum MoveAxisSearch(IDevice axis, IDevice sensor)
        {
            throw new NotImplementedException();
        }
    }
}