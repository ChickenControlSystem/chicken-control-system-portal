using Crosscutting.Contract.HAL.Interface;

namespace HAL.Models.Device
{
    public class DoorAxis : IDoor
    {
        public byte Id { get; } = 2;
        public string Name { get; } = "Door Axis";
    }
}