using System.Net;
using System.Net.Sockets;
using ControlLine.Contract;
using ControlLine.Contract.Sockets;

namespace ControlLine.Sockets
{
    public class ControlLineSockets : IControlLine
    {

        private IRawSocketClient _socketClient;

        public ControlLineSockets(IRawSocketClient socketClient)
        {
            _socketClient = socketClient;
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
