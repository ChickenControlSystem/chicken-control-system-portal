using System;
using System.Collections.Generic;
using BLL.HardwareModules.Common.Contract;
using CodeContracts;

namespace BLL.HardwareModules.Common.Sequence
{
    public class SequenceBuilder : ISequenceBuilder
    {
        private readonly ISequenceFactory _sequenceFactory;
        private int _sequenceType;
        private readonly List<IRunnable> _tasks;

        private bool _sequenceTypeSet;
        private bool _tasksSet;

        public SequenceBuilder(ISequenceFactory sequenceFactory)
        {
            _sequenceFactory = sequenceFactory;
            _tasks = new List<IRunnable>();
        }

        public ISequenceBuilder QueueTask(IRunnable task)
        {
            CodeContract.PreCondition<ArgumentException>(!_tasksSet);

            _tasks.Add(task);
            return this;
        }

        public ISequenceBuilder EndQueue()
        {
            CodeContract.PreCondition<ArgumentException>(!_tasksSet);

            _tasksSet = true;
            return this;
        }

        public ISequenceBuilder SetSerial()
        {
            CodeContract.PreCondition<ArgumentException>(!_sequenceTypeSet);

            _sequenceType = ISequenceFactory.Serial;
            _sequenceTypeSet = true;
            return this;
        }

        public ISequenceBuilder SetParrelell()
        {
            CodeContract.PreCondition<ArgumentException>(!_sequenceTypeSet);

            _sequenceType = ISequenceFactory.Parelell;
            _sequenceTypeSet = true;
            return this;
        }

        public ISequence Build()
        {
            CodeContract.PreCondition<ArgumentException>(_sequenceTypeSet);
            CodeContract.PreCondition<ArgumentException>(_tasksSet);

            return _sequenceFactory.CreateSequence(_sequenceType, _tasks);
        }
    }
}