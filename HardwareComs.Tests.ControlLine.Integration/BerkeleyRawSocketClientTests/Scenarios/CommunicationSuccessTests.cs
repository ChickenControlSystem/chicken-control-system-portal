using System.Linq;
using System.Net.Sockets;
using System.Threading;
using NUnit.Framework;

namespace ControlLineIntegrationTests.BerkeleyRawSocketClientTests.Scenarios
{
    [TestFixture]
    [NonParallelizable]
    [Description("Given The Sockets Server Is Up, When The Data Is Sent And Received")]
    public class CommunicationSuccessTests : SocketClientTests
    {
        private readonly byte[] _recievedFromServer;
        private readonly byte[] _sendToServer;
        private byte[] _result;

        public CommunicationSuccessTests()
        {
            _sendToServer = new byte[] {10, 20, 30};
            ServerPayload = new byte[] {10, 120};
            _recievedFromServer = new byte[]
            {
                10, 20, 30, 10, 120,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0
            };
        }

        [SetUp]
        protected new void Init()
        {
            Port = 5000;
            base.Init();
        }

        private void When()
        {
            Client.Connect();
            Thread.Sleep(50);
            Client.Send(_sendToServer);
            Thread.Sleep(50);
            _result = Client.Recieve();
            Thread.Sleep(50);
            Client.Close();
        }

        [Test]
        [Description("Then The Payload Was Received")]
        public void PayloadRecievedTest()
        {
            //arrange
            Server.RunRecieveRespond();

            //act
            When();

            //assert
            Assert.IsTrue(_recievedFromServer.SequenceEqual(_result));
        }

        [Test]
        [Description("Then Socket Cannot Be Reused")]
        public void SocketReuseTest()
        {
            //arrange
            Server.RunRecieveRespond();
            When();
            InitClient();

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