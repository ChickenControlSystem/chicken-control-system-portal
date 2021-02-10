using ControlLine.Contract;
using HAL.Operations.Contract;
using NSubstitute;
using UnitTest;

namespace HAL.Operations.Tests.Unit.AxisOperations
{
    public abstract class Given_Operation_Was_Called : GenericGivenWhenThenTests<Operations.AxisOperations>
    {
        protected IErrorService MockErrorService;
        protected IControlLine MockControlLine;

        protected override void Given()
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