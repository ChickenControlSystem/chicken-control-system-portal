using Crosscutting.Contract.HAL.Enum;

namespace Crosscutting.Contract.HAL.Dto
{
    public class OperationResult
    {
        public OperationResultEnum ResultStatus { get; set; }

        public int Return { get; set; }
    }
}