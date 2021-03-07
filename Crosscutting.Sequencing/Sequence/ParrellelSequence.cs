using System;
using System.Collections.Generic;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.TaskRecovery;
using Crosscutting.Threading;

namespace Crosscutting.Sequencing.Sequence
{
    public class ParrellelSequence : Sequence, ISequence
    {
        private IThreadOperations _threadOperations;

        public SequenceResultEnum Run()
        {
            throw new System.NotImplementedException();
        }

        public void HandleFail() => FailAction();

        public ParrellelSequence(
            List<IRunnable> tasks,
            RecoveryOptionsDto recoveryOptions,
            Action failAction,
            int runCount,
            IThreadOperations threadOperations
        ) : base(
            tasks,
            recoveryOptions,
            failAction,
            runCount
        )
        {
            _threadOperations = threadOperations;
        }
    }
}