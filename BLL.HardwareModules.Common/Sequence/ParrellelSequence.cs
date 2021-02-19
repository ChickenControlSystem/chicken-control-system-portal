using System.Collections.Generic;
using BLL.HardwareModules.Common.Contract;
using Threading;

namespace BLL.HardwareModules.Common.Sequence
{
    public class ParrellelSequence : Sequence, ISequence
    {
        private IThreadOperations _threadOperations;

        public ParrellelSequence(List<IRunnable> tasks, IThreadOperations threadOperations) : base(tasks)
        {
            _threadOperations = threadOperations;
        }

        public SequenceResultEnum Run()
        {
            throw new System.NotImplementedException();
        }

        public void HandleFail()
        {
            throw new System.NotImplementedException();
        }
    }
}