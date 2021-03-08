using System;
using System.Collections.Generic;
using Crosscutting.Services.Contract.Crosscutting.Dto.Sequencing;

namespace Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing
{
    public interface ISequenceFactory
    {
        public const int Serial = 0;
        public const int Parelell = 1;

        public ISequence CreateSequence(
            int type,
            List<IRunnable> tasks,
            RecoveryOptionsDto recoveryOptions,
            Action failAction,
            int runCount
        );
    }
}