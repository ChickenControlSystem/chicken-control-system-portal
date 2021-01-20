using System;
using ControlLine.Contract;
using ControlLine.Contract.Sockets;
using ControlLine.Dto;
using ControlLine.Sockets;
using NSubstitute;

namespace ControlLineUnitTests.ControlLineSocketsTests.Scenarios.SendOperation
{
    public abstract class SendOperationTests
    {

        protected ControlLineSockets Sut;
        protected IRawSocketClient MockSocketClient;
        protected IControlLineStatusValidator MockStatusValidator;
        protected const int TimeOut = 1;

        protected void Init()
        {
            MockStatusValidator = Substitute.For<IControlLineStatusValidator>();
            MockSocketClient = Substitute.For<IRawSocketClient>();
            Sut = new ControlLineSockets(MockSocketClient,MockStatusValidator);
        }
    }
}