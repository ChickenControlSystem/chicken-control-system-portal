using ControlLine.Contract.Sockets;
using ControlLine.Sockets;
using NSubstitute;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation
{
    public abstract class Given_Read_Is_Called
    {

        protected readonly ControlLineSockets Sut;
        protected readonly IRawSocketClient MockSocketClient;

        protected Given_Read_Is_Called()
        {
            MockSocketClient = Substitute.For<IRawSocketClient>();
            Sut = new ControlLineSockets(MockSocketClient);
        }
    }
}