using System;
using System.Net.Sockets;
using System.Threading;
using ControlLine.Dto;
using ControlLine.Exception;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation.When
{
    [TestFixture(5001)]
    [TestFixture(6000)]
    [TestFixture(7000)]
    [Description("Given ControlLineSockets.SendOperation Is Called, When Payload Cannot Be Sent")]
    public class ControlLineTimesOutTests : SendOperationTests
    {
        private readonly Exception _exception;
        private readonly byte[] _payload = new byte[]{115,121,1,255,255};
        private readonly OperationDto _operation = new OperationDto() {Operation = 115, Device = 121, Params = new int[] {65535}};
        
        public ControlLineTimesOutTests(int recievePeriod)
        { 
            MockSocketClient
                .When(x => x.Recieve())
                .Do(Callback.Always(x => { Thread.Sleep(recievePeriod); }));

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
        [Description("Then Payload Was Sent")]
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
        [Description("Then Data Was Attempted To Be Received")]
        public void DataRecievedTest()
        {
            MockSocketClient
                .Received(1)
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
        [Description("Then Control Line Time Out Error Occurs")]
        public void SocketErrorTest()
        {
            Assert.AreEqual(new ControlLineTimeOut(),_exception);
        }
    }
}