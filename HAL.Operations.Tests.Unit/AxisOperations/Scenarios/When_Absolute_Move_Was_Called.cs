using System.Linq;
using ControlLine.Dto;
using HAL.Models.Device;
using HAL.Operations.Enum;
using NSubstitute;
using NUnit.Framework;

namespace HAL.Operations.Tests.Unit.AxisOperations.Scenarios
{
    [TestFixture(OperationResultEnum.Failiure, (byte) 4)]
    [TestFixture(OperationResultEnum.Succeess, (byte) 1)]
    public class When_Absolute_Move_Was_Called : Given_Operation_Was_Called
    {
        private readonly OperationResultEnum _moveResult;
        private readonly byte _errorCode;
        private OperationResultEnum _result;

        public When_Absolute_Move_Was_Called(OperationResultEnum moveResult, byte errorCode)
        {
            _moveResult = moveResult;
            _errorCode = errorCode;
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

            _result = SUT.MoveAxisAbsolute(new DoorAxis(), 120);
        }

        [Test]
        public void Then_Operation_Was_Sent()
        {
            MockControlLine
                .Received()
                .SendOperation(Arg.Is<OperationDto>(
                    operation =>
                        operation.Device == 2 &&
                        operation.Operation == 2 &&
                        operation.Params.SequenceEqual(new[] {120})
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