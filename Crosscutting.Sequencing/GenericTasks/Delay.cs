using System;
using Crosscutting.DateTime;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.Sequence;
using Crosscutting.Sequencing.TaskRecovery;
using Crosscutting.Threading;

namespace Crosscutting.Sequencing.GenericTasks
{
    public class Delay : IDelay
    {
        private readonly ITimeService _timeService;
        private readonly IThreadOperations _threadOperations;
        private int _runCount;

        public RecoveryOptionsDto RecoveryOptions { get; }

        private double Period { get; set; }

        public Delay(IThreadOperations threadOperations, ITimeService timeService)
        {
            _threadOperations = threadOperations;
            _timeService = timeService;
            _runCount = 1;
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

        public int GetRunCount()
        {
            return _runCount;
        }
    }
}