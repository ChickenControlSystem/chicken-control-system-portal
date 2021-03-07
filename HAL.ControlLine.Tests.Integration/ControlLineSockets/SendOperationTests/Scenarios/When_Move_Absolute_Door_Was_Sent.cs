using HAL.ControlLine.Dto;
using NUnit.Framework;

namespace HAL.ControlLine.Tests.Integration.ControlLineSockets.SendOperationTests.Scenarios
{
    public class When_Move_Absolute_Door_Was_Sent : Given_ControlLine_SendOperation_Was_Called
    {
        private OperationResponseDto _result;

        public override void When()
        {
            _result = SUT.SendOperation(
                new OperationDto
                {
                    Device = 2,
                    Operation = 2,
                    Params = new[] {120}
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