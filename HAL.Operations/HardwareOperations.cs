using Crosscutting.Contract.HAL.Dto;
using Crosscutting.Contract.HAL.Enum;
using Crosscutting.Contract.HAL.Interface;

namespace HAL.Operations
{
    public abstract class HardwareOperations
    {
        protected readonly IControlLine ControlLine;
        protected readonly IErrorService ErrorService;

        protected HardwareOperations(IErrorService errorService, IControlLine controlLine)
        {
            ErrorService = errorService;
            ControlLine = controlLine;
        }

        protected OperationResultEnum SendOperation(OperationDto operation)
        {
            var result = ControlLine.SendOperation(operation);
            return ErrorService.Validate(result.Status);
        }
    }
}