using System;
using System.Collections.Generic;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.TaskRecovery;

namespace Crosscutting.Sequencing.Sequence
{
    public abstract class Sequence
    {
        protected readonly List<IRunnable> Tasks;
        public RecoveryOptionsDto RecoveryOptions { get; }
        protected readonly Action FailAction;
        protected int RunCount;

        public int GetRunCount()
        {
            return RunCount;
        }

        protected Sequence(List<IRunnable> tasks, RecoveryOptionsDto recoveryOptions, Action failAction, int runCount)
        {
            Tasks = tasks;
            RecoveryOptions = recoveryOptions;
            FailAction = failAction;
            RunCount = runCount;
        }
    }
}