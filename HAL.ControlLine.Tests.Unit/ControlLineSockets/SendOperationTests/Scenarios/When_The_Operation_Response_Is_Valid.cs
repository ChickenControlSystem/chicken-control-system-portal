using Crosscutting.Services.Contract.HAL.Dto;
using HAL.ControlLine.Tests.Unit.ControlLineSockets.SendOperationTests.Scenarios.Shared.Socket;
using NSubstitute;
using NUnit.Framework;

namespace HAL.ControlLine.Tests.Unit.ControlLineSockets.SendOperationTests.Scenarios
{
    [TestFixture(new byte[] {1, 1, 116, 0, 0, 0, 0, 0}, (byte) 1, 116)]
    [TestFixture(new byte[] {2, 2, 116, 116, 0, 0, 0, 0}, (byte) 2, 29812)]
    [TestFixture(new byte[] {2, 2, 116, 121, 0, 0, 0, 0}, (byte) 2, 31092)]
    [TestFixture(new byte[] {3, 3, 0, 0, 0, 0, 0, 0}, (byte) 3, 0)]
    public class When_The_Operation_Response_Is_Valid : When_Socket_Communication_Was_Successful
    {
        private readonly byte[] _operationResponseParams;
        private OperationResponseDto _result;
        private readonly OperationResponseDto _expectedResult;

        public When_The_Operation_Response_Is_Valid(byte[] operationResponseParams, byte status, int returnValue)
        {
            _operationResponseParams = operationResponseParams;
            _expectedResult = new OperationResponseDto
            {
                Status = status,
                Returns = returnValue
            };
        }

        public override void When()
        {
            Operation = new OperationDto
            {
                Device = 1, Operation = 1, Params = new[] {1}
            };
            Payload = new byte[] {1, 1, 1, 1};
            MockSocketClient
                .Recieve()
                .Returns(_operationResponseParams);

            _result = SUT.SendOperation(Operation);
        }

        [Test]
        public void Then_OperationResponse_Is_Returned()
        {
            Assert.AreEqual(_expectedResult.Returns, _result.Returns);
            Assert.AreEqual(_expectedResult.Status, _result.Status);
        }
    }
}