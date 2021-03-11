using System;
using Bootstrapping.Services.Contract.Crosscutting.Enum.Sequencing;

namespace Bootstrapping.Services.Contract.Crosscutting.Dto.Sequencing
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