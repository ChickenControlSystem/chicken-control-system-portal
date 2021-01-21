using System.Linq;
using System.Threading;
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
                0, 0, 0, 0, 0,
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
            Port = 5001;
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
            Server.RunRecieveClose();

            //act
            When();

            //assert
            Assert.IsTrue(_recievedFromServer.SequenceEqual(_result));
        }

        [TearDown]
        public void TearDown()
        {
            CoolDown();
        }
    }
}