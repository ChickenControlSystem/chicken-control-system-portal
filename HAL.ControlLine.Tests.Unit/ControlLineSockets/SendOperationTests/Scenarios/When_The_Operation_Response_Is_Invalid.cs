using System;
using Crosscutting.Contract.HAL.ControlLine;
using HAL.ControlLine.Tests.Unit.ControlLineSockets.SendOperationTests.Scenarios.Shared.Socket;
using NSubstitute;
using NUnit.Framework;

namespace HAL.ControlLine.Tests.Unit.ControlLineSockets.SendOperationTests.Scenarios
{
    [TestFixture("Data Type Is Less Than 1", new byte[] {1, 0, 116, 0, 0, 0, 0, 0})]
    [TestFixture("Data Type Is Greater Than 3", new byte[] {1, 4, 116, 0, 0, 0, 0, 0})]
    [TestFixture("Status Code Is Less Than 1", new byte[] {0, 3, 116, 0, 0, 0, 0, 0})]
    [TestFixture("Response Is 9 Bytes", new byte[] {1, 3, 116, 0, 0, 0, 0, 0, 0})]
    [TestFixture("Response Is 12 Bytes", new byte[] {1, 3, 116, 0, 0, 0, 0, 0, 0, 0, 0, 0})]
    [TestFixture("Response Is 7 Bytes", new byte[] {1, 3, 116, 0, 0, 0, 0})]
    [TestFixture("Response Is 3 Bytes", new byte[] {1, 3, 116})]
    public class When_The_Operation_Response_Is_Invalid : When_Socket_Communication_Was_Successful
    {
        private readonly byte[] _operationResponseParams;

        public When_The_Operation_Response_Is_Invalid(string testName, byte[] operationResponseParams)
        {
            _operationResponseParams = operationResponseParams;
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

            Assert.Throws<ArgumentException>(() => SUT.SendOperation(Operation));
        }
    }
}