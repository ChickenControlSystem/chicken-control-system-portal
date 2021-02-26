using System;
using BLL.Common.Contract;
using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;
using Common.DateTime;
using Threading;

namespace BLL.Common.GenericTasks
{
    public class Delay : IDelay
    {
        private readonly ITimeService _timeService;
        private readonly IThreadOperations _threadOperations;
        public int RunCount { get; }
        public RecoveryOptionsDto RecoveryOptions { get; }

        private double Period { get; set; }

        public Delay(IThreadOperations threadOperations, ITimeService timeService)
        {
            _threadOperations = threadOperations;
            _timeService = timeService;
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