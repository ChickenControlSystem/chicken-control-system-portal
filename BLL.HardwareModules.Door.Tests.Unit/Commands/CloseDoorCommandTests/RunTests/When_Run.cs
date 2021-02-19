using BLL.HardwareModules.Common.Sequence;
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
                    Arg.Any<IDevice>(),
                    Arg.Any<IDevice>(),
                    Arg.Any<bool>()
                );
            MockAxisOperations
                .Received()
                .MoveAxisSearch(
                    Arg.Is<IDevice>(
                        device =>
                            device.Id == DoorAxis.Id &&
                            device.Name == DoorAxis.Name
                    ),
                    Arg.Is<IDevice>(
                        device =>
                            device.Id == FloorSensor.Id &&
                            device.Name == FloorSensor.Name
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