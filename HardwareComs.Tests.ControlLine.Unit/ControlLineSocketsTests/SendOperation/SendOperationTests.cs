using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Contract.Threading;
using ControlLine.Sockets;
using NSubstitute;

namespace ControlLineUnitTests.ControlLineSocketsTests.SendOperation
{
    public abstract class SendOperationTests
    {
        protected const int Timeout = 10;
        protected ISocketClient MockSocketClient;
        protected IControlLineStatusValidator MockStatusValidator;
        protected IThreadOperations MockThreadOperations;
        protected ControlLineSockets Sut;

        protected void Init()
        {
            MockStatusValidator = Substitute.For<IControlLineStatusValidator>();
            MockSocketClient = Substitute.For<ISocketClient>();
            MockThreadOperations = Substitute.For<IThreadOperations>();
            Sut = new ControlLineSockets(
                MockSocketClient,
                MockStatusValidator,
                MockThreadOperations
            );
        }
    }
}