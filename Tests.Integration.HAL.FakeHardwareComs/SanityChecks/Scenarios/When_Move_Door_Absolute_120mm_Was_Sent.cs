using NUnit.Framework;

namespace Tests.Integration.HAL.FakeHardwareComs.SanityChecks.Scenarios
{
    public class When_Move_Door_Absolute_120_Was_Sent : Given_Operation_Was_Sent
    {
        private byte[] _result;

        protected override void When()
        {
            _result = SendAndGetResponse(new byte[] {2, 2, 1, 120});
        }

        [Test]
        public void Then_No_Value_Is_Returned()
        {
            Assert.AreEqual(3, _result[1]);
        }

        [Test]
        public void Then_Device_Offline_Is_Returned()
        {
            Assert.AreEqual(4, _result[0]);
        }
    }
}