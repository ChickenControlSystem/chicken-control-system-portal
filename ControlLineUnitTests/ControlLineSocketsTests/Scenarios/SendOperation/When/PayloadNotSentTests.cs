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
        private readonly SocketException _socketException = new SocketException(10048);
        private readonly byte[] _payload = {115,121,1,255,255};
        private readonly OperationDto _operation = new OperationDto(){Operation = 115,Device = 121,Params = new []{65535}};

        [SetUp]
        protected new void Init()
        {
            base.Init();
            
            //arrange
            MockSocketClient
                .When(x => x.Send(Arg.Any<byte[]>()))
                .Do(x =>  throw _socketException );
            
        }

        private void When()
        {
            try { Sut.SendOperation(_operation,TimeOut); }catch (ControlLineOffline) { }
        }
        
        private void WhenWithErrors()
        {
            Sut.SendOperation(_operation,TimeOut);
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
                .Send(Arg.Is<byte[]>( payload => payload.SequenceEqual(_payload)));
            MockSocketClient
                .Received(1)
                .Send(Arg.Any<byte[]>());
        }

        [Test]
        [Description("Then Data Was Not Received")]
        public void DataRecievedTest()
        { 
            //act
            When();

            //assert
            MockSocketClient
                .DidNotReceive()
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
        
        [Test]
        [Description("Then Control Line Offline Error Occurs")]
        public void ControlLineOfflineTest()
        {
            //act/assert
            Assert.Throws<ControlLineOffline>(WhenWithErrors);
        }
    }
}