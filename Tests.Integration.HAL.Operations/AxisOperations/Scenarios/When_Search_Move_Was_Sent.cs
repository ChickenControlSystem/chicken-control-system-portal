using Bootstrapping.Services.Contract.HAL.Enum;
using HAL.Models.Device;
using NUnit.Framework;

namespace Tests.Integration.HAL.Operations.AxisOperations.Scenarios
{
    public class When_Search_Move_Was_Sent : Given_Operation_Was_Called
    {
        [Test]
        public void Then_Move_Is_Move_Result()
        {
            Assert.AreEqual(OperationResultEnum.Succeess,
                SUT.MoveAxisSearch(new DoorAxis(), new CeilingDigitalSensor(), true));
        }
    }
}