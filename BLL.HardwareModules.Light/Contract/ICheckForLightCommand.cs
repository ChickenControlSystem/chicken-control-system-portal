using BLL.Common.Contract;

namespace BLL.HardwareModules.Light.Contract
{
    public interface ICheckForLightCommand : ICommand
    {
        /// <summary>
        /// sets the run count, as run count depends on external factors like time
        /// </summary>
        void SetRunCountBeforeMorning(int runCount);
    }
}