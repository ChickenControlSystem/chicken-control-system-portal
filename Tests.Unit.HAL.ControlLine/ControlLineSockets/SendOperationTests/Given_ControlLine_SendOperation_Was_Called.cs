using Crosscutting.Services.Contract.HAL.Dto;
using Crosscutting.Sockets.Client;
using Crosscutting.UnitTest;
using NSubstitute;

namespace Tests.Unit.HAL.ControlLine.ControlLineSockets.SendOperationTests
{
    public abstract class
        Given_ControlLine_SendOperation_Was_Called : GenericGivenWhenThenTests<
            global::HAL.ControlLine.Sockets.ControlLineSockets>
    {
        protected ISocketClient MockSocketClient;
        protected OperationDto Operation;

        protected override void Given()
        {
            MockSocketClient = Substitute.For<ISocketClient>();

            SUT = new global::HAL.ControlLine.Sockets.ControlLineSockets(
                MockSocketClient
            );
        }
    }
}