using System.Net.Sockets;
using Crosscutting.Services.Contract.HAL.Dto;
using NSubstitute;
using NUnit.Framework;
using Tests.Unit.HAL.ControlLine.ControlLineSockets.SendOperationTests.Scenarios.Shared;

namespace Tests.Unit.HAL.ControlLine.ControlLineSockets.SendOperationTests.Scenarios
{
    [TestFixture]
    public class When_Connection_Cannot_Be_Opened : When_Preconditions_Are_Satisfied
    {
        private readonly SocketException _socketException = new SocketException();

        protected override void When()
        {
            Operation = new OperationDto
            {
                Operation = 115,
                Device = 121,
                Params = new[] {65535}
            };
            MockSocketClient
                .When(x => x.Connect())
                .Do(x => throw _socketException);

            Assert.Throws<SocketException>(() => SUT.SendOperation(Operation));
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