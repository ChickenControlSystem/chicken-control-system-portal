using System.Net.Sockets;
using ControlLine.Dto;
using ControlLine.Exception.Hardware;
using NUnit.Framework;

namespace ControlLineIntegrationTests.ControlLineSocketsTests.SendOperation.Scenarios
{
    [TestFixture((byte) 2, (byte) 2, 120)]
    [Description("Given Door Is Moved 120mm Absolute, When Device Error Occurs")]
    public class SanityCheckTests : SendOperationTests
    {
        [SetUp]
        public new void Init()
        {
            base.Init();
        }

        private readonly OperationDto _operation;

        public SanityCheckTests(byte operation, byte device, int ammount)
        {
            _operation = new OperationDto
            {
                Operation = operation,
                Timeout = 1000,
                Device = device,
                Params = new[] {ammount}
            };
        }

        [Test]
        [NonParallelizable]
        [Description("Then The Device Error Is Received, Then The Control Line Times Out")]
        public void SanityCheck()
        {
            //act/assert
            Assert.Throws<DeviceOffline>(() => Sut.SendOperation(_operation));
            Assert.Throws<SocketException>(() => Sut.SendOperation(_operation));
        }
    }
}