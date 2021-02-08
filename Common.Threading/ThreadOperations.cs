using System;
using System.Threading.Tasks;
using Threading.Exception;

namespace Threading
{
    public class ThreadOperations : IThreadOperations
    {
        public T WaitUntilFuncTimeout<T>(Func<T> func, int timeout)
        {
            try
            {
                var task = Task.Run(func);
                if (task.Wait(timeout)) return task.Result;
            }
            catch (AggregateException e)
            {
                if (e.InnerException != null) throw e.InnerException;
            }

            throw new ThreadTimeout();
        }

        public void WaitUntilActionTimeout(Action action, int timeout)
        {
            try
            {
                var task = Task.Run(action);
                if (task.Wait(timeout)) return;
            }
            catch (AggregateException e)
            {
                if (e.InnerException != null) throw e.InnerException;
            }

            throw new ThreadTimeout();
        }

        public void RunBackground(Action action)
        {
            Task.Run(action);
        }
    }
}