using System;
using System.Collections.Generic;
using Crosscutting.CodeContracts;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.TaskRecovery;

namespace Crosscutting.Sequencing.Sequence
{
    public class FluentSequenceBuilder : IFluentSequenceBuilder
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

        public FluentSequenceBuilder(ISequenceFactory sequenceFactory)
        {
            _sequenceFactory = sequenceFactory;
            _tasks = new List<IRunnable>();
        }

        public IFluentSequenceBuilder Queue(IRunnable task)
        {
            CodeContract.PreCondition<ArgumentException>(!_tasksSet);

            _tasks.Add(task);
            return this;
        }

        public IFluentSequenceBuilder QueueConditional(IRunnable task, bool condition)
        {
            if (condition)
                Queue(task);
            return this;
        }

        private void EndQueue()
        {
            CodeContract.PreCondition<ArgumentException>(!_tasksSet);

            _tasksSet = true;
        }

        public IFluentSequenceBuilder Serial()
        {
            EndQueue();
            CodeContract.PreCondition<ArgumentException>(!_sequenceTypeSet);

            _sequenceType = ISequenceFactory.Serial;
            _sequenceTypeSet = true;
            return this;
        }

        public IFluentSequenceBuilder Fail(Action failAction)
        {
            CodeContract.PreCondition<ArgumentException>(!_sequenceFailActionSet);

            _failAction = failAction;
            _sequenceFailActionSet = true;
            return this;
        }

        public IFluentSequenceBuilder Recovery(Func<SequenceResultEnum> recoveryFunc)
        {
            CodeContract.PreCondition<ArgumentException>(!_sequenceRecoverySet);

            _recoveryFunc = recoveryFunc;
            _sequenceRecoverySet = true;
            return this;
        }

        public IFluentSequenceBuilder RunCount(int runCount)
        {
            CodeContract.PreCondition<ArgumentException>(_runCount == 1);

            _runCount = runCount;
            return this;
        }

        public IFluentSequenceBuilder Parrelell()
        {
            EndQueue();
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