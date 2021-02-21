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
                    var result = RunOrRetryTask(task);
                    if (result == SequenceResultEnum.Success) return;
                    result = RecoverOrFailTask(task);
                    if (result == SequenceResultEnum.Success) return;
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

        private static SequenceResultEnum RunOrRetryTask(IRunnable task)
        {
            var result = SequenceResultEnum.Fail;

            for (var i = 0; i < task.RunCount; i++)
            {
                var taskResult = task.Run();
                if (taskResult != SequenceResultEnum.Success) continue;
                result = SequenceResultEnum.Success;
                break;
            }

            return result;
        }

        private static SequenceResultEnum RecoverOrFailTask(IRunnable task)
        {
            var result = SequenceResultEnum.Fail;

            if (task.RecoveryOptions.IsRecoverable)
            {
                result = task.RecoveryOptions.RecoveryFunc();
            }

            return result;
        }

        public void HandleFail() => FailAction();
    }
}