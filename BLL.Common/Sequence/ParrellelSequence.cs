using System;
using System.Collections.Generic;
using BLL.Common.Contract;
using BLL.Common.TaskRecovery;
using Threading;

namespace BLL.Common.Sequence
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