using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Contract.Threading;
using ControlLine.ControlLineStatusValidator;
using ControlLine.Sockets;
using ControlLine.Threading;

namespace ControlLineIntegrationTests.ControlLineSocketsTests.SendOperation
{
    public class SendOperationTests
    {
        private ISocketClient _socketClient;
        private IControlLineStatusValidator _controlLineStatusValidator;
        private IThreadOperations _threadOperations;
        protected IControlLine Sut;
        private FakeControlLineServer _fakeControlLineServer;

        protected void Init()
        {
            _threadOperations = new ThreadOperations();
            _socketClient = new BerkeleyTcpClient(
                new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000),
                ControlLineSockets.MaxPayloadLength,
                _threadOperations,
                250
            );
            try
            {
                _fakeControlLineServer = new FakeControlLineServer(
                    new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000),
                    new Dictionary<byte[], byte[]>()
                    {
                        {
                            //mock request: move door relative, 12mm
                            //mock response: success
                            new byte[] {3, 2, 1, 12, 0, 0, 0, 0},
                            new byte[] {1, 0, 0, 0, 0, 0, 0, 0}
                        },
                        {
                            //mock request: move door absolute, 120mm 
                            //mock response: device timeout
                            new byte[] {2, 2, 1, 120, 0, 0, 0, 0},
                            new byte[] {4, 0, 0, 0, 0, 0, 0, 0}
                        },
                        {
                            //mock request: get light
                            //mock response: success, 200 lux
                            new byte[] {1, 1, 0, 0, 0, 0, 0, 0},
                            new byte[] {1, 2, 244, 1, 0, 0, 0, 0}
                        }
                    },
                    _threadOperations
                );
                _fakeControlLineServer.Run();
            }
            catch (SocketException)
            {
                //TODO: handle
            }

            _controlLineStatusValidator = new ControlLineStatusValidator();
            Sut = new ControlLineSockets(
                _socketClient,
                _controlLineStatusValidator,
                _threadOperations
            );
        }
    }
}