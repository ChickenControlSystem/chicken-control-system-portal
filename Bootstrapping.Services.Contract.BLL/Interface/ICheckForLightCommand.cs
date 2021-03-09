using Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing;

namespace Bootstrapping.Services.Contract.BLL.Interface
{
    public interface ICheckForLightCommand : ICommand
    {
        /// <summary>
        /// sets the run count, as run count depends on external factors like time
        /// </summary>
        void SetRunCountBeforeMorning(int runCount);
    }
}