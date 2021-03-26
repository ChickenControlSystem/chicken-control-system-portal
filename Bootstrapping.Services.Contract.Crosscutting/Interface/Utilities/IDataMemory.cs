using System;

namespace Bootstrapping.Services.Contract.Crosscutting.Interface.Utilities
{
    /// <summary>
    /// describes a thread safe, singleton class that stores a value which needs to be shared across multiple classes
    /// </summary>
    public interface IDataMemory<T>
    {
        public T Value { get; set; }
        public event Action ValueChangedEvent;
    }
}