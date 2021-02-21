using System;
using System.Collections.Generic;
using BLL.Common.TaskRecovery;

namespace BLL.Common.Contract
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