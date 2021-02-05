using System.Net.Sockets;
using System.Threading;
using NUnit.Framework;

namespace ControlLineIntegrationTests.ThreadOperationsTests.WaitUntilTimeout.Scenarios
{
    [TestFixture(0)]
    [TestFixture(200)]
    [Description("Given ThreadOperations.WaitUntilTimeout Is Called, When Error Occurs In Call")]
    public class CallThrowsException : WaitUntilTimeoutTests
    {
        [SetUp]
        protected new void Init()
        {
            base.Init();
        }

        private readonly int _timeoutPeriod;

        public CallThrowsException(int timeoutPeriod)
        {
            _timeoutPeriod = timeoutPeriod;
        }

        private void When()
        {
            Sut.WaitUntilFuncTimeout(SutCall, Timeout);
        }

        private byte[] SutCall()
        {
            Thread.Sleep(_timeoutPeriod);
            throw new SocketException();
        }

        [Test]
        [Description("Then Error Occurs")]
        public void ErrorTest()
        {
            Assert.Throws<SocketException>(When);
        }
    }
}