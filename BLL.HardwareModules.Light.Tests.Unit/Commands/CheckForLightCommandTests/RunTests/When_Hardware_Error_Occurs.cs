using BLL.Common.Sequence;
using HAL.Models.Contract;
using HAL.Operations.Dto;
using HAL.Operations.Enum;
using NSubstitute;
using NUnit.Framework;

namespace BLL.HardwareModules.Light.Tests.Unit.Commands.CheckForLightCommandTests.RunTests
{
    public class When_Hardware_Error_Occurs : Given_CheckForLightCommand
    {
        private SequenceResultEnum _result;

        public override void When()
        {
            MockAnalogOperations
                .Read(Arg.Any<IDevice>())
                .Returns(new OperationResult
                {
                    ResultStatus = OperationResultEnum.Failiure
                });
            MockValidateOperationService
                .GetSequenceResult(Arg.Any<OperationResultEnum>())
                .Returns(SequenceResultEnum.Fail);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Command_Fails()
        {
            Assert.AreEqual(SequenceResultEnum.Fail, _result);
        }

        [Test]
        public void Then_10_Minute_Delay_Occurs_Once()
        {
            MockThreadingOperations
                .Received()
                .SyncronousDelay(Arg.Is(600000d));
            MockThreadingOperations
                .Received(1)
                .SyncronousDelay(Arg.Any<double>());
        }

        [Test]
        public void Then_Light_Value_Is_Read_Once()
        {
            MockAnalogOperations
                .Received()
                .Read(Arg.Is(MockLightSensor));
            MockAnalogOperations
                .Received(1)
                .Read(Arg.Any<IDevice>());
        }
    }
}