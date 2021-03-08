using System;
using Crosscutting.Services.Contract.Crosscutting.Dto.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Enum.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Interface.Utilities;
using Crosscutting.Threading;

namespace Crosscutting.Sequencing.GenericTasks
{
    public class Delay : IDelay
    {
        private readonly ITimeService _timeService;
        private readonly IThreadOperations _threadOperations;
        private readonly int _runCount;

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