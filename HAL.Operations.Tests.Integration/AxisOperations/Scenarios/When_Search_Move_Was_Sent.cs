using HAL.Models.Device;
using HAL.Operations.Enum;
using NUnit.Framework;

namespace HAL.Operations.Tests.Integration.AxisOperations
{
    public class When_Search_Move_Was_Sent : Given_Operation_Was_Called
    {
        [Test]
        public void Then_No_Exception_Is_Thrown()
        {
            Assert.DoesNotThrow(() => SUT.MoveAxisSearch(new DoorAxis(), new FloorDigitalSensor(), false));
        }

        [Test]
        public void Then_Move_Is_Move_Result()
        {
            Assert.AreEqual(OperationResultEnum.Succeess,
                SUT.MoveAxisSearch(new DoorAxis(), new FloorDigitalSensor(), false));
        }
    }
}