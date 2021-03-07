using Crosscutting.Threading;
using Crosscutting.UnitTest;
using HAL.Fakes.FakeHardwareComs;

namespace HAL.FakeHardwareComs.Tests.Integration.SanityChecks
{
    public class Given_Hardware_Coms_Server_Is_Running : IntegrationService<FakeHardwareComsServer>
    {
        protected override void GivenServices()
        {
            Service = new FakeHardwareComsServer(
                new ThreadOperations(),
                new RequestResponseFlagsDto
                {
                    AbsoluteMoveDoorFail = true,
                    ReadLightFail = false,
                    RelativeMoveDoorFail = false
                }
            );
        }
    }
}