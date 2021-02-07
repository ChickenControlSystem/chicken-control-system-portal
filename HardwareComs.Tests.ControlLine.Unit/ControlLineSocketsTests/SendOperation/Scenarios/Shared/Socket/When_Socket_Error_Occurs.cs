using ControlLine.Exception;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.SendOperation.Scenarios.Shared.Socket
{
    public abstract class When_Socket_Error_Occurs : When_Preconditions_Are_Satisfied
    {
        [Test]
        public void Then_Response_Status_Was_Not_Validated()
        {
            MockStatusValidator
                .DidNotReceive()
                .IsError(Arg.Any<byte>());
        }

        [Test]
        public void Then_Response_Error_Was_Not_Validated()
        {
            MockStatusValidator
                .DidNotReceive()
                .ValidateError(Arg.Any<byte>());
        }

        [Test]
        public void Then_Control_Line_Offline_Error_Occurs()
        {
            Assert.Throws<ControlLineOffline>(() => SUT.SendOperation(Operation));
        }
    }
}