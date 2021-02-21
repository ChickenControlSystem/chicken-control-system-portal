using System;
using BLL.Common.Sequence;

namespace BLL.Common.Contract
{
    public interface ISequenceBuilder
    {
        /// <summary>
        /// builds final sequence (called last)
        /// </summary>
        ISequence Build();

        /// <summary>
        /// adds another task to the list
        /// </summary>
        ISequenceBuilder Queue(IRunnable task);

        /// <summary>
        /// stops adding any more tasks
        /// </summary>
        public ISequenceBuilder EndQueue();

        /// <summary>
        /// makes the sequence a serial sequence
        /// </summary>
        public ISequenceBuilder Serial();

        /// <summary>
        /// makes the sequence a parallel sequence
        /// </summary>
        public ISequenceBuilder Parrelell();

        /// <summary>
        /// adds an action to run when the IRunnable->HandleFail() method is called
        /// </summary>
        public ISequenceBuilder Fail(Action failAction);

        /// <summary>
        /// adds a function that can be run if the normal function fails that will allow the sequence to continue
        /// </summary>
        public ISequenceBuilder Recovery(Func<SequenceResultEnum> recoveryFunc);

        /// <summary>
        /// sets how many times the run function will run for
        /// </summary>
        public ISequenceBuilder RunCount(int runCount);
    }
}