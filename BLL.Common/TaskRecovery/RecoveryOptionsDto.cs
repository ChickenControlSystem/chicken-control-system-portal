using System;
using BLL.Common.Sequence;

namespace BLL.Common.TaskRecovery
{
    public class RecoveryOptionsDto
    {
        public RecoveryOptionsDto(bool isRecoverable, Func<SequenceResultEnum> recoveryFunc)
        {
            IsRecoverable = isRecoverable;
            RecoveryFunc = recoveryFunc;
        }

        public RecoveryOptionsDto()
        {
        }

        public bool IsRecoverable { get; }
        public Func<SequenceResultEnum> RecoveryFunc { get; }
    }
}