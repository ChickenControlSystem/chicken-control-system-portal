using System.Threading;
using NUnit.Framework;

namespace ControlLineIntegrationTests.ThreadOperationsTests.WaitUntilTimeout.Scenarios
{
    [TestFixture(240)]
    [TestFixture(200)]
    [TestFixture(100)]
    [TestFixture(10)]
    [Description("Given ThreadOperations.WaitUntilTimeout Is Called, When Call Doesnt Time Out")]
    public class DoesntTimeoutTests : WaitUntilTimeoutTests
    {
        private readonly int _timeoutPeriod;
        private byte[] _result;

        public DoesntTimeoutTests(int timeoutPeriod)
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
            _result = Sut.WaitUntilTimeout(SutCall, Timeout);
        }

        private byte[] SutCall()
        {
            Thread.Sleep(_timeoutPeriod);
            return Return;
        }

        [Test]
        [Description("Then Return Value Is Returned")]
        public void ReturnTest()
        {
            //act
            When();

            Assert.AreEqual(Return, _result);
        }
    }
}