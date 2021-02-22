using HAL.Models.Device;
using HAL.Operations.Enum;
using NUnit.Framework;

namespace HAL.Operations.Tests.Integration.AxisOperations
{
    public class When_Absolute_Move_Was_Sent : Given_Operation_Was_Called
    {
        [Test]
        public void Then_Move_Is_Move_Result()
        {
            Assert.AreEqual(OperationResultEnum.Failiure, SUT.MoveAxisAbsolute(new DoorAxis(), 120));
        }
    }
}