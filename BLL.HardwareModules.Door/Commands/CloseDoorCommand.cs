using BLL.Common.Contract;
using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;
using BLL.HardwareModules.Door.Contract;
using HAL.Models.Contract;
using HAL.Operations.Contract;

namespace BLL.HardwareModules.Door.Commands
{
    public class CloseDoorCommand : IDoorCommand
    {
        private readonly IAxisOperations _axisOperations;
        private readonly IValidateOperationService _validateOperationService;
        private readonly IDoor _door;
        private readonly IFloorSensor _floor;

        public int RunCount { get; }
        public RecoveryOptionsDto RecoveryOptions { get; }

        public CloseDoorCommand(IAxisOperations axisOperations, IValidateOperationService validateOperationService,
            IDoor door, IFloorSensor floor)
        {
            _axisOperations = axisOperations;
            _validateOperationService = validateOperationService;
            _door = door;
            _floor = floor;

            RunCount = 3;
            RecoveryOptions = new RecoveryOptionsDto();
        }

        public SequenceResultEnum Run()
        {
            var result = _axisOperations
                .MoveAxisSearch(
                    _door,
                    _floor,
                    false
                );
            return _validateOperationService.GetSequenceResult(result);
        }

        public void HandleFail()
        {
            throw new System.NotImplementedException();
        }
    }
}