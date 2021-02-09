using HAL.Models.Contract;

namespace HAL.Models
{
    public class FloorDigitalSensor : IDevice
    {
        public byte Id { get; } = 3;
        public string Name { get; } = "Floor Sensor";
    }
}