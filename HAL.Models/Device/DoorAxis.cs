using HAL.Models.Contract;

namespace HAL.Models.Device
{
    public class DoorAxis : IDevice
    {
        public byte Id { get; } = 2;
        public string Name { get; } = "Door Axis";
    }
}