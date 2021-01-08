using System.Net;
using System.Net.Sockets;
using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Dto;

namespace ControlLine.Sockets
{
    public class ControlLineSockets : IControlLine
    {

        private IRawSocketClient _socketClient;

        public ControlLineSockets(IRawSocketClient socketClient)
        {
            _socketClient = socketClient;
        }

        public string SendOperation(OperationDto operationDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
