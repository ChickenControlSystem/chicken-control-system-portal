using BLL.HardwareModules.Common.Sequence;

namespace BLL.HardwareModules.Common.Contract
{
    public interface IRunnable
    {
        public SequenceResultEnum Run();

        /// <summary>
        /// handles error when task in sequence fails
        /// </summary>
        public void HandleFail();
    }
}