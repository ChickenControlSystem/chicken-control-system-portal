using System.Net.Sockets;
using Crosscutting.Contract.HAL.ControlLine;
using NUnit.Framework;

namespace HAL.ControlLine.Tests.Integration.ControlLineSockets.SendOperationTests.Scenarios
{
    public class When_Socket_Error_Occurs : Given_ControlLine_SendOperation_Was_Called
    {
        [TestCase(10, 123, new[] {123, 1})]
        [TestCase(6, 5, new int[] { })]
        [TestCase(87, 7, new[] {1})]
        public void Then_Socket_Exception_Occurs(byte device, byte operation, int[] parameters)
        {
            Assert.Throws<SocketException>(() =>
            {
                SUT.SendOperation(
                    new OperationDto
                    {
                        Device = device,
                        Operation = operation,
                        Params = parameters
                    }
                );
            });
        }
    }
}