using ControlLine.Dto;
using NUnit.Framework;

namespace ControlLineIntegrationTests.ControlLineSocketsTests.SendOperation.Scenarios
{
    [TestFixture((byte) 1, (byte) 1, (short) 500, (byte) 1)]
    [Description("Given Light Is Fetched From, When Value Is Retrieved Successfully")]
    public class GetLightValueTests : SendOperationTests
    {
        private readonly OperationDto _operation;
        private readonly OperationResponseDto _operationResponse;

        private OperationResponseDto _result;

        public GetLightValueTests(byte operation, byte device, short luxValue, byte responseCode)
        {
            _operationResponse = new OperationResponseDto()
            {
                Status = responseCode,
                Returns = luxValue
            };
            _operation = new OperationDto()
            {
                Operation = operation,
                Timeout = 1000,
                Device = device,
                Params = new int[] { }
            };
        }

        [SetUp]
        public new void Init()
        {
            base.Init();
            _result = Sut.SendOperation(_operation);
        }

        [Test]
        [NonParallelizable]
        public void SuccessResponseTest()
        {
            Assert.AreEqual(_result.Returns, _operationResponse.Returns);
            Assert.AreEqual(_result.Status, _operationResponse.Status);
        }
    }
}