using Bootstrapping.Services.Contract.Crosscutting.Utils;

namespace BLL.HardwareModules.Light.DataMonitors
{
    public class LightSensorMonitor
    {
        private readonly IThreadOperations _timeService;

        public LightSensorMonitor(IThreadOperations timeService)
        {
            _timeService = timeService;
        }
    }
}