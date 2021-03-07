using System;
using Crosscutting.Contract.HAL.ControlLine;
using NUnit.Framework;

namespace HAL.ControlLine.Tests.Unit.ControlLineSockets.SendOperationTests.Scenarios
{
    [TestFixture(65536)]
    [TestFixture(75540)]
    [TestFixture(65540, 122)]
    [TestFixture(123, 65540)]
    [TestFixture(-2)]
    public class When_The_Operation_Is_Invalid : Given_ControlLine_SendOperation_Was_Called
    {
        private OperationDto _operation;
        private readonly int[] _operationParams;

        public When_The_Operation_Is_Invalid(int operationParam)
        {
            _operationParams = new[] {operationParam};
        }

        public When_The_Operation_Is_Invalid(int operationParam1, int operationParam2)
        {
            _operationParams = new[] {operationParam1, operationParam2};
        }

        public override void When()
        {
            _operation = new OperationDto {Params = _operationParams};
        }

        [Test]
        public void Then_ArgumentException_Is_Thrown()
        {
            Assert.Throws<ArgumentException>(() => SUT.SendOperation(_operation));
        }
    }
}