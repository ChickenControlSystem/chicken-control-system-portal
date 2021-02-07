using System.Net.Sockets;
using ControlLine.Dto;
using ControlLineUnitTests.ControlLineSockets.SendOperationTests.Scenarios.Shared.Socket;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSockets.SendOperationTests.Scenarios
{
    [TestFixture]
    public class When_Connection_Cannot_Be_Opened : When_Socket_Error_Occurs
    {
        private readonly SocketException _socketException = new SocketException();

        protected override void When()
        {
            Operation = new OperationDto
            {
                Operation = 115,
                Device = 121,
                Params = new[] {65535},
                Timeout = Timeout
            };
            MockSocketClient
                .When(x => x.Connect())
                .Do(x => throw _socketException);

            SUT.SendOperation(Operation);
        }

        [Test]
        public void Then_Payload_Was_Not_Sent()
        {
            MockSocketClient
                .DidNotReceive()
                .Send(Arg.Any<byte[]>());
        }

        [Test]
        public void Then_Data_Was_Not_Received()
        {
            MockSocketClient
                .DidNotReceive()
                .Recieve();
        }
    }
}