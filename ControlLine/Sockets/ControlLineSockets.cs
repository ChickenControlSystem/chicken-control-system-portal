using System.Net;
using System.Net.Sockets;
using ControlLine.Contract;

namespace ControlLine.Sockets
{
    public class ControlLineSockets : IControlLine
    {
        private UdpClient _udpClient;
        
        private ControlLineSockets(UdpClient udpClient)
        {
            _udpClient = udpClient;
        }

        public string Recieve()
        {
            throw new System.NotImplementedException();
        }

        public void Send(string data)
        {
            throw new System.NotImplementedException();
        }
    }
}
