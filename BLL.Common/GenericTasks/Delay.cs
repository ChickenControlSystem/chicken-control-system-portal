using System;
using BLL.Common.Contract;
using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;
using Threading;

namespace BLL.Common.GenericTasks
{
    public class Delay : IDelay
    {
        private readonly IThreadOperations _threadOperations;
        public int RunCount { get; }
        public RecoveryOptionsDto RecoveryOptions { get; }

        public double Period { get; set; }

        public Delay(IThreadOperations threadOperations)
        {
            _threadOperations = threadOperations;
            RunCount = 1;
            RecoveryOptions = new RecoveryOptionsDto();
        }

        public SequenceResultEnum Run()
        {
            _threadOperations.SyncronousDelay(Period);
            return SequenceResultEnum.Success;
        }

        public void HandleFail()
        {
            //TODO: LOG
        }

        public void WaitUntil(double milliseconds)
        {
            Period = milliseconds;
        }

        public void WaitUntil(TimeSpan time)
        {
            WaitUntil(time.Milliseconds);
        }
    }
}