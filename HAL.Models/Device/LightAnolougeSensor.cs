using System;
using Bootstrapping.Services.Contract.HAL.Interface;

namespace HAL.Models.Device
{
    public class LightAnolougeSensor : ILightSensor
    {
        public byte Id { get; } = 1;
        public string Name { get; } = "Light Sensor";

        private readonly object _lockObject = new object();

        public event Action AnalogValueChanged;

        private double _value;

        public double Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        private void SetValue(double value)
        {
            lock (_lockObject)
            {
                _value = value;
                AnalogValueChanged?.Invoke();
            }
        }

        private double GetValue()
        {
            lock (_lockObject)
            {
                return _value;
            }
        }
    }
}