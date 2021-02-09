using ControlLine.Dto;
using NUnit.Framework;

namespace HardwareComs.Tests.ControlLine.Integration.ControlLineSockets.SendOperationTests.Scenarios
{
    public class When_Move_Relative_Door_Was_Sent : Given_ControlLine_SendOperation_Was_Called
    {
        private OperationResponseDto _result;

        protected override void When()
        {
            _result = SUT.SendOperation(
                new OperationDto
                {
                    Device = 2,
                    Operation = 3,
                    Params = new[] {28536}
                }
            );
        }

        [Test]
        public void Then_Operation_Response_Status_Is_Successful()
        {
            Assert.AreEqual(1, _result.Status);
        }

        [Test]
        public void Then_Operation_Response_Return_Is_Zero()
        {
            Assert.AreEqual(0, _result.Returns);
        }
    }
}