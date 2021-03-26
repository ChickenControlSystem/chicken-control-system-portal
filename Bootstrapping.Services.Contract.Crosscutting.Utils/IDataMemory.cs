using System;

namespace Bootstrapping.Services.Contract.Crosscutting.Utils
{
    /// <summary>
    /// describes a thread safe, singleton class that stores a value which needs to be shared across multiple classes
    /// </summary>
    public interface IDataMemory<T>
    {
        public event Action ValueChangedEvent;
    }
}