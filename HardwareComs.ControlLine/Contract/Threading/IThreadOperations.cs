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
        T WaitUntilFuncTimeout<T>(Func<T> func, int timeout);

        /// <summary>
        /// calls action, blocks until the timeout period is up
        /// </summary>
        void WaitUntilActionTimeout(Action action, int timeout);

        /// <summary>
        /// starts and runs a background thread
        /// </summary>
        void RunBackground(Action action);
    }
}