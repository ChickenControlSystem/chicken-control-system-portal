using ControlLine.Dto;
using ControlLineUnitTests.ControlLineSockets.SendOperationTests.Scenarios.Shared.Socket;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSockets.SendOperationTests.Scenarios
{
    [TestFixture((byte) 115, (byte) 121, new[] {65535}, new byte[] {115, 121, 2, 255, 255}, (byte) 115, 65535,
        new byte[] {115, 2, 255, 255})]
    [TestFixture((byte) 100, (byte) 50, new[] {120}, new byte[] {100, 50, 1, 120}, (byte) 115, 255,
        new byte[] {115, 1, 255})]
    [TestFixture((byte) 50, (byte) 90, new[] {111, 112}, new byte[] {50, 90, 1, 111, 1, 112}, (byte) 115, 4351,
        new byte[] {115, 2, 255, 16})]
    [TestFixture((byte) 40, (byte) 112, new[] {123, 321}, new byte[] {40, 112, 1, 123, 2, 65, 1}, (byte) 115, 16,
        new byte[] {115, 1, 16})]
    public class When_Response_Retrieved_Successfully : When_Socket_Communication_Was_Successful
    {
        private readonly byte[] _response;
        private readonly byte _status;
        private readonly int _returnData;
        private OperationResponseDto _operationResponse;
        private OperationResponseDto _result;

        public When_Response_Retrieved_Successfully(byte operation, byte deviceId, int[] parameters, byte[] payload,
            byte status,
            int returnData, byte[] response)
        {
            Payload = payload;
            _response = response;
            _status = status;
            _returnData = returnData;
            Operation = new OperationDto
            {
                Operation = operation,
                Device = deviceId,
                Params = parameters
            };
        }

        protected override void When()
        {
            _operationResponse = new OperationResponseDto
            {
                Status = _status,
                Returns = _returnData
            };

            MockSocketClient
                .Recieve()
                .Returns(_response);

            _result = SUT.SendOperation(Operation);
        }

        [Test]
        public void Then_Operation_Response_Is_Returned()
        {
            Assert.AreEqual(_operationResponse.Returns, _result.Returns);
            Assert.AreEqual(_operationResponse.Status, _result.Status);
        }
    }
}