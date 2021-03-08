using Crosscutting.Services.Contract.Crosscutting.Dto.Sequencing;
using Crosscutting.Services.Contract.Crosscutting.Enum.Sequencing;

namespace Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing
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