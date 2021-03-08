using System;

namespace Crosscutting.Services.Contract.Crosscutting.Interface.Utilities
{
    /// <summary>
    ///     handles threading operations for functions
    /// </summary>
    public interface IThreadOperations
    {
        /// <summary>
        ///     calls function, blocks until the timeout period is up
        /// </summary>
        T WaitUntilFuncTimeout<T>(Func<T> func, int timeout);

        /// <summary>
        ///     calls action, blocks until the timeout period is up
        /// </summary>
        void WaitUntilActionTimeout(Action action, int timeout);

        /// <summary>
        ///     starts and runs a background thread
        /// </summary>
        void RunBackground(Action action);

        /// <summary>
        /// blocks main thread by certain delay specified
        /// </summary>
        void SyncronousDelay(double timeInMiliSeconds);
    }
}