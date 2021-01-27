using System.Net.Sockets;
using NUnit.Framework;

namespace ControlLineIntegrationTests.BerkeleyRawSocketClientTests.Scenarios
{
    [TestFixture]
    [NonParallelizable]
    [Description("Given The Sockets Server Is Down, When The Client Attempts To Connect")]
    public class ServerOfflineTests : SocketClientTests
    {
        [SetUp]
        protected new void Init()
        {
            base.Init();
        }

        private void When()
        {
            Client.Connect();
        }

        [Test]
        [Description("Then Socket Error Occurs")]
        public void SocketErrorTest()
        {
            //act/assert
            Assert.Throws<SocketException>(When);
        }
    }
}