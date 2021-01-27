using System.Linq;
using NUnit.Framework;

namespace ControlLineIntegrationTests.BerkeleyRawSocketClientTests.Scenarios
{
    [TestFixture]
    [NonParallelizable]
    [Description("Given The Sockets Server Is Up, When The Data Is Sent And Not Received")]
    public class AbruptCloseTests : SocketClientTests
    {
        private readonly byte[] _recievedFromServer;
        private readonly byte[] _sendToServer;
        private byte[] _result;

        public AbruptCloseTests()
        {
            _sendToServer = new byte[] {10, 20, 30};
            ServerPayload = new byte[] {10, 120};
            _recievedFromServer = new byte[]
            {
                0, 0, 0, 0, 0, 0, 0, 0
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
        [Description("Then The Blank Payload Was Received")]
        public void PayloadRecievedTest()
        {
            //arrange
            Server.RunRecieveClose();

            //act
            When();

            //assert
            Assert.IsTrue(_recievedFromServer.SequenceEqual(_result));
        }
    }
}