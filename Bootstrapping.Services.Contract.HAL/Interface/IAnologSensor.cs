using System;

namespace Bootstrapping.Services.Contract.HAL.Interface
{
    public interface IAnologSensor : IDevice
    {
        public event Action AnalogValueChanged;

        public double Value { get; set; }
    }
}