using Crosscutting.Configuration;
using Crosscutting.Sockets.Client;
using Crosscutting.Threading;
using Crosscutting.UnitTest;

namespace HAL.ControlLine.Tests.Integration.ControlLineSockets.SendOperationTests
{
    public abstract class
        Given_ControlLine_SendOperation_Was_Called : GenericGivenWhenThenTests<Sockets.ControlLineSockets>
    {
        public override void Given()
        {
            SUT = new Sockets.ControlLineSockets(
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