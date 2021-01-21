using System.Net.Sockets;
using System.Threading;
using NUnit.Framework;

namespace ControlLineIntegrationTests.BerkeleyRawSocketClientTests.Scenarios
{
    [TestFixture]
    [NonParallelizable]
    [Description("Given The Sockets Server Is Up, When The Connection Is Very Slow")]
    public class SlowConnectionTests : SocketClientTests
    {
        private readonly byte[] _sendToServer;

        public SlowConnectionTests()
        {
            _sendToServer = new byte[] {10, 20, 30};
            ServerPayload = new byte[] {10, 120};
        }

        [SetUp]
        protected new void Init()
        {
            Port = 5002;
            base.Init();
        }

        private void When()
        {
            Client.Connect();
            Thread.Sleep(50);
            Client.Send(_sendToServer);
            Thread.Sleep(200);
            Client.Recieve();
            Client.Close();
        }

        [Test]
        [Description("Then Socket Error Occurs")]
        public void SocketErrorTest()
        {
            //arrange
            Server.RunBadConnection();

            //act/assert
            Assert.Throws<SocketException>(When);
        }

        [TearDown]
        public void TearDown()
        {
            CoolDown();
        }
    }
}