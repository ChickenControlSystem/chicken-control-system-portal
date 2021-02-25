using System;
using BLL.Common.Sequence;

namespace BLL.Common.Contract
{
    public interface IFluentSequenceBuilder : ISimpleSequenceBuilder
    {
        /// <summary>
        /// adds another task to the list
        /// </summary>
        IFluentSequenceBuilder Queue(IRunnable task);

        /// <summary>
        /// makes the sequence a serial sequence, ends queueing other tasks
        /// </summary>
        public IFluentSequenceBuilder Serial();

        /// <summary>
        /// makes the sequence a parallel sequence, ends queueing other tasks
        /// </summary>
        public IFluentSequenceBuilder Parrelell();

        /// <summary>
        /// adds an action to run when the IRunnable->HandleFail() method is called
        /// </summary>
        public IFluentSequenceBuilder Fail(Action failAction);

        /// <summary>
        /// adds a function that can be run if the normal function fails that will allow the sequence to continue
        /// </summary>
        public IFluentSequenceBuilder Recovery(Func<SequenceResultEnum> recoveryFunc);

        /// <summary>
        /// sets how many times the run function will run for
        /// </summary>
        public IFluentSequenceBuilder RunCount(int runCount);
    }
}