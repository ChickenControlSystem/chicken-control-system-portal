using System.Net.Sockets;
using NUnit.Framework;

namespace HardwareComs.Tests.ControlLine.Unit.ControlLineSockets.SendOperationTests.Scenarios.Shared.Socket
{
    public abstract class When_Socket_Error_Occurs : When_Preconditions_Are_Satisfied
    {
        [Test]
        public void Then_SocketException_Is_Thrown()
        {
            Assert.Throws<SocketException>(() => SUT.SendOperation(Operation));
        }
    }
}