using Crosscutting.Services.Contract.HAL.Interface;
using Crosscutting.UnitTest;
using NSubstitute;

namespace HAL.Operations.Tests.Unit.AxisOperations
{
    public abstract class Given_Operation_Was_Called : GenericGivenWhenThenTests<Operations.AxisOperations>
    {
        protected IControlLine MockControlLine;
        protected IErrorService MockErrorService;

        public override void Given()
        {
            MockControlLine = Substitute.For<IControlLine>();
            MockErrorService = Substitute.For<IErrorService>();

            SUT = new Operations.AxisOperations(
                MockErrorService,
                MockControlLine
            );
        }
    }
}