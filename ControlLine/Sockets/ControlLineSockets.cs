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
        private IControlLineStatusValidator _statusValidator;

        public ControlLineSockets(IRawSocketClient socketClient, IControlLineStatusValidator statusValidator)
        {
            _socketClient = socketClient;
            _statusValidator = statusValidator;
        }
        
        public OperationResponseDto SendOperation(OperationDto operationDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
