using System;
using System.Collections.Generic;
using Crosscutting.Services.Contract.Crosscutting.Dto.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Interface.Utilities;

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
            return type switch
            {
                ISequenceFactory.Serial => new SerialSequence(tasks, recoveryOptions, failAction, runCount),
                ISequenceFactory.Parelell => new ParrellelSequence(tasks, recoveryOptions, failAction, runCount,
                    _threadOperations),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}