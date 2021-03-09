using System;
using System.Collections.Generic;
using Bootstrapping.Services.Contract.Crosscutting.Dto.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing;

namespace Crosscutting.Sequencing.Sequence
{
    public abstract class Sequence
    {
        protected readonly List<IRunnable> Tasks;
        public RecoveryOptionsDto RecoveryOptions { get; }
        protected readonly Action FailAction;
        private readonly int _runCount;

        public int GetRunCount()
        {
            return _runCount;
        }

        protected Sequence(List<IRunnable> tasks, RecoveryOptionsDto recoveryOptions, Action failAction, int runCount)
        {
            Tasks = tasks;
            RecoveryOptions = recoveryOptions;
            FailAction = failAction;
            _runCount = runCount;
        }
    }
}