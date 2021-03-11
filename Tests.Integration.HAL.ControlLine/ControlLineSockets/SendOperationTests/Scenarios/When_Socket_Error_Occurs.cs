using System.Net.Sockets;
using Bootstrapping.Services.Contract.HAL.Dto;
using NUnit.Framework;

namespace Tests.Integration.HAL.ControlLine.ControlLineSockets.SendOperationTests.Scenarios
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