using System;
using System.Linq;
using System.Net.Sockets;
using ControlLine.Dto;
using ControlLine.Exception;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation.When
{
    [TestFixture((byte)115,(byte)121,new []{65535},new byte[]{115,121,1,255,255},(byte)115,65535,new byte[]{115,1,255,255})]
    [TestFixture((byte)100,(byte)50,new []{120},new byte[]{100,50,0,120},(byte)115,255,new byte[]{115,0,255})]
    [TestFixture((byte)50,(byte)90,new []{111,112},new byte[]{50,90,0,111,0,112},(byte)115,4351,new byte[]{115,1,255,16})]
    [TestFixture((byte)40,(byte)112,new []{123,321},new byte[]{40,112,0,123,1,65,1},(byte)115,16,new byte[]{115,0,16})]
    [Description("Given ControlLineSockets.SendOperation Is Called, When Response Retrieved Successfully")]
    public class SuccessTests : SendOperationTests
    {
        private readonly byte[] _payload;
        private readonly byte[] _response;
        private readonly byte _status;
        private readonly int _returnData;
        private readonly OperationDto _operation;
        private readonly OperationResponseDto _operationResponse;
        private OperationResponseDto _result;
        
        public SuccessTests(byte operation, byte deviceId, int[] parameters, byte[] payload, byte status, int returnData, byte[] response)
        {
            _payload = payload;
            _response = response;
            _status = status;
            _returnData = returnData;
            _operation = new OperationDto() {Operation = operation, Device = deviceId, Params = parameters};
            _operationResponse = new OperationResponseDto() {Status = _status, Returns = returnData};
        }
        
        [SetUp]
        protected new void Init()
        {
            base.Init();
            
            //arrange
            MockSocketClient
                .Recieve()
                .Returns(_response);
            MockStatusValidator
                .IsError(Arg.Any<byte>())
                .Returns(false);
        }        
        
        private void When()
        {
            Sut.SendOperation(_operation);
        }
        
        private void WhenWithReturn()
        {
            _result = Sut.SendOperation(_operation);
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
                .DidNotReceive()
                .ValidateError(Arg.Any<byte>());
        }
        
        [Test]
        [Description("Then Operation Response Is Returned")]
        public void ResponseTest()
        {
            //act
            WhenWithReturn();

            //assert
            Assert.AreEqual(_operationResponse.Returns,_result.Returns);
            Assert.AreEqual(_operationResponse.Status,_result.Status);
        }
    }
}