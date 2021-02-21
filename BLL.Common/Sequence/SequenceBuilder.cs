using System;
using System.Collections.Generic;
using BLL.Common.Contract;
using BLL.Common.TaskRecovery;
using CodeContracts;

namespace BLL.Common.Sequence
{
    public class SequenceBuilder : ISequenceBuilder
    {
        private readonly ISequenceFactory _sequenceFactory;
        private int _sequenceType;
        private readonly List<IRunnable> _tasks;
        private Func<SequenceResultEnum> _recoveryFunc;
        private int _runCount = 1;
        private Action _failAction;

        private bool _sequenceTypeSet;
        private bool _sequenceRecoverySet;
        private bool _sequenceFailActionSet;
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

        public ISequenceBuilder MakeSerial()
        {
            CodeContract.PreCondition<ArgumentException>(!_sequenceTypeSet);

            _sequenceType = ISequenceFactory.Serial;
            _sequenceTypeSet = true;
            return this;
        }

        public ISequenceBuilder AddFailAction(Action failAction)
        {
            CodeContract.PreCondition<ArgumentException>(!_sequenceFailActionSet);

            _failAction = failAction;
            _sequenceFailActionSet = true;
            return this;
        }

        public ISequenceBuilder AddRecoveryFunc(Func<SequenceResultEnum> recoveryFunc)
        {
            CodeContract.PreCondition<ArgumentException>(!_sequenceRecoverySet);

            _recoveryFunc = recoveryFunc;
            _sequenceRecoverySet = true;
            return this;
        }

        public ISequenceBuilder SetRunCount(int runCount)
        {
            CodeContract.PreCondition<ArgumentException>(_runCount == 1);

            _runCount = runCount;
            return this;
        }

        public ISequenceBuilder MakeParrelell()
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
            CodeContract.PreCondition<ArgumentException>(_sequenceFailActionSet);

            var recovery = _sequenceRecoverySet
                ? new RecoveryOptionsDto(true, _recoveryFunc)
                : new RecoveryOptionsDto();

            return _sequenceFactory.CreateSequence(
                _sequenceType,
                _tasks,
                recovery,
                _failAction,
                _runCount
            );
        }
    }
}