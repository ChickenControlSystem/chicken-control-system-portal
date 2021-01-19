using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Sockets;
using NSubstitute;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation
{
    public abstract class SendOperationTests
    {

        protected readonly ControlLineSockets Sut;
        protected readonly IRawSocketClient MockSocketClient;
        protected readonly IControlLineStatusValidator MockStatusValidator;

        protected SendOperationTests()
        {
            MockStatusValidator = Substitute.For<IControlLineStatusValidator>();
            MockSocketClient = Substitute.For<IRawSocketClient>();
            Sut = new ControlLineSockets(MockSocketClient,MockStatusValidator);
        }
    }
}