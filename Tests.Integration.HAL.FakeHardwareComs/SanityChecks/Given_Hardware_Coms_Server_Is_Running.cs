using Crosscutting.Threading;
using Crosscutting.UnitTest;
using Tests.Fakes.HAL.FakeHardwareComs;

namespace Tests.Integration.HAL.FakeHardwareComs.SanityChecks
{
    public class Given_Hardware_Coms_Server_Is_Running : IntegrationService<FakeHardwareComsServer>
    {
        protected override void GivenServices()
        {
            Service = new FakeHardwareComsServer(
                new ThreadOperations(),
                FakeHardwareComsHelper
                    .GetDefaultMockRequestResponseCollection()
            );
        }
    }
}