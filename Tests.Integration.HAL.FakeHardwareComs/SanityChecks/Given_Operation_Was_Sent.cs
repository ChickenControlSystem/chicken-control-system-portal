using System.Net;
using System.Net.Sockets;
using Crosscutting.ApplicationConfiguration.Enviroment.Configuration;
using Crosscutting.UnitTest;

namespace Tests.Integration.HAL.FakeHardwareComs.SanityChecks
{
    public abstract class Given_Operation_Was_Sent : GivenWhenThenTests
    {
        protected Socket ClientSocket;
        protected EndPoint EndPoint;

        protected override void Given()
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            EndPoint = ConfigurationLoader.GetTestConfigurationLoader().GetControlLineSettings().GetEndPoint();
        }

        protected byte[] SendAndGetResponse(byte[] request)
        {
            ClientSocket.Connect(EndPoint);
            ClientSocket.Send(request);
            var buffer = new byte[8];
            ClientSocket.Receive(buffer);
            ClientSocket.Close();
            return buffer;
        }
    }
}