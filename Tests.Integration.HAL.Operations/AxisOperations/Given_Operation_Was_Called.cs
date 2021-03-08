using Crosscutting.ApplicationConfiguration.Enviroment.Configuration;
using Crosscutting.Sockets.Client;
using Crosscutting.Threading;
using Crosscutting.UnitTest;
using HAL.ControlLine.Sockets;
using HAL.Operations;

namespace Tests.Integration.HAL.Operations.AxisOperations
{
    public abstract class Given_Operation_Was_Called : GenericGivenWhenThenTests<global::HAL.Operations.AxisOperations>
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

            SUT = new global::HAL.Operations.AxisOperations(
                errorService,
                controlLine
            );
        }
    }
}