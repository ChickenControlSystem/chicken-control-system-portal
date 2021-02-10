﻿using System.Linq;
using ControlLine.Dto;
using HAL.Models.Device;
using HAL.Operations.Enum;
using NSubstitute;
using NUnit.Framework;

namespace HAL.Operations.Tests.Unit.AxisOperations.Scenarios
{
    [TestFixture(OperationResultEnum.Failiure, (byte) 3, (byte) 1, true)]
    [TestFixture(OperationResultEnum.Succeess, (byte) 1, (byte) 1, true)]
    [TestFixture(OperationResultEnum.Failiure, (byte) 3, (byte) 0, false)]
    [TestFixture(OperationResultEnum.Succeess, (byte) 1, (byte) 0, false)]
    public class When_Search_Move_Was_Sent : Given_Operation_Was_Called
    {
        private readonly OperationResultEnum _moveResult;
        private readonly byte _errorCode;
        private readonly byte _directionPayload;
        private readonly bool _direction;
        private OperationResultEnum _result;

        public When_Search_Move_Was_Sent(OperationResultEnum moveResult, byte errorCode, byte directionPayload,
            bool direction)
        {
            _moveResult = moveResult;
            _errorCode = errorCode;
            _directionPayload = directionPayload;
            _direction = direction;
        }

        protected override void When()
        {
            var operationResponse = new OperationResponseDto {Status = _errorCode};
            MockControlLine
                .SendOperation(Arg.Any<OperationDto>())
                .Returns(operationResponse);
            MockErrorService
                .Validate(Arg.Any<byte>())
                .Returns(_moveResult);

            _result = SUT.MoveAxisSearch(new DoorAxis(), new FloorDigitalSensor(), _direction);
        }

        [Test]
        public void Then_Error_Validator_Was_Called()
        {
            MockErrorService
                .Received()
                .Validate(Arg.Is(_errorCode));
            MockErrorService
                .Received(1)
                .Validate(Arg.Any<byte>());
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
        public void Then_Steps_Were_Executed_In_Order()
        {
            Received.InOrder(
                () =>
                {
                    MockControlLine.SendOperation(Arg.Any<OperationDto>());
                    MockErrorService.Validate(Arg.Any<byte>());
                }
            );
        }

        [Test]
        public void Then_Move_Is_Move_Result()
        {
            Assert.AreEqual(_moveResult, _result);
        }
    }
}