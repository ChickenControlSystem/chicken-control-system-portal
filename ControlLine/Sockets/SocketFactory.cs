using System.Net;
using System.Net.Sockets;

namespace ControlLine.Sockets
{
    public abstract class SocketFactory
    {
        public static UdpClient GetUdpClient(IPEndPoint socket)
        {
            return new UdpClient(socket);
        }
    }
}