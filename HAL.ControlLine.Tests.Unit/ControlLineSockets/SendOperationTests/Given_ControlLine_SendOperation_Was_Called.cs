using Crosscutting.Contract.HAL.Dto;
using Crosscutting.Sockets.Client;
using Crosscutting.UnitTest;
using NSubstitute;

namespace HAL.ControlLine.Tests.Unit.ControlLineSockets.SendOperationTests
{
    public abstract class
        Given_ControlLine_SendOperation_Was_Called : GenericGivenWhenThenTests<
            global::HAL.ControlLine.Sockets.ControlLineSockets>
    {
        protected ISocketClient MockSocketClient;
        protected OperationDto Operation;

        public override void Given()
        {
            MockSocketClient = Substitute.For<ISocketClient>();

            SUT = new global::HAL.ControlLine.Sockets.ControlLineSockets(
                MockSocketClient
            );
        }
    }
}