using System.Collections.Generic;
using System.Net.Sockets;
using ControlSystem.Tests.Enviroment.ControlSystem.Configuration;
using Threading;

namespace HardwareComs.Tests.Common.FakeHardwareComs
{
    public class FakeHardwareComsServer
    {
        private readonly Socket _socket;
        private readonly IThreadOperations _threadOperations;
        private readonly Dictionary<byte[], byte[]> _requestResponseCollection;

        private FakeHardwareComsServer(IThreadOperations threadOperations, Socket socket,
            Dictionary<byte[], byte[]> requestResponseCollection)
        {
            _threadOperations = threadOperations;
            _socket = socket;
            _requestResponseCollection = requestResponseCollection;
        }

        public FakeHardwareComsServer(IThreadOperations threadOperations)
            : this(
                threadOperations,
                new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp),
                new Dictionary<byte[], byte[]>()
                {
                    {
                        //LIGHT SENSOR, READ
                        //SUCCESS, 200 LUX
                        new byte[] {1, 1, 0, 0, 0, 0, 0, 0},
                        new byte[] {1, 1, 200, 0, 0, 0, 0, 0}
                    }
                }
            )
        {
            _socket.Bind(
                ConfigurationLoader
                    .GetTestConfigurationLoader()
                    .GetControlLineSettings()
                    .GetEndPoint()
            );
        }

        public void Run()
        {
            _socket.Listen(10);
            _threadOperations.RunBackground(
                () =>
                {
                    _threadOperations.WaitUntilActionTimeout(() =>
                    {
                        var handle = _socket.Accept();
                        var buffer = new byte[8];
                        handle.Receive(buffer);
                        handle.Send(_requestResponseCollection[buffer]);
                    }, 500);
                }
            );
        }
    }
}