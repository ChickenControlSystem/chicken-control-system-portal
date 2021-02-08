using Threading;
using UnitTest;

namespace HardwareComs.Tests.Common.FakeHardwareComs.SanityChecks.SanityChecks
{
    public class Given_Hardware_Coms_Server_Is_Running : IntegrationService<FakeHardwareComsServer>
    {
        protected override void GivenServices()
        {
            Service = new FakeHardwareComsServer(new ThreadOperations());
        }
    }
}