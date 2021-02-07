using System.Linq;
using System.Net.Sockets;
using ControlLine.Dto;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.SendOperation.Scenarios
{
    [TestFixture]
    public class When_Payload_Cannot_Be_Sent : Given_ControlLine_SendOperation_Was_Called
    {
        private readonly SocketException _socketException = new SocketException();
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
                .When(x => x.Send(Arg.Any<byte[]>()))
                .Do(x => throw _socketException);

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
        public void Then_Data_Was_Not_Received()
        {
            MockSocketClient
                .DidNotReceive()
                .Recieve();
        }
    }
}