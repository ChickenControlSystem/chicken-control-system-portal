using System;
using Crosscutting.Services.Contract.Crosscutting.Enum.Sequencing;

namespace Crosscutting.Services.Contract.Crosscutting.Dto.Sequencing
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