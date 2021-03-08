using NUnit.Framework;

namespace Tests.Integration.HAL.FakeHardwareComs.SanityChecks.Scenarios
{
    public class When_Read_Light_Sensor_Was_Sent : Given_Operation_Was_Sent
    {
        private byte[] _result;

        public override void When()
        {
            _result = SendAndGetResponse(new byte[] {1, 1});
        }

        [Test]
        public void Then_200_Lux_Is_Returned()
        {
            Assert.AreEqual(1, _result[1]);
            Assert.AreEqual(200, _result[2]);
        }

        [Test]
        public void Then_Success_Is_Returned()
        {
            Assert.AreEqual(1, _result[0]);
        }
    }
}