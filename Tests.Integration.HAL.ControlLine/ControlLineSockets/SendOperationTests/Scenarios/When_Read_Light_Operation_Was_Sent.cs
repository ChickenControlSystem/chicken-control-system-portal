using Crosscutting.Services.Contract.HAL.Dto;
using NUnit.Framework;

namespace Tests.Integration.HAL.ControlLine.ControlLineSockets.SendOperationTests.Scenarios
{
    public class When_Read_Light_Operation_Was_Sent : Given_ControlLine_SendOperation_Was_Called
    {
        private OperationResponseDto _result;

        public override void When()
        {
            _result = SUT.SendOperation(
                new OperationDto
                {
                    Device = 1,
                    Operation = 1,
                    Params = new int[] { }
                }
            );
        }

        [Test]
        public void Then_Operation_Response_Status_Is_Successful()
        {
            Assert.AreEqual(1, _result.Status);
        }

        [Test]
        public void Then_Operation_Response_Return_Is_200()
        {
            Assert.AreEqual(200, _result.Returns);
        }
    }
}