using System;
using System.Collections.Generic;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.TaskRecovery;
using Crosscutting.Threading;

namespace Crosscutting.Sequencing.Sequence
{
    public class SequenceFactory : ISequenceFactory
    {
        private readonly IThreadOperations _threadOperations;

        public SequenceFactory(IThreadOperations threadOperations)
        {
            _threadOperations = threadOperations;
        }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ISequence CreateSequence(
            int type,
            List<IRunnable> tasks,
            RecoveryOptionsDto recoveryOptions,
            Action failAction,
            int runCount
        )
        {
            switch (type)
            {
                case ISequenceFactory.Serial:
                    return new SerialSequence(
                        tasks,
                        recoveryOptions,
                        failAction,
                        runCount
                    );
                case ISequenceFactory.Parelell:
                    return new ParrellelSequence(
                        tasks,
                        recoveryOptions,
                        failAction,
                        runCount,
                        _threadOperations
                    );
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}