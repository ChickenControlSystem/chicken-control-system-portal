using HAL.Models.Contract;

namespace HAL.Models.Device
{
    public class CeilingDigitalSensor : ICeilingSensor
    {
        public byte Id { get; } = 3;
        public string Name { get; } = "Floor Sensor";
    }
}