#nullable enable
using System;
using System.Net.Sockets;
using Bootstrapping.Services.Contract.Crosscutting.Utils;
using Crosscutting.ApplicationConfiguration.Enviroment.Configuration;
using Tests.Fakes.HAL.FakeHardwareComs.RequestResponses;

namespace Tests.Fakes.HAL.FakeHardwareComs
{
    public class FakeHardwareComsServer : ITestService
    {
        private readonly Socket _socket;
        private readonly IThreadOperations _threadOperations;
        private readonly IRequestResponseCollection _requestResponseCollection;

        private FakeHardwareComsServer(
            IThreadOperations threadOperations,
            Socket socket, IRequestResponseCollection requestResponseCollection)
        {
            _threadOperations = threadOperations;
            _socket = socket;
            _requestResponseCollection = requestResponseCollection;
        }

        public FakeHardwareComsServer(
            IThreadOperations threadOperations, IRequestResponseCollection requestResponseCollection)
            : this(
                threadOperations,
                new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), requestResponseCollection)
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
                            try
                            {
                                var handle = _socket.Accept();
                                var buffer = new byte[8];
                                handle.Receive(buffer);
                                var payload = _requestResponseCollection.GetResponse(buffer);
                                if (payload != null) handle.Send(payload);

                                handle.Close();
                            }
                            catch (Exception)
                            {
                                //ignore
                            }
                    }, 50);
                }
            );
        }
    }
}