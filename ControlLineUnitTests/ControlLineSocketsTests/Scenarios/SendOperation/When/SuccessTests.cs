using System;
using System.Net.Sockets;
using ControlLine.Dto;
using ControlLine.Exception;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation.When
{
    [TestFixture((byte)115,(byte)121,new int[]{65535},new byte[]{115,121,1,255,255},(byte)115,65535,new byte[]{115,1,255,255})]
    [TestFixture((byte)100,(byte)50,new int[]{120},new byte[]{100,50,0,120},(byte)115,new byte[]{115,0,255,255})]
    [TestFixture((byte)50,(byte)90,new int[]{111,112},new byte[]{50,90,0,111,0,112})]
    [TestFixture((byte)40,(byte)112,new int[]{123,321},new byte[]{40,112,0,123,1,65,1})]
    [Description("Given ControlLineSockets.SendOperation Is Called, When Response Retrieved Successfully")]
    public class SuccessTests : SendOperationTests
    {
        private readonly byte[] _payload;
        private readonly byte[] _response;
        private readonly byte _status;
        private readonly int _returnData;
        private readonly OperationDto _operation;
        private readonly OperationResponseDto _operationResponse;
        private readonly OperationResponseDto _result;
        
        public SuccessTests(byte operation, byte deviceId, int[] parameters, byte[] payload, byte status, int returnData, byte[] response)
        {
            _payload = payload;
            _response = response;
            _status = status;
            _returnData = returnData;
            _operation = new OperationDto() {Operation = operation, Device = deviceId, Params = parameters};
            _operationResponse = new OperationResponseDto() {Status = _status, Returns = returnData};
            MockSocketClient
                .Recieve()
                .Returns(_response);
            MockStatusValidator
                .IsError(Arg.Any<byte>())
                .Returns(true);

            _result = Sut.SendOperation(_operation);
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
        [Description("Then Data Was Received")]
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

        //TODO: change to 1 method call
        [Test]
        [Description("Then Response Status Was Validated")]
        public void IsErrorTest()
        {
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
            MockStatusValidator
                .Received(1)
                .ValidateError(Arg.Any<byte>());
            MockStatusValidator
                .Received()
                .ValidateError(Arg.Is(_status));
        }
        
        [Test]
        [Description("Then Operation Response Is Returned")]
        public void SocketErrorTest()
        {
            Assert.AreEqual(_operationResponse,_result);
        }
    }
}