using ControlLine.Dto;
using NUnit.Framework;

namespace ControlLineIntegrationTests.ControlLineSocketsTests.SendOperation.Scenarios
{
    [TestFixture((byte) 3, (byte) 2, 12, (byte) 1)]
    [Description("Given Door Is Moved 12mm Relative, When Move Occurs Successfully")]
    public class MoveDoorAbsoluteTests : SendOperationTests
    {
        private readonly OperationDto _operation;
        private readonly OperationResponseDto _operationResponse;

        private OperationResponseDto _result;

        public MoveDoorAbsoluteTests(byte operation, byte device, int ammount, byte responseCode)
        {
            _operationResponse = new OperationResponseDto()
            {
                Status = responseCode
            };
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
            _result = Sut.SendOperation(_operation);
        }

        [Test]
        public void SuccessResponseTest()
        {
            Assert.AreEqual(_result.Returns, _operationResponse.Returns);
            Assert.AreEqual(_result.Status, _operationResponse.Status);
        }
    }
}