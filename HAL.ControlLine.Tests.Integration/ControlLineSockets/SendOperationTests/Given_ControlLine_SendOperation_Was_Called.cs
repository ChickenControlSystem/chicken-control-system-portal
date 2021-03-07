using Crosscutting.Configuration;
using Crosscutting.Sockets.Client;
using Crosscutting.Threading;
using Crosscutting.UnitTest;
using ControlLineTcpSockets = HAL.ControlLine.Sockets.ControlLineSockets;

namespace HAL.ControlLine.Tests.Integration.ControlLineSockets.SendOperationTests
{
    public abstract class Given_ControlLine_SendOperation_Was_Called : GenericGivenWhenThenTests<ControlLineTcpSockets>
    {
        public override void Given()
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