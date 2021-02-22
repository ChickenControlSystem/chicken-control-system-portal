using ControlLine.Dto;
using NSubstitute;
using Sockets.Client;
using UnitTest;

namespace HardwareComs.Tests.ControlLine.Unit.ControlLineSockets.SendOperationTests
{
    public abstract class
        Given_ControlLine_SendOperation_Was_Called : GenericGivenWhenThenTests<
            global::ControlLine.Sockets.ControlLineSockets>
    {
        protected ISocketClient MockSocketClient;
        protected OperationDto Operation;

        public override void Given()
        {
            MockSocketClient = Substitute.For<ISocketClient>();

            SUT = new global::ControlLine.Sockets.ControlLineSockets(
                MockSocketClient
            );
        }
    }
}