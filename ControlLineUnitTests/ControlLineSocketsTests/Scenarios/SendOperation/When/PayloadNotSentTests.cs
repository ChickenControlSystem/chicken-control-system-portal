using System;
using System.Collections.Generic;
using System.Net.Sockets;
using ControlLine.Dto;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using ControlLine.Exception;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation.When
{
    [TestFixture]
    [Description("Given ControlLineSockets.SendOperation Is Called, When Payload Cannot Be Sent")]
    public class PayloadNotSentTests : SendOperationTests
    {
        private readonly Exception _exception;
        private readonly SocketException _socketException = new SocketException(10048);
        private readonly byte[] _payload = new byte[]{115,121,1,255,255};
        private readonly OperationDto _operation = new OperationDto();
        
        public PayloadNotSentTests()
        {
            MockSocketClient
                .When(x => x.Send(Arg.Any<byte[]>()))
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
        [Description("Then Connection Was Opened")]
        public void ConnectionOpenTest()
        {
            MockSocketClient
                .Received(1)
                .Connect();
        }
        
        //TODO: change to 1 method call
        [Test]
        [Description("Then Payload Was Attempted To Be Sent")]
        public void PayloadSendTest()
        {
            MockSocketClient
                .Received()
                .Send(Arg.Is(_payload));
            MockSocketClient
                .Received(1)
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