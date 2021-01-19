using System;
using System.Net.Sockets;
using ControlLine.Dto;
using ControlLine.Exception;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation.When
{
    [TestFixture]
    [Description("Given ControlLineSockets.SendOperation Is Called, When Connection Cannot Be Opened")]
    public class ConnectionNotOpenedTests : SendOperationTests
    {
        private readonly Exception _exception;
        private readonly SocketException _socketException = new SocketException(10048);
        private readonly byte[] _payload = new byte[]{115,121,1,255,255};
        private readonly OperationDto _operation = new OperationDto(){Operation = 115, Device = 121, Params = new int[]{65535}};
        
        public ConnectionNotOpenedTests()
        {
            MockSocketClient
                .When(x => x.Connect())
                .Do(x =>  throw _socketException );

            try
            {
                Sut.SendOperation(
                    _operation
                );
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }

        [Test]
        [Description("Then Connection Was Attempted To Be Opened")]
        public void ConnectionOpenTest()
        {
            MockSocketClient
                .Received(1)
                .Connect();
        }
        
        [Test]
        [Description("Then Payload Was Not Sent")]
        public void PayloadSendTest()
        {
            MockSocketClient
                .DidNotReceive()
                .Send(Arg.Any<byte[]>());
        }

        [Test]
        [Description("Then Data Was Not Received")]
        public void DataRecievedTest()
        {
            MockSocketClient
                .DidNotReceive()
                .Recieve();
        }
        
        [Test]
        [Description("Then Connection Was Closed")]
        public void ConnectionCloseTest()
        {
            MockSocketClient
                .Received(1)
                .Close();
        }

        [Test]
        [Description("Then Response Status Was Not Validated")]
        public void IsErrorTest()
        {
            MockStatusValidator
                .DidNotReceive()
                .IsError(Arg.Any<byte>());
        }
        
        [Test]
        [Description("Then Response Error Was Not Validated")]
        public void ValidateErrorTest()
        {
            MockStatusValidator
                .DidNotReceive()
                .ValidateError(Arg.Any<byte>());
        }
        
        [Test]
        [Description("Then Control Line Offline Error Occurs")]
        public void SocketErrorTest()
        {
            Assert.AreEqual(new ControlLineOffline(),_exception);
        }
    }
}