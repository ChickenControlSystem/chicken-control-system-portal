using Bootstrapping.Services.Contract.Crosscutting.Dto.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Enum.Sequencing;

namespace Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing
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