using System;
using System.Linq;
using System.Threading;
using ControlLine.Dto;
using ControlLine.Exception;
using ControlLine.Exception.Hardware;
using ControlLine.Exception.Hardware.Axis;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation.When
{
    [TestFixture]
    [Description("Given ControlLineSockets.SendOperation Is Called, When Device Error Occurs")]
    public class DeviceFailiureTests : SendOperationTests
    {
        private readonly byte[] _payload = new byte[]{115,121,1,255,255};
        private readonly byte _status = 115;
        private readonly OperationDto _operation = new OperationDto() {Operation = 115, Device = 121, Params = new int[] {65535}};
        private readonly DeviceFailiure _deviceFailiure = new AxisObstruction();

        [SetUp]
        protected new void Init()
        {
            base.Init();
            
            //arrange
            MockStatusValidator
                .IsError(Arg.Any<byte>())
                .Returns(true);
            MockStatusValidator
                .ValidateError(Arg.Any<byte>())
                .Returns(_deviceFailiure);
            MockSocketClient
                .Recieve()
                .Returns(new byte[]{_status});
        }
        
        private void When()
        {
            try { Sut.SendOperation(_operation,TimeOut); }catch (DeviceFailiure) { }
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
        [Description("Then Data Was Received")]
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

        //TODO: change to 1 method call
        [Test]
        [Description("Then Response Status Was Validated")]
        public void IsErrorTest()
        { 
            //act
            When();

            //assert
            MockStatusValidator
                .Received(1)
                .IsError(Arg.Any<byte>());
            MockStatusValidator
                .Received()
                .IsError(Arg.Is(_status));
        }
        
        //TODO: change to 1 method call
        [Test]
        [Description("Then Response Error Was Validated")]
        public void ValidateErrorTest()
        { 
            //act
            When();

            //assert
            MockStatusValidator
                .Received(1)
                .ValidateError(Arg.Any<byte>());
            MockStatusValidator
                .Received()
                .ValidateError(Arg.Is(_status));
        }
        
        [Test]
        [Description("Then Device Failure Error Occurs")]
        public void SocketErrorTest()
        {
            //act/assert
            Assert.Throws<AxisObstruction>(WhenWithErrors);
        }

    }
}