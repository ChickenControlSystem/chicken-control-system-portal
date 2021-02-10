using System.Linq;
using ControlLine.Dto;
using HAL.Models.Device;
using HAL.Operations.Enum;
using NSubstitute;
using NUnit.Framework;

namespace HAL.Operations.Tests.Unit.AxisOperations.Relative
{
    public class When_Error_Occurs : Given_Operation_Was_Called
    {
        private OperationResultEnum _result;

        protected override void When()
        {
            MockControlLine
                .SendOperation(Arg.Any<OperationDto>())
                .Returns(new OperationResponseDto {Status = 4});
            MockErrorService
                .Validate(Arg.Any<int>())
                .Returns(OperationResultEnum.Failiure);

            _result = SUT.MoveAxisRelative(new DoorAxis(), 120);
        }

        [Test]
        public void Then_Error_Validator_Was_Called()
        {
            MockErrorService
                .Received()
                .Validate(Arg.Is(4));
            MockErrorService
                .Received(1)
                .Validate(Arg.Any<int>());
        }

        [Test]
        public void Then_Operation_Was_Sent()
        {
            MockControlLine
                .Received()
                .SendOperation(Arg.Is<OperationDto>(
                    operation =>
                        operation.Device == 2 &&
                        operation.Operation == 3 &&
                        operation.Params.SequenceEqual(new[] {120})
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
                    MockErrorService.Validate(Arg.Any<int>());
                }
            );
        }

        [Test]
        public void Then_Move_Fails()
        {
            Assert.AreEqual(OperationResultEnum.Failiure, _result);
        }
    }
}