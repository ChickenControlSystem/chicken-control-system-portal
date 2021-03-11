using Crosscutting.ApplicationConfiguration.Enviroment.Configuration;
using Crosscutting.Sockets.Client;
using Crosscutting.Threading;
using Crosscutting.UnitTest;

namespace Tests.Integration.HAL.ControlLine.ControlLineSockets.SendOperationTests
{
    public abstract class
        Given_ControlLine_SendOperation_Was_Called : GenericGivenWhenThenTests<
            global::HAL.ControlLine.Sockets.ControlLineSockets>
    {
        protected override void Given()
        {
            SUT = new global::HAL.ControlLine.Sockets.ControlLineSockets(
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