using System;
using System.Net.Sockets;
using ControlLine.Dto;
using ControlLine.Exception;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.SendOperation.Scenarios
{
    [TestFixture]
    [Description("Given ControlLineSockets.SendOperation Is Called, When Connection Cannot Be Opened")]
    public class ConnectionNotOpenedTests : SendOperationTests
    {
        [SetUp]
        protected new void Init()
        {
            base.Init();

            //arrange
            MockSocketClient
                .When(x => x.Connect())
                .Do(x => throw _socketException);
        }

        private readonly SocketException _socketException = new SocketException(10048);

        private readonly OperationDto _operation = new OperationDto
        {
            Operation = 115,
            Device = 121,
            Params = new[] {65535},
            Timeout = Timeout
        };

        private void When()
        {
            try
            {
                Sut.SendOperation(_operation);
            }
            catch (ControlLineOffline)
            {
            }
        }

        private void WhenWithErrors()
        {
            Sut.SendOperation(_operation);
        }

        [Test]
        [Description("Then Connection Was Attempted To Be Opened")]
        public void ConnectionOpenTest()
        {
            //act
            When();

            //assert
            MockSocketClient
                .Received(1)
                .Connect();
        }

        [Test]
        [Description("Then Payload Was Not Sent")]
        public void PayloadSendTest()
        {
            //act
            When();

            //assert
            MockSocketClient
                .DidNotReceive()
                .Send(Arg.Any<byte[]>());
        }

        [Test]
        [Description("Then Data Was Not Received")]
        public void DataRecievedTest()
        {
            //act
            When();

            //assert
            MockThreadOperations
                .DidNotReceive()
                .WaitUntilFuncTimeout(Arg.Any<Func<byte[]>>(), Arg.Any<int>());
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

        [Test]
        [Description("Then Control Line Offline Error Occurs")]
        public void ControlLineOfflineTest()
        {
            //assert
            Assert.Throws<ControlLineOffline>(WhenWithErrors);
        }
    }
}