using ControlLine.Dto;
using NUnit.Framework;

namespace HardwareComs.Tests.ControlLine.Integration.ControlLineSockets.SendOperationTests.Scenarios
{
    public class When_Read_Light_Operation_Was_Sent : Given_ControlLine_SendOperation_Was_Called
    {
        private OperationResponseDto _result;

        protected override void When()
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
            Assert.AreEqual(4, _result.Status);
        }

        [Test]
        public void Then_Operation_Response_Return_Is_Zero()
        {
            Assert.AreEqual(0, _result.Returns);
        }
    }
}