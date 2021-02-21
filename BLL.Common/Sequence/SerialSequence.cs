using System;
using System.Collections.Generic;
using BLL.Common.Contract;
using BLL.Common.TaskRecovery;

namespace BLL.Common.Sequence
{
    public class SerialSequence : Sequence, ISequence, ISerialSeqeuenceBuilder
    {
        public SerialSequence(
            List<IRunnable> tasks,
            RecoveryOptionsDto recoveryOptions,
            Action failAction,
            int runCount
        ) : base(
            tasks,
            recoveryOptions,
            failAction,
            runCount
        )
        {
        }

        public SequenceResultEnum Run()
        {
            try
            {
                Tasks.ForEach(task =>
                {
                    var result = task.Run();
                    if (result != SequenceResultEnum.Fail) return;
                    task.HandleFail();
                    throw new InvalidOperationException();
                });
            }
            catch (InvalidOperationException)
            {
                return SequenceResultEnum.Fail;
            }

            return SequenceResultEnum.Success;
        }

        public void HandleFail() => FailAction();
    }
}