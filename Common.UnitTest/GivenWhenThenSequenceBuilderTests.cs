using BLL.Common.Contract;
using BLL.Common.Sequence;
using NSubstitute;
using Threading;

namespace UnitTest
{
    public abstract class GivenWhenThenSequenceBuilderTests<T> : GenericGivenWhenThenTests<T>
        where T : ISimpleSequenceBuilder
    {
        protected FluentSequenceBuilder FluentSequenceBuilder;

        public override void Given()
        {
            FluentSequenceBuilder = new FluentSequenceBuilder(new SequenceFactory(Substitute.For<IThreadOperations>()));
        }

        public override void When()
        {
            SUT
                .Build()
                .Run();
        }
    }
}