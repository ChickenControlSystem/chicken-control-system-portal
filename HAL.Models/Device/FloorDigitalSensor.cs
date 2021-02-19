using HAL.Models.Contract;

namespace HAL.Models.Device
{
    public class FloorDigitalSensor : IFloorSensor
    {
        public byte Id { get; } = 3;
        public string Name { get; } = "Floor Sensor";
    }
}