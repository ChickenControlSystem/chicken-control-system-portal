using ControlLine.Dto;
using ControlLine.Exception.Hardware;
using NUnit.Framework;

namespace ControlLineIntegrationTests.ControlLineSocketsTests.SendOperation.Scenarios
{
    [TestFixture((byte) 2, (byte) 2, 120)]
    [Description("Given Door Is Moved 120mm Absolute, When Device Error Occurs")]
    public class MoveDoorRelativeTests : SendOperationTests
    {
        private readonly OperationDto _operation;

        public MoveDoorRelativeTests(byte operation, byte device, int ammount)
        {
            _operation = new OperationDto()
            {
                Operation = operation,
                Timeout = 1000,
                Device = device,
                Params = new[] {ammount}
            };
        }

        [SetUp]
        public new void Init()
        {
            base.Init();
        }

        [Test]
        public void SuccessResponseTest()
        {
            Assert.Throws<DeviceOffline>(() => Sut.SendOperation(_operation));
        }
    }
}