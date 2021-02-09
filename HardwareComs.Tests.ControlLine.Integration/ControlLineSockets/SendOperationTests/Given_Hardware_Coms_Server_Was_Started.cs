using HardwareComs.Tests.Common.FakeHardwareComs;
using Threading;
using UnitTest;

namespace HardwareComs.Tests.ControlLine.Integration.ControlLineSockets.SendOperationTests
{
    public class Given_Hardware_Coms_Server_Was_Started : IntegrationService<FakeHardwareComsServer>
    {
        protected override void GivenServices()
        {
            Service = new FakeHardwareComsServer(
                new ThreadOperations(),
                new RequestResponseFlagsDto
                {
                    AbsoluteMoveDoorFail = false,
                    ReadLightFail = true,
                    RelativeMoveDoorFail = false
                }
            );
        }
    }
}