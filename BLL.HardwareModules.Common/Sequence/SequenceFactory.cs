using System;
using System.Collections.Generic;
using BLL.HardwareModules.Common.Contract;
using Threading;

namespace BLL.HardwareModules.Common.Sequence
{
    public class SequenceFactory : ISequenceFactory
    {
        private IThreadOperations _threadOperations;

        public SequenceFactory(IThreadOperations threadOperations)
        {
            _threadOperations = threadOperations;
        }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public ISequence CreateSequence(int type, List<IRunnable> tasks)
        {
            switch (type)
            {
                case ISequenceFactory.Serial:
                    return new SerialSequence(tasks);
                case ISequenceFactory.Parelell:
                    return new ParrellelSequence(tasks, _threadOperations);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}