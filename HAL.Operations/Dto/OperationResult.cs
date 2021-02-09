using HAL.Operations.Enum;

namespace HAL.Operations.Dto
{
    public class OperationResult
    {
        public OperationResultEnum ResultStatus { get; set; }

        public int Return { get; set; }
    }
}