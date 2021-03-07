using BLL.Common.Sequence;
using BLL.Common.TaskRecovery;

namespace BLL.Common.Contract
{
    public interface IRunnable
    {
        /// <summary>
        /// number of times the run method is ran until it passes
        /// </summary>
        public int GetRunCount();

        public RecoveryOptionsDto RecoveryOptions { get; }

        public SequenceResultEnum Run();

        /// <summary>
        /// handles error when task in sequence fails
        /// </summary>
        public void HandleFail();
    }
}