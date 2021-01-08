using System;
using System.Net.Sockets;
using ControlLine.Dto;
using NSubstitute;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation.When
{

    /// <summary>
    /// covers situations
    /// <para>==================================================================</para>
    /// <para>* when socket is offline</para>
    /// <para>* when socket was unexpectedly closed</para>
    /// </summary>
    [TestFixture]
    public class When_Read_Function_Over_Sockets_Cannot_Be_Sent : Given_Read_Is_Called
    {
        private readonly Exception _exception;
        private readonly SocketException _socketException = new SocketException(10048);
        private const string Payload = "read";
        
        public When_Read_Function_Over_Sockets_Cannot_Be_Sent()
        {
            MockSocketClient
                .When(x => x.Send("read"))
                .Do(x =>  throw _socketException );
            try
            {
                Sut.SendOperation(new OperationDto{Name = 0, Device = 0, Params = new Byte[]{0}});
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }

        [Test]
        public void Then_Socket_Connects_Successfully()
        {
            MockSocketClient.Received(1).Connect();
        }

        [Test]
        public void Then_Data_Is_Attempted_To_Be_Sent_Once()
        {
            MockSocketClient.DidNotReceive().Send(Arg.Any<string>());
        }
        
        [Test]
        public void Then_Data_Is_Not_Attempted_To_Be_Recieved()
        {
            MockSocketClient.DidNotReceive().Recieve();
        }
        
        [Test]
        public void Then_Socket_Is_Closed()
        {
            MockSocketClient.Received(1).Close();
        }
        
        [Test]
        public void Then_SocketException_Must_Be_Thrown()
        {
            Assert.AreEqual(_socketException, _exception);
        }
    }
}