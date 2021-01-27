using System.Collections.Generic;
using System.Net.Sockets;
using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Contract.Threading;
using ControlLine.ControlLineStatusValidator;
using ControlLine.Sockets;
using ControlLine.Threading;
using ControlSystem.Tests.Enviroment;

namespace ControlLineIntegrationTests.ControlLineSocketsTests.SendOperation
{
    public class SendOperationTests
    {
        protected ISocketClient SocketClient;
        protected IControlLineStatusValidator ControlLineStatusValidator;
        protected IThreadOperations ThreadOperations;
        protected IControlLine Sut;
        protected FakeControlLineServer FakeControlLineServer;

        protected void Init()
        {
            ThreadOperations = new ThreadOperations();
            SocketClient = new BerkeleyTcpClient(
                ConfigurationLoader.GetTestConfigurationLoader().GetControlLineSettings().GetEndPoint(),
                ControlLineSockets.MaxPayloadLength,
                ThreadOperations,
                250
            );
            try
            {
                FakeControlLineServer = new FakeControlLineServer(
                    ConfigurationLoader.GetTestConfigurationLoader().GetControlLineSettings().GetEndPoint(),
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
                    ThreadOperations
                );
                FakeControlLineServer.Run();
            }
            catch (SocketException)
            {
                //TODO: handle
            }

            ControlLineStatusValidator = new ControlLineStatusValidator();
            Sut = new ControlLineSockets(
                SocketClient,
                ControlLineStatusValidator,
                ThreadOperations
            );
        }
    }
}