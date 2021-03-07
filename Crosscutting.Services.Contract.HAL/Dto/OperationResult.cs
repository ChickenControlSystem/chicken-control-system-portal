using Crosscutting.Services.Contract.HAL.Enum;

namespace Crosscutting.Services.Contract.HAL.Dto
{
    public class OperationResult
    {
        public OperationResultEnum ResultStatus { get; set; }

        public int Return { get; set; }
    }
}