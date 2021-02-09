#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using ControlSystem.Tests.Enviroment.ControlSystem.Configuration;
using Threading;
using UnitTest;

namespace HardwareComs.Tests.Common.FakeHardwareComs
{
    public class FakeHardwareComsServer : ITestService
    {
        private readonly Socket _socket;
        private readonly IThreadOperations _threadOperations;
        private readonly List<Tuple<byte[], byte[]>> _requestResponseCollection;

        private FakeHardwareComsServer(IThreadOperations threadOperations, Socket socket,
            List<Tuple<byte[], byte[]>> requestResponseCollection)
        {
            _threadOperations = threadOperations;
            _socket = socket;
            _requestResponseCollection = requestResponseCollection;
        }

        public FakeHardwareComsServer(IThreadOperations threadOperations, RequestResponseFlagsDto requestResponseFlags)
            : this(
                threadOperations,
                new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp),
                RequestResponseCollection.GetRequestResponseCollection(requestResponseFlags).ToList()
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
                        while (true)
                        {
                            try
                            {
                                var handle = _socket.Accept();
                                var buffer = new byte[8];
                                handle.Receive(buffer);
                                var payload = GetResponse(buffer);
                                if (payload != null)
                                {
                                    handle.Send(payload);
                                }

                                handle.Close();
                            }
                            catch (Exception)
                            {
                                //ignore
                            }
                        }
                    }, 500);
                }
            );
        }

        private byte[]? GetResponse(byte[] request)
        {
            return _requestResponseCollection.Find(x => x.Item1.SequenceEqual(request))?.Item2;
        }
    }
}