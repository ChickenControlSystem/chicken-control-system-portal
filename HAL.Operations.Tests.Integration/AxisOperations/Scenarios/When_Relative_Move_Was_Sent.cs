using Crosscutting.Services.Contract.HAL.Enum;
using HAL.Models.Device;
using NUnit.Framework;

namespace HAL.Operations.Tests.Integration.AxisOperations
{
    public class When_Relative_Move_Was_Sent : Given_Operation_Was_Called
    {
        [Test]
        public void Then_Move_Is_Move_Result()
        {
            Assert.AreEqual(OperationResultEnum.Succeess, SUT.MoveAxisRelative(new DoorAxis(), 28536));
        }
    }
}