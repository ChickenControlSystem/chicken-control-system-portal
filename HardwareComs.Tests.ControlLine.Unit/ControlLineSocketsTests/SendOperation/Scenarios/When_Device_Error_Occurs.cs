using ControlLine.Dto;
using ControlLine.Exception.Hardware;
using ControlLine.Exception.Hardware.Axis;
using ControlLineUnitTests.ControlLineSocketsTests.SendOperation.Scenarios.Shared.Socket;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.SendOperation.Scenarios
{
    [TestFixture]
    public class When_Device_Error_Occurs : When_Socket_Communication_Was_Successful
    {
        private readonly DeviceFailiure _deviceFailiure = new AxisObstruction();

        protected override void When()
        {
            Status = 115;
            Operation = new OperationDto
            {
                Operation = 115,
                Device = 121,
                Params = new[] {65535},
                Timeout = Timeout
            };
            Payload = new byte[] {115, 121, 2, 255, 255};

            MockStatusValidator
                .IsError(Arg.Any<byte>())
                .Returns(true);
            MockStatusValidator
                .ValidateError(Arg.Any<byte>())
                .Returns(_deviceFailiure);
            MockSocketClient
                .Recieve()
                .Returns(new[] {Status});

            SUT.SendOperation(Operation);
        }

        //TODO: change to 1 method call
        [Test]
        public void Then_Response_Status_Was_Validated()
        {
            MockStatusValidator
                .Received(1)
                .IsError(Arg.Any<byte>());
            MockStatusValidator
                .Received()
                .IsError(Arg.Is(Status));
        }

        //TODO: change to 1 method call
        [Test]
        public void Then_Response_Error_Was_Validated()
        {
            MockStatusValidator
                .Received(1)
                .ValidateError(Arg.Any<byte>());
            MockStatusValidator
                .Received()
                .ValidateError(Arg.Is(Status));
        }

        [Test]
        public void Then_Device_Failure_Error_Occurs()
        {
            //act/assert
            Assert.Throws<AxisObstruction>(() => SUT.SendOperation(Operation));
        }
    }
}