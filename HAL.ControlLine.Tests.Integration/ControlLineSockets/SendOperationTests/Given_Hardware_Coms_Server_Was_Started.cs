using Crosscutting.Threading;
using Crosscutting.UnitTest;
using HAL.Fakes.FakeHardwareComs;

namespace HAL.ControlLine.Tests.Integration.ControlLineSockets.SendOperationTests
{
    public class Given_Hardware_Coms_Server_Was_Started : IntegrationService<FakeHardwareComsServer>
    {
        protected override void GivenServices()
        {
            Service = new FakeHardwareComsServer(
                new ThreadOperations(),
                new RequestResponseFlagsDto()
            );
        }
    }
}