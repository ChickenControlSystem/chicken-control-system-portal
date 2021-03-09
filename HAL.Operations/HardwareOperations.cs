using Bootstrapping.Services.Contract.HAL.Dto;
using Bootstrapping.Services.Contract.HAL.Enum;
using Bootstrapping.Services.Contract.HAL.Interface;

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