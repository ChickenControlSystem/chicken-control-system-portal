using System.Linq;
using System.Net.Sockets;
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
                10, 20, 30, 10, 120, 0, 0, 0
            };
        }

        [SetUp]
        protected new void Init()
        {
            base.Init();
        }

        private void When()
        {
            Client.Connect();
            Client.Send(_sendToServer);
            _result = Client.Recieve();
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
    }
}