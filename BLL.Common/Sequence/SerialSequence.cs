using System;
using System.Collections.Generic;
using BLL.Common.Contract;
using BLL.Common.TaskRecovery;

namespace BLL.Common.Sequence
{
    public class SerialSequence : Sequence, ISequence
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
            var result = SequenceResultEnum.Success;

            foreach (var task in Tasks)
            {
                if (RunOrRetryTask(task) == SequenceResultEnum.Success) continue;

                if (RecoverOrFailTask(task) == SequenceResultEnum.Success) continue;

                task.HandleFail();
                result = SequenceResultEnum.Fail;
                break;
            }

            return result;
        }

        private static SequenceResultEnum RunOrRetryTask(IRunnable task)
        {
            var result = SequenceResultEnum.Fail;

            for (var i = 0; i < task.GetRunCount(); i++)
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