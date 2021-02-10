using ControlSystem.Tests.Enviroment.ControlSystem.Configuration;
using Sockets.Client;
using Threading;
using UnitTest;
using ControlLineTcpSockets = ControlLine.Sockets.ControlLineSockets;

namespace HardwareComs.Tests.ControlLine.Integration.ControlLineSockets.SendOperationTests
{
    public abstract class Given_ControlLine_SendOperation_Was_Called : GenericGivenWhenThenTests<ControlLineTcpSockets>
    {
        protected override void Given()
        {
            SUT = new ControlLineTcpSockets(
                new TcpClient(
                    ConfigurationLoader.GetTestConfigurationLoader().GetControlLineSettings().GetEndPoint(),
                    8,
                    new ThreadOperations(),
                    5000
                )
            );
        }
    }
}