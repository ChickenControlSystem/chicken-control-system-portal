using HAL.Models.Device;
using HAL.Operations.Enum;
using NUnit.Framework;

namespace HAL.Operations.Tests.Integration.AnalogOperations
{
    public class When_Read_Operation_Was_Called : Given_Operation_Was_Called
    {
        [Test]
        public void Then_No_Exception_Is_Thrown()
        {
            Assert.DoesNotThrow(() => SUT.Read(new LightAnolougeSensor()));
        }

        [Test]
        public void Then_200_Is_Read_Successfully()
        {
            var result = SUT.Read(new LightAnolougeSensor());
            Assert.AreEqual(OperationResultEnum.Succeess, result.ResultStatus);
            Assert.AreEqual(200, result.Return);
        }
    }
}