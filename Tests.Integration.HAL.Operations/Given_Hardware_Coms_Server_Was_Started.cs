using Crosscutting.Threading;
using Crosscutting.UnitTest;
using Tests.Fakes.HAL.FakeHardwareComs;

namespace Tests.Integration.HAL.Operations
{
    public class Given_Hardware_Coms_Server_Was_Started : IntegrationService<FakeHardwareComsServer>
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