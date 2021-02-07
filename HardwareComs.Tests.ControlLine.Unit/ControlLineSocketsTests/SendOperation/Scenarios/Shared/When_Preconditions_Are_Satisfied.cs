using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.SendOperation.Scenarios.Shared
{
    public abstract class When_Preconditions_Are_Satisfied : Given_ControlLine_SendOperation_Was_Called
    {
        protected byte[] Payload;

        [Test]
        public void Then_Connection_Was_Closed()
        {
            MockSocketClient
                .Received(1)
                .Close();
        }

        [Test]
        public void Then_Connection_Was_Attempted_To_Be_Opened()
        {
            MockSocketClient
                .Received(1)
                .Connect();
        }
    }
}