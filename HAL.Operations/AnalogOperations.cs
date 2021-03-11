using Bootstrapping.Services.Contract.HAL.Dto;
using Bootstrapping.Services.Contract.HAL.Interface;

namespace HAL.Operations
{
    public class AnalogOperations : HardwareOperations, IAnalogOperations
    {
        public AnalogOperations(IErrorService errorService, IControlLine controlLine) : base(errorService, controlLine)
        {
        }

        public OperationResult Read(IDevice input)
        {
            var operationResult = ControlLine.SendOperation(
                new OperationDto
                {
                    Device = input.Id,
                    Operation = 1,
                    Params = new int[] { }
                }
            );
            var result = ErrorService.Validate(operationResult.Status);
            return new OperationResult
            {
                Return = operationResult.Returns,
                ResultStatus = result
            };
        }
    }
}