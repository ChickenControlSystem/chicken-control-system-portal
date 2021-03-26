using Bootstrapping.Services.Contract.Crosscutting.Utils;

namespace Bootstrapping.Services.Contract.HAL.Interface
{
    public interface ILightSensor : IDataMemory<double>, IDevice
    {
    }
}