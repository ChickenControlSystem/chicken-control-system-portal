using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Unit.HAL.ControlLine.ControlLineSockets.SendOperationTests.Scenarios.Shared.Socket
{
    public abstract class When_Socket_Communication_Was_Successful : When_Preconditions_Are_Satisfied
    {
        protected byte Status;

        //TODO: change to 1 method call
        [Test]
        public void Then_Payload_Was_Sent()
        {
            MockSocketClient
                .Received()
                .Send(Arg.Is<byte[]>(payload => payload.SequenceEqual(Payload)));
            MockSocketClient
                .Received(1)
                .Send(Arg.Any<byte[]>());
        }

        [Test]
        public void Then_Data_Was_Received()
        {
            MockSocketClient
                .Received(1)
                .Recieve();
        }

        [Test]
        public void Then_Communication_Was_In_Order()
        {
            Received.InOrder(
                () =>
                {
                    MockSocketClient.Connect();
                    MockSocketClient.Send(Arg.Any<byte[]>());
                    MockSocketClient.Recieve();
                    MockSocketClient.Close();
                }
            );
        }
    }
}