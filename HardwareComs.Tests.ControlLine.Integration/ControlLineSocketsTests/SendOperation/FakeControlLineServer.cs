using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using ControlLine.Contract.Threading;
using NUnit.Framework;

namespace ControlLineIntegrationTests.ControlLineSocketsTests.SendOperation
{
    public class FakeControlLineServer
    {
        private readonly Socket _socket;
        private readonly IThreadOperations _threadOperations;
        private readonly Dictionary<byte[], byte[]> _requestResponses;

        private FakeControlLineServer(Socket socket, Dictionary<byte[], byte[]> requestResponses,
            IThreadOperations threadOperations)
        {
            _socket = socket;
            _requestResponses = requestResponses;
            _threadOperations = threadOperations;
        }

        public FakeControlLineServer(EndPoint endPoint, Dictionary<byte[], byte[]> requestResponses,
            IThreadOperations threadOperations)
            : this(new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp
                ),
                requestResponses,
                threadOperations
            )
        {
            _socket.Bind(endPoint);
        }

        public void Run()
        {
            _threadOperations.RunBackground(() =>
                _threadOperations.WaitUntilActionTimeout(
                    () =>
                    {
                        while (true)
                        {
                            _socket.Listen(10);
                            var client = _socket.Accept();
                            var buffer = new byte[8];
                            client.Receive(buffer);
                            _requestResponses.First().Key.ToList().ForEach(x => TestContext.Out.Write(x));
                            client.Send(
                                _requestResponses
                                    .ToList()
                                    .Where(x => x.Key.SequenceEqual(buffer))
                                    .Select(x => x.Value)
                                    .First()
                            );
                            client.Close();
                        }
                    },
                    30000
                ));
        }
    }
}