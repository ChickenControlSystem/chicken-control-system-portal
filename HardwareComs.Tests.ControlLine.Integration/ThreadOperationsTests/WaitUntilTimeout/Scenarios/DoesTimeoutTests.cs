using System.Threading;
using ControlLine.Exception;
using NUnit.Framework;

namespace ControlLineIntegrationTests.ThreadOperationsTests.WaitUntilTimeout.Scenarios
{
    [TestFixture(251)]
    [TestFixture(300)]
    [TestFixture(500)]
    [TestFixture(600)]
    [Description("Given ThreadOperations.WaitUntilTimeout Is Called, When Call Times Out")]
    public class DoesTimeoutTests : WaitUntilTimeoutTests
    {
        private readonly int _timeoutPeriod;

        public DoesTimeoutTests(int timeoutPeriod)
        {
            _timeoutPeriod = timeoutPeriod;
        }

        [SetUp]
        protected new void Init()
        {
            base.Init();
        }

        private void When()
        {
            Sut.WaitUntilTimeout(SutCall, Timeout);
        }

        private byte[] SutCall()
        {
            Thread.Sleep(_timeoutPeriod);
            return Return;
        }

        [Test]
        [Description("Then Thread Timeout Error Occurs")]
        public void TimeoutErrorTest()
        {
            //act/assert
            Assert.Throws<ThreadTimeout>(When);
        }
    }
}