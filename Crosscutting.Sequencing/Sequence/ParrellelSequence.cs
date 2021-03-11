using System;
using System.Collections.Generic;
using Bootstrapping.Services.Contract.Crosscutting.Dto.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Enum.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Utilities;

namespace Crosscutting.Sequencing.Sequence
{
    public class ParrellelSequence : Sequence, ISequence
    {
        private IThreadOperations _threadOperations;

        public SequenceResultEnum Run()
        {
            throw new NotImplementedException();
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