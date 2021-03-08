using Crosscutting.Sequencing.Sequence;
using Crosscutting.Services.Contract.HAL.Enum;
using Crosscutting.Services.Contract.HAL.Interface;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Unit.BLL.HardwareModules.Door.Commands.CloseDoorCommandTests.RunTests
{
    [TestFixture(OperationResultEnum.Failiure, SequenceResultEnum.Fail)]
    [TestFixture(OperationResultEnum.Succeess, SequenceResultEnum.Success)]
    public class When_Run : Given_OpenDoorCommand
    {
        private readonly OperationResultEnum _operationResult;
        private readonly SequenceResultEnum _expectedResult;
        private SequenceResultEnum _result;

        public When_Run(OperationResultEnum operationResult, SequenceResultEnum expectedResult)
        {
            _operationResult = operationResult;
            _expectedResult = expectedResult;
        }

        protected override void When()
        {
            MockErrorValidateOperationService
                .GetSequenceResult(Arg.Any<OperationResultEnum>())
                .Returns(_expectedResult);
            MockAxisOperations
                .MoveAxisSearch(
                    Arg.Any<IDevice>(),
                    Arg.Any<IDevice>(),
                    Arg.Any<bool>()
                )
                .Returns(_operationResult);

            _result = SUT.Run();
        }

        [Test]
        public void Then_Search_Move_For_Door_To_Floor_Sensor_Occured()
        {
            MockAxisOperations
                .Received(1)
                .MoveAxisSearch(
                    Arg.Any<IDoor>(),
                    Arg.Any<ICeilingSensor>(),
                    Arg.Any<bool>()
                );
            MockAxisOperations
                .Received()
                .MoveAxisSearch(
                    Arg.Is<IDoor>(
                        device =>
                            device.Id == 2 &&
                            device.Name == "Door Axis"
                    ),
                    Arg.Is<ICeilingSensor>(
                        device =>
                            device.Id == 3 &&
                            device.Name == "Floor Sensor"
                    ),
                    Arg.Is(true)
                );
        }

        [Test]
        public void Then_Command_Result_Equals_Search_Move_Result()
        {
            Assert.AreEqual(_expectedResult, _result);
        }
    }
}