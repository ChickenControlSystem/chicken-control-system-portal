using HAL.Models.Contract;

namespace HAL.Models
{
    public class LightAnolougeSensor : IDevice
    {
        public byte Id { get; } = 1;
        public string Name { get; } = "Light Sensor";
    }
}