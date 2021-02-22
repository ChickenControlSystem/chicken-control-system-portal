using ControlLine.Sockets;
using ControlSystem.Tests.Enviroment.ControlSystem.Configuration;
using Sockets.Client;
using Threading;
using UnitTest;

namespace HAL.Operations.Tests.Integration.AxisOperations
{
    public abstract class Given_Operation_Was_Called : GenericGivenWhenThenTests<Operations.AxisOperations>
    {
        public override void Given()
        {
            var controlLine = new ControlLineSockets(
                new TcpClient(
                    ConfigurationLoader.GetTestConfigurationLoader().GetControlLineSettings().GetEndPoint(),
                    8,
                    new ThreadOperations(),
                    5000
                )
            );
            var errorService = new ErrorService();

            SUT = new Operations.AxisOperations(
                errorService,
                controlLine
            );
        }
    }
}