using Bootstrapping.Services.Contract.HAL.Enum;

namespace Bootstrapping.Services.Contract.HAL.Dto
{
    public class OperationResult
    {
        public OperationResultEnum ResultStatus { get; set; }

        public int Return { get; set; }
    }
}