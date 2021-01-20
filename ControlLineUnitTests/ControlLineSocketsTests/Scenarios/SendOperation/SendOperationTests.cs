using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Contract.Threading;
using ControlLine.Sockets;
using NSubstitute;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation
{
    public abstract class SendOperationTests
    {
        protected ControlLineSockets Sut;
        protected IRawSocketClient MockSocketClient;
        protected IControlLineStatusValidator MockStatusValidator;
        protected IThreadOperations MockThreadOperations;

        protected const int Timeout = 10;

        protected void Init()
        {
            MockStatusValidator = Substitute.For<IControlLineStatusValidator>();
            MockSocketClient = Substitute.For<IRawSocketClient>();
            MockThreadOperations = Substitute.For<IThreadOperations>();
            Sut = new ControlLineSockets(
                MockSocketClient,
                MockStatusValidator,
                MockThreadOperations
            );
        }
    }
}