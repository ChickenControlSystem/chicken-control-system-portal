using BLL.HardwareModules.Common.Contract;
using BLL.HardwareModules.Common.Sequence;
using HAL.Models.Contract;
using HAL.Models.Device;
using HAL.Operations.Contract;

namespace BLL.HardwareModules.Door.Commands
{
    public class CloseDoorCommand : ICommand
    {
        private readonly IAxisOperations _axisOperations;
        private readonly IValidateOperationService _validateOperationService;
        private readonly IDevice _door;
        private readonly IDevice _floor;

        public CloseDoorCommand(IAxisOperations axisOperations, IValidateOperationService validateOperationService)
        {
            _axisOperations = axisOperations;
            _validateOperationService = validateOperationService;
            _door = new DoorAxis();
            _floor = new FloorDigitalSensor();
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