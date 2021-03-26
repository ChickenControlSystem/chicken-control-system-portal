using System;
using Bootstrapping.Services.Contract.Crosscutting.Utils;

namespace Crosscutting.Threading
{
    public abstract class DataMemory<T> : IDataMemory<T>
    {
        private readonly object _lockObject;
        private T _value;

        protected DataMemory()
        {
            _lockObject = new object();
        }

        protected T Value
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