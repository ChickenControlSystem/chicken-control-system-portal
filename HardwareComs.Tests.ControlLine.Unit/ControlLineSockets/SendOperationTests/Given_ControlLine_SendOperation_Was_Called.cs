using ControlLine.Contract.Sockets;
using ControlLine.Dto;
using NSubstitute;
using UnitTest;

namespace ControlLineUnitTests.ControlLineSockets.SendOperationTests
{
    public abstract class
        Given_ControlLine_SendOperation_Was_Called : GivenWhenThenTests<ControlLine.Sockets.ControlLineSockets>
    {
        protected const int Timeout = 10;
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