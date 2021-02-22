using System.Linq;
using ControlLine.Dto;
using HAL.Models.Device;
using HAL.Operations.Dto;
using HAL.Operations.Enum;
using NSubstitute;
using NUnit.Framework;

namespace HAL.Operations.Tests.Unit.AnalogOperations.Scenarios
{
    [TestFixture(OperationResultEnum.Failiure, (byte) 4)]
    [TestFixture(OperationResultEnum.Succeess, (byte) 1)]
    public class When_Read_Operation_Was_Called : Given_Operation_Was_Called
    {
        private readonly OperationResultEnum _readResult;
        private readonly byte _status;

        private OperationResult _result;

        public When_Read_Operation_Was_Called(OperationResultEnum readResult, byte status)
        {
            _readResult = readResult;
            _status = status;
        }

        public override void When()
        {
            var light = new LightAnolougeSensor();

            MockControlLine
                .SendOperation(Arg.Any<OperationDto>())
                .Returns(new OperationResponseDto
                {
                    Status = _status,
                    Returns = 200
                });
            MockErrorService
                .Validate(Arg.Any<byte>())
                .Returns(_readResult);

            _result = SUT.Read(light);
        }

        [Test]
        public void Then_The_Valid_Operation_Was_Called()
        {
            MockControlLine
                .Received()
                .SendOperation(Arg.Is<OperationDto>(
                    x =>
                        x.Device == 1 &&
                        x.Operation == 1 &&
                        x.Params.SequenceEqual(new int[] { })
                ));
            MockControlLine
                .Received(1)
                .SendOperation(Arg.Any<OperationDto>());
        }

        [Test]
        public void Then_Error_Is_Validated()
        {
            MockErrorService
                .Received()
                .Validate(_status);
            MockErrorService
                .Received(1)
                .Validate(Arg.Any<byte>());
        }

        [Test]
        public void Then_Steps_Were_Executed_In_Order()
        {
            Received.InOrder(
                () =>
                {
                    MockControlLine
                        .SendOperation(Arg.Any<OperationDto>());
                    MockErrorService
                        .Validate(Arg.Any<byte>());
                }
            );
        }

        [Test]
        public void Then_Valid_Result_Is_Returned()
        {
            Assert.AreEqual(200, _result.Return);
            Assert.AreEqual(_readResult, _result.ResultStatus);
        }
    }
}