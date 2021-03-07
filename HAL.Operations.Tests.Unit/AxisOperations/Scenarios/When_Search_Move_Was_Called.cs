using System.Linq;
using Crosscutting.Contract.HAL.Dto;
using Crosscutting.Contract.HAL.Enum;
using HAL.Models.Device;
using NSubstitute;
using NUnit.Framework;

namespace HAL.Operations.Tests.Unit.AxisOperations.Scenarios
{
    [TestFixture(OperationResultEnum.Failiure, (byte) 3, (byte) 1, true)]
    [TestFixture(OperationResultEnum.Succeess, (byte) 1, (byte) 1, true)]
    [TestFixture(OperationResultEnum.Failiure, (byte) 3, (byte) 0, false)]
    [TestFixture(OperationResultEnum.Succeess, (byte) 1, (byte) 0, false)]
    public class When_Search_Move_Was_Called : Given_Operation_Was_Called
    {
        private readonly OperationResultEnum _moveResult;
        private readonly byte _errorCode;
        private readonly byte _directionPayload;
        private readonly bool _direction;
        private OperationResultEnum _result;

        public When_Search_Move_Was_Called(OperationResultEnum moveResult, byte errorCode, byte directionPayload,
            bool direction)
        {
            _moveResult = moveResult;
            _errorCode = errorCode;
            _directionPayload = directionPayload;
            _direction = direction;
        }

        public override void When()
        {
            var operationResponse = new OperationResponseDto {Status = _errorCode};
            MockControlLine
                .SendOperation(Arg.Any<OperationDto>())
                .Returns(operationResponse);
            MockErrorService
                .Validate(Arg.Any<byte>())
                .Returns(_moveResult);

            _result = SUT.MoveAxisSearch(new DoorAxis(), new CeilingDigitalSensor(), _direction);
        }

        [Test]
        public void Then_Operation_Was_Sent()
        {
            MockControlLine
                .Received()
                .SendOperation(Arg.Is<OperationDto>(
                    operation =>
                        operation.Device == 2 &&
                        operation.Operation == 4 &&
                        operation.Params.SequenceEqual(new[] {3, _directionPayload})
                ));
            MockControlLine
                .Received(1)
                .SendOperation(Arg.Any<OperationDto>());
        }

        [Test]
        public void Then_Move_Is_Move_Result()
        {
            Assert.AreEqual(_moveResult, _result);
        }
    }
}