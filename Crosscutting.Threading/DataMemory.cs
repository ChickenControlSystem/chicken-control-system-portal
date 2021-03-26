using System;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Utilities;

namespace Crosscutting.Threading
{
    public class DataMemory<T> : IDataMemory<T>
    {
        private readonly object _lockObject;
        private T _value;

        public DataMemory()
        {
            _lockObject = new object();
        }

        public T Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        public event Action ValueChangedEvent;

        private T GetValue()
        {
            lock (_lockObject)
            {
                return _value;
            }
        }

        private void SetValue(T value)
        {
            lock (_lockObject)
            {
                _value = value;
                ValueChangedEvent?.Invoke();
            }
        }
    }
}