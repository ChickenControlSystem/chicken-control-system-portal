using System;
using System.Net;
using System.Net.Sockets;
using ControlLine.Contract.Sockets;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.Send.When
{
    /// <summary>
    /// covers situations
    /// <para>==================================================================</para>
    /// <para>* when socket is offline</para>
    /// <para>* when socket doesnt exist</para>
    /// </summary>
    [TestFixture]
    public class When_Sockets_Connection_Cannot_Be_Opened : Given_Send_Is_Called
    {
        private readonly Exception _exception;
        private readonly SocketException _socketException = new SocketException(10048);
        
        public When_Sockets_Connection_Cannot_Be_Opened()
        {
            MockSocketClient
                .When(x => x.Connect())
                .Do(x => throw _socketException );
            try
            {
                Sut.Send("random data");
            }
            catch (Exception e)
            {
                _exception = e;
            }
        }

        [Test]
        public void Then_Socket_Attempts_To_Connect()
        {
            MockSocketClient.Received(1).Connect();
        }

        [Test]
        public void Then_Data_Is_Not_Attempted_To_Be_Sent()
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