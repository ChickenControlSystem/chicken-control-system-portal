using BLL.Common.Sequence;
using HAL.Models.Contract;
using HAL.Operations.Enum;
using NSubstitute;
using NUnit.Framework;

namespace BLL.HardwareModules.Door.Tests.Unit.Commands.CloseDoorCommandTests.RunTests
{
    [TestFixture(OperationResultEnum.Failiure, SequenceResultEnum.Fail)]
    [TestFixture(OperationResultEnum.Succeess, SequenceResultEnum.Success)]
    public class When_Run : Given_CloseDoorCommand_Was_Run
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
        public void Then_Validate_Operation_Method_Was_Called()
        {
            MockErrorValidateOperationService
                .Received(1)
                .GetSequenceResult(Arg.Any<OperationResultEnum>());
            MockErrorValidateOperationService
                .Received()
                .GetSequenceResult(Arg.Is(_operationResult));
        }

        [Test]
        public void Then_Search_Move_For_Door_To_Floor_Sensor_Occured()
        {
            MockAxisOperations
                .Received(1)
                .MoveAxisSearch(
                    Arg.Any<IDoor>(),
                    Arg.Any<IFloorSensor>(),
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
                    Arg.Is<IFloorSensor>(
                        device =>
                            device.Id == 1 &&
                            device.Name == "Floor Sensor"
                    ),
                    Arg.Is(false)
                );
        }

        [Test]
        public void Then_Command_Result_Equals_Search_Move_Result()
        {
            Assert.AreEqual(_expectedResult, _result);
        }
    }
}