using System;

namespace ControlLine.Contract.Threading
{
    /// <summary>
    /// handles threading operations for functions
    /// </summary>
    public interface IThreadOperations
    {
        /// <summary>
        /// calls function, blocks until the timeout period is up
        /// </summary>
        public T WaitUntilTimeout<T>(Func<T> func, int timeout);
    }
}