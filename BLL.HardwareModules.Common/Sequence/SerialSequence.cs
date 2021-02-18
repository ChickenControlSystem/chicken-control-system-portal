using System;
using System.Collections.Generic;
using BLL.HardwareModules.Common.Contract;

namespace BLL.HardwareModules.Common.Sequence
{
    public class SerialSequence : Sequence, ISequence
    {
        public SerialSequence(List<IRunnable> tasks) : base(tasks)
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

        public void HandleFail()
        {
            //TODO: LOGS ERROR
        }
    }
}