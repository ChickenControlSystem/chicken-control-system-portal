using Bootstrapping.Services.Contract.HAL.Interface;
using Crosscutting.Threading;

namespace HAL.Models.Device
{
    public class LightAnolougeSensor : DataMemory<double>, ILightSensor
    {
        public byte Id { get; } = 1;
        public string Name { get; } = "Light Sensor";
    }
}