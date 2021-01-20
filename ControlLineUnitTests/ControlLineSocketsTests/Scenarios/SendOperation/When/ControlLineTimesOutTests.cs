using System.Linq;
using System.Threading;
using ControlLine.Dto;
using ControlLine.Exception;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation.When
{
    [TestFixture(11)]
    [TestFixture(20)]
    [TestFixture(7000)]
    [Description("Given ControlLineSockets.SendOperation Is Called, When Payload Cannot Be Sent")]
    public class ControlLineTimesOutTests : SendOperationTests
    {
        private readonly byte[] _payload = new byte[] {115, 121, 1, 255, 255};

        private readonly OperationDto _operation = new OperationDto()
            {Operation = 115, Device = 121, Params = new int[] {65535}};

        private readonly int _recievePeriod;

        public ControlLineTimesOutTests(int recievePeriod)
        {
            _recievePeriod = recievePeriod;
        }

        [SetUp]
        protected new void Init()
        {
            base.Init();

            //arrange
            MockSocketClient
                .When(x => x.Recieve())
                .Do(Callback.Always(x => { Thread.Sleep(_recievePeriod); }));
        }

        private void When()
        {
            try
            {
                Sut.SendOperation(_operation, TimeOut);
            }
            catch (ControlLineTimeOut)
            {
            }
        }

        private void WhenWithErrors()
        {
            Sut.SendOperation(_operation, TimeOut);
        }

        [Test]
        [Description("Then Connection Was Opened")]
        public void ConnectionOpenTest()
        {
            //act
            When();

            //assert
            MockSocketClient
                .Received(1)
                .Connect();
        }

        //TODO: change to 1 method call
        [Test]
        [Description("Then Payload Was Sent")]
        public void PayloadSendTest()
        {
            //act
            When();

            //assert
            MockSocketClient
                .Received()
                .Send(Arg.Is<byte[]>(payload => payload.SequenceEqual(_payload)));
            MockSocketClient
                .Received(1)
                .Send(Arg.Any<byte[]>());
        }

        [Test]
        [Description("Then Data Was Attempted To Be Received")]
        public void DataRecievedTest()
        {
            //act
            When();

            //assert
            MockSocketClient
                .Received(1)
                .Recieve();
        }

        [Test]
        [Description("Then Connection Was Closed")]
        public void ConnectionCloseTest()
        {
            //act
            When();

            //assert
            MockSocketClient
                .Received(1)
                .Close();
        }

        [Test]
        [Description("Then Response Status Was Not Validated")]
        public void IsErrorTest()
        {
            //act
            When();

            //assert
            MockStatusValidator
                .DidNotReceive()
                .IsError(Arg.Any<byte>());
        }

        [Test]
        [Description("Then Response Error Was Not Validated")]
        public void ValidateErrorTest()
        {
            //act
            When();

            //assert
            MockStatusValidator
                .DidNotReceive()
                .ValidateError(Arg.Any<byte>());
        }

        public void ControlLineTimeOutTest()
        {
            //assert
            Assert.Throws<ControlLineTimeOut>(WhenWithErrors);
        }
    }
}