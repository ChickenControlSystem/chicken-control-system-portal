using ControlLine.Contract;
using ControlLine.Dto;
using HAL.Models.Contract;
using HAL.Operations.Contract;
using HAL.Operations.Dto;

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