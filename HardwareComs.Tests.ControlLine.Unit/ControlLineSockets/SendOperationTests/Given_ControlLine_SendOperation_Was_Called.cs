using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Contract.Threading;
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
        protected IControlLineStatusValidator MockStatusValidator;
        protected IThreadOperations MockThreadOperations;
        protected OperationDto Operation;

        protected override void Given()
        {
            MockStatusValidator = Substitute.For<IControlLineStatusValidator>();
            MockSocketClient = Substitute.For<ISocketClient>();
            MockThreadOperations = Substitute.For<IThreadOperations>();

            SUT = new ControlLine.Sockets.ControlLineSockets(
                MockSocketClient,
                MockStatusValidator,
                MockThreadOperations
            );
        }
    }
}