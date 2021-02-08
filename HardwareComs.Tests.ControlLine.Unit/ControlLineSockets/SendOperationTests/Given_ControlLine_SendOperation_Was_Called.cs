using ControlLine.Dto;
using NSubstitute;
using Sockets;
using UnitTest;

namespace ControlLineUnitTests.ControlLineSockets.SendOperationTests
{
    public abstract class
        Given_ControlLine_SendOperation_Was_Called : GivenWhenThenTests<ControlLine.Sockets.ControlLineSockets>
    {
        protected ISocketClient MockSocketClient;
        protected OperationDto Operation;

        protected override void Given()
        {
            MockSocketClient = Substitute.For<ISocketClient>();

            SUT = new ControlLine.Sockets.ControlLineSockets(
                MockSocketClient
            );
        }
    }
}