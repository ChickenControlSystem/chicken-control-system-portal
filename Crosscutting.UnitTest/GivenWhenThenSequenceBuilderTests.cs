using Crosscutting.Sequencing.Contract;
using Crosscutting.Sequencing.Sequence;
using Crosscutting.Threading;
using NSubstitute;

namespace Crosscutting.UnitTest
{
    public abstract class GivenWhenThenSequenceBuilderTests<T> : GenericGivenWhenThenTests<T>
        where T : ISimpleSequenceBuilder
    {
        protected FluentSequenceBuilder FluentSequenceBuilder;
        protected SequenceResultEnum Result;

        public override void Given()
        {
            FluentSequenceBuilder = new FluentSequenceBuilder(new SequenceFactory(Substitute.For<IThreadOperations>()));
        }

        public override void When()
        {
            Result = SUT
                .Build()
                .Run();
        }
    }
}