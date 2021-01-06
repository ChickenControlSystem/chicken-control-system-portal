using System.Diagnostics.CodeAnalysis;
using ControlLine.Contract.Sockets;
using ControlLine.Sockets;

namespace ControlLine.UnitTets.Scenarios.Send
{
    public class Given_Send_Is_Called
    {
        protected ControlLineSockets _sut;
        protected IRawSocketClient _mockSocketClient;

        public Given_Send_Is_Called(IRawSocketClient mockSocketClient)
        {
            _mockSocketClient = mockSocketClient;
            _sut = new ControlLineSockets(_mockSocketClient);
        }
    }
}