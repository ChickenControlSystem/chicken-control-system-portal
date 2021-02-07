using System;
using ControlLine.Dto;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSockets.SendOperationTests.Scenarios
{
    [TestFixture(65536)]
    [TestFixture(75540)]
    [TestFixture(65540, 122)]
    [TestFixture(123, 65540)]
    [TestFixture(-2)]
    public class When_The_Operation_Parameters_Are_Out_Of_Range : Given_ControlLine_SendOperation_Was_Called
    {
        private OperationDto _operation;
        private readonly int[] _operationParams;

        public When_The_Operation_Parameters_Are_Out_Of_Range(int operationParam)
        {
            _operationParams = new[] {operationParam};
        }

        public When_The_Operation_Parameters_Are_Out_Of_Range(int operationParam1, int operationParam2)
        {
            _operationParams = new[] {operationParam1, operationParam2};
        }

        protected override void When()
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