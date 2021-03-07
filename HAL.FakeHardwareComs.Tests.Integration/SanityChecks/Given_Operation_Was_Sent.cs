using System.Net;
using System.Net.Sockets;
using Crosscutting.Configuration;
using Crosscutting.UnitTest;

namespace HAL.FakeHardwareComs.Tests.Integration.SanityChecks
{
    public abstract class Given_Operation_Was_Sent : GivenWhenThenTests
    {
        protected Socket ClientSocket;
        protected EndPoint EndPoint;

        public override void Given()
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