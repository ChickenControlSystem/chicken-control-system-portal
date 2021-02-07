using System.Linq;
using System.Net.Sockets;
using ControlLine.Dto;
using ControlLineUnitTests.ControlLineSockets.SendOperationTests.Scenarios.Shared.Socket;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSockets.SendOperationTests.Scenarios
{
    [TestFixture]
    public class When_Response_Cannot_Be_Received : When_Socket_Error_Occurs
    {
        private readonly SocketException _socketException = new SocketException(10048);
        private readonly byte[] _payload = {115, 121, 2, 255, 255};

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
                .Recieve()
                .Throws(_socketException);

            SUT.SendOperation(Operation);
        }

        //TODO: change to 1 method call
        [Test]
        public void Then_Payload_Was_Sent()
        {
            MockSocketClient
                .Received()
                .Send(Arg.Is<byte[]>(payload => payload.SequenceEqual(_payload)));
            MockSocketClient
                .Received(1)
                .Send(Arg.Any<byte[]>());
        }

        [Test]
        public void Then_Data_Was_Attempted_To_Be_Received()
        {
            MockSocketClient
                .Received(1)
                .Recieve();
        }
    }
}