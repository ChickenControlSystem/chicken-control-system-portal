using System;
using System.Threading.Tasks;
using ControlLine.Contract.Threading;
using ControlLine.Exception;

namespace ControlLine.Threading
{
    public class ThreadOperations : IThreadOperations
    {
        public T WaitUntilTimeout<T>(Func<T> func, int timeout)
        {
            try
            {
                var task = Task.Run(func);
                if (task.Wait(timeout))
                {
                    return task.Result;
                }
            }
            catch (AggregateException e)
            {
                if (e.InnerException != null) throw e.InnerException;
            }

            throw new ThreadTimeout();
        }
    }
}